using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using CostPlanningServer.DataBase;
using CostPlanningServer.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace CostPlanningServer.Controllers
{
    [Route("[controller]/[action]")]
    [ApiController]
    public class OrderController : ControllerBase
    {
        //TODO logger
        private readonly CostPlanningContext _context;
        //private readonly ILogger _logger;
        public OrderController(CostPlanningContext context/*, ILogger logger*/)
        {
            _context = context;
            //_logger = logger;
        }
        public async Task<IActionResult> PostOrders(List<Order> orders)
        {
            foreach (var item in orders)
            {

                Order o = new Order()
                {
                    CategoryId = item.CategoryId,
                    Cost = item.Cost,
                    Date = item.Date,
                    Description = item.Description,
                    UserId = item.UserId
                };
                await _context.Orders.AddAsync(o);
                await _context.SaveChangesAsync();
                //TODO: This in helper repeat code
                var user = new SyncUser<Order>()
                {
                    UserId = o.UserId,
                    ItemId = o.Id
                };
                _context.SyncUserOrder.Add(user);
                await _context.SaveChangesAsync();
            }
            return Ok();
            //TODO: if save change return ok
        }
        public async Task<IActionResult> UpdateOrders(List<Order> orders)
        {
            var Ids = new Dictionary<int, int>();
            foreach (var item in orders)
            {
                try
                {
                    Order o = new Order()
                    {
                        CategoryId = item.CategoryId,
                        Cost = item.Cost,
                        Date = item.Date,
                        Description = item.Description,
                        //promena
                        UserId = item.User.Id
                    };
                    await _context.Orders.AddAsync(o);
                    await _context.SaveChangesAsync();
                    var user = new SyncUser<Order>()
                    {
                        UserId = o.UserId,
                        ItemId = o.Id
                    };
                    _context.SyncUserOrder.Add(user);
                    await _context.SaveChangesAsync();
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
        public IActionResult GetAllOrders()
        {
            return Ok(_context.Orders.ToList());
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
        public IActionResult GetOrdersCountFromServer()
        {
            return Ok(_context.Orders.Count());
        }
        public IActionResult IsServerAvailable()
        {
            return Ok();
        }
        public IActionResult SyncDisable()
        {
            var categores = _context.Orders.Where(c => c.IsVisible == true);

            var res = new List<int>();
            foreach (var item in categores)
            {
                res.Add(item.Id);
            }

            return Ok(res);
        }
        [Route("{idUser}")]
        public IActionResult SyncVisibility([FromRoute] int idUser)
        {
            //TODO: Refactor
            Dictionary<int, bool> ordersForSync = new Dictionary<int, bool>();
            var userForSync = new List<User>();
            var allOrdersId = _context.Orders.Select(c => c.Id);
            var userOrdersId = _context.SyncUserOrder.Where(c => c.UserId == idUser).Select(x => x.ItemId);

            var res = allOrdersId.Except(userOrdersId);
            foreach (var item in res)
            {
                var category = _context.Orders.FirstOrDefault(x => x.Id == item);
                ordersForSync.Add(item, category.IsVisible);
                var user = new SyncUser<Order>()
                {
                    UserId = idUser,
                    ItemId = category.Id
                };
                _context.SyncUserOrder.Add(user);
            }
            if (res.Any())
            {
                _context.SaveChanges();
            }

            return Ok(JsonConvert.SerializeObject(ordersForSync));
        }
    }
}
