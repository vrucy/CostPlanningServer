using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CostPlanningServer.DataBase;
using CostPlanningServer.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using CostPlanningServer.Interface;

namespace CostPlanningServer.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //TODO logger
        private readonly CostPlanningContext _context;
        private readonly ISynchronization _synchronization;
        //private readonly ILogger _logger;
        public OrderController(CostPlanningContext context, ISynchronization synchronization/*, ILogger logger*/)
        {
            _context = context;
            _synchronization = synchronization;
            //_logger = logger;
        }
        [Route("{deviceId}")]
        public async Task PostOrder([FromBody] Order order, [FromRoute] string deviceId)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            await _synchronization.SyncDataOrder(order, deviceId);

        }
        [Route("{deviceId}")]
        public async Task<IActionResult> UpdateOrders(List<Order> orders, [FromRoute] string deviceId)
        {
            var Ids = new Dictionary<int, int>();
            foreach (var item in orders)
            {
                try
                {
                    Order o = new Order()
                    {
                        CategoryId = item.Category.Id,
                        Cost = item.Cost,
                        Date = item.Date,
                        IsVisible = item.IsVisible,
                        Description = item.Description,
                        UserId = item.User.Id
                    };
                    await _context.Orders.AddAsync(o);
                    await _context.SaveChangesAsync();

                    await _synchronization.SyncDataOrder(o, deviceId);
                    Ids.Add(item.Id, o.Id);
                }
                catch (Exception e)
                {
                    //_logger.Error(e.Message);
                    throw;
                }
            }
            return Ok(JsonConvert.SerializeObject(Ids));
        }
        [Route("{deviceId}")]
        public async Task<IActionResult> GetAllOrders([FromRoute] string deviceId)
        {
            await _synchronization.SyncDataAllOrders(deviceId);
            var orders = _context.Orders.ToList();

            return Ok(orders);
        }
        public IActionResult GetAllOrdersByIds(List<int> ids)
        {
            var orders = _context.Orders.Where(o => !ids.Contains(o.Id));
            return Ok(orders);
        }
        public IActionResult GetLastOrderServerId()
        {
            var orders = _context.Orders.OrderByDescending(x => x.Id);

            if (!orders.Any())
            {
                return Ok(-1);
            }

            return Ok(orders.FirstOrDefault().Id);
        }
        [Route("{deviceId}")]
        public async Task<IActionResult> SyncVisibility([FromRoute] string deviceId)
        {
            //TODO: Refactor
            Dictionary<int, bool> ordersForSync = new Dictionary<int, bool>();
            var userForSync = new List<User>();
            var allOrdersId = _context.Orders.Select(c => c.Id);
            var userOrdersId = _context.SyncDataOrder.Where(c => c.DeviceId.Equals(deviceId)).Select(x => x.ItemId);

            var res = allOrdersId.Except(userOrdersId);
            if (res.Any())
            {
                var orders = new List<Order>();
                foreach (var item in res)
                {
                    var order = _context.Orders.FirstOrDefault(x => x.Id == item);
                    ordersForSync.Add(item, order.IsVisible);
                    orders.Add(order);
                }

                await _synchronization.SyncDataOrders(orders, deviceId);
            }

            return Ok(JsonConvert.SerializeObject(ordersForSync));
        }
        [Route("{deviceId}")]
        public async Task<IActionResult> EditOrder(Order o, [FromRoute] string deviceId)
        {
            var order = _context.Orders.FirstOrDefault(x => x.Id == o.ServerId);
            if (order != null)
            {
                var ordersForDelete = _context.SyncDataOrder.Where(x => x.ItemId == order.Id);
                _context.SyncDataOrder.RemoveRange(ordersForDelete);
                order.IsVisible = o.IsVisible;
                order.Cost = o.Cost;
                order.Description = o.Description;
                order.Date = o.Date;

                await _synchronization.SyncDataOrder(order, deviceId);

                return Ok();
            }

            return BadRequest();
        }
    }
}
