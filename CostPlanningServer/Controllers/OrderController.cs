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
                try
                {
                    Order o = new Order()
                    {
                        CategoryId = item.CategoryId,
                        Cost = item.Cost,
                        Date = item.Date,
                        Description = item.Description,
                        UserId = item.UserId,
                        IsWriteToDb = true
                    };
                    await _context.Orders.AddAsync(o);
                }
                catch (Exception e)
                {
                    //_logger.Error(e.Message);
                    throw;
                }
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
                        UserId = item.UserId,
                        IsWriteToDb = true
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
            //TODO: if save change return ok
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
            return Ok(_context.Orders.OrderByDescending(x=>x.Id).FirstOrDefault().Id);
        }
    }
}
