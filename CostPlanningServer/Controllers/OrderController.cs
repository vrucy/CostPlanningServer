using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using CostPlanningServer.DataBase;
using CostPlanningServer.Model;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Serilog;

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

            }
            //_context.Orders.AddRange(orders);
            try
            {
                await _context.SaveChangesAsync();
                return Ok();

            }
            catch (Exception e)
            {

                throw;
            }
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
                    Ids.Add(item.Id, o.Id);
                }
                catch (Exception e)
                {
                    //_logger.Error(e.Message);
                    throw;
                }
            }
            try
            {

                return Ok(JsonConvert.SerializeObject(Ids));

            }
            catch (Exception e)
            {

                throw;
            }
        }
        public IActionResult GetAllOrders()
        {
            return Ok(_context.Orders.ToList());
        }
        public IActionResult GetAllOrdersByIds(List<int> ids)
        {
            //if (ids.Count == 0)
            //{
            //    return Ok(_context.Orders);
            //}
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
    }
}
