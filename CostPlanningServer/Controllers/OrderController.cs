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
        public async Task<IActionResult> PostOrder([FromBody] Order order, [FromRoute] string deviceId)
        {
            _context.Orders.Add(order);
            await _context.SaveChangesAsync();

            await _synchronization.SyncDataOrder(order, deviceId);
            return Ok(order);
        }
        [Route("{deviceId}")]
        public async Task<IActionResult> UpdateOrders(List<Order> orders, [FromRoute] string deviceId)
        {
            var Ids = new Dictionary<int, int>();
            foreach (var item in orders)
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
            return Ok(JsonConvert.SerializeObject(Ids));
        }
        [Route("{deviceId}")]
        public async Task<IActionResult> GetAllOrders([FromRoute] string deviceId)
        {
            await _synchronization.SyncDataAllOrders(deviceId);
            var orders = _context.Orders.ToList();

            return Ok(orders);
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
        [Route("{deviceId}")]
        public async Task<IActionResult> GetUnsyncOrders([FromRoute] string deviceId)
        {
            var groupOrders = _context.SyncDataOrder.ToLookup(x => x.DeviceId);
            var usersOrders = _context.SyncDataOrder.Where(x=>x.DeviceId == deviceId).ToList();
            var ordersIds = new List<int>();
            var xx = groupOrders.Where(x=>x.Key == deviceId);
            foreach (var group in groupOrders.Where(x =>x.Key != deviceId))
            {
                var res = group.ToList().Select(x=>x.ItemId).Except(usersOrders.Select(x=>x.ItemId));
                ordersIds.AddRange(res);                
            }

            var orders = _context.Orders.Where(x=> ordersIds.Contains(x.Id));
            await _synchronization.SyncDataOrders(orders.ToList(), deviceId);
            return Ok(orders);
        }
    }
}
