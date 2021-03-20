using CostPlanningServer.DataBase;
using CostPlanningServer.Interface;
using CostPlanningServer.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostPlanningServer.Services
{
    public class SynchronizationService : ISynchronization
    {
        private readonly CostPlanningContext _context;

        public SynchronizationService(CostPlanningContext context)
        {
            _context = context;
        }
        public async Task SyncDataAllCategories(string deviceId)
        {
            var categories = _context.Categories;
            foreach (var c in categories)
            {
                var category = new SyncData<Category>()
                {
                    DeviceId = deviceId,
                    ItemId = c.Id
                };
                await _context.SyncDataCategory.AddAsync(category);
            }
            await _context.SaveChangesAsync();
        }

        public async Task SyncDataAllOrders(string deviceId)
        {
            var orders = _context.Orders;
            foreach (var o in orders)
            {
                var order = new SyncData<Order>()
                {
                    DeviceId = deviceId,
                    ItemId = o.Id
                };
                await _context.SyncDataOrder.AddAsync(order);
            }
            await _context.SaveChangesAsync();
        }

        public async Task SyncDataCategory(Category category, string deviceId)
        {
            var cat = new SyncData<Category>()
            {
                ItemId = category.Id,
                DeviceId = deviceId
            };
            await _context.SyncDataCategory.AddAsync(cat);
            await _context.SaveChangesAsync();
        }

        public async Task SyncDataOrder(Order order, string deviceId)
        {
            //do transaction here
            var o = new SyncData<Order>()
            {
                ItemId = order.Id,
                DeviceId = deviceId
            };
            try
            {
                await _context.SyncDataOrder.AddAsync(o);
                await _context.SaveChangesAsync();

            }
            catch (System.Exception e)
            {

                throw;
            }

        }
        public async Task SyncDataOrders(List<Order> orders, string deviceId)
        {
            try
            {
                foreach (var item in orders)
                {
                    var o = new SyncData<Order>()
                    {
                        ItemId = item.Id,
                        DeviceId = deviceId
                    };
                await _context.SyncDataOrder.AddAsync(o);
                }
                await _context.SaveChangesAsync();
            }
            catch (System.Exception e)
            {

                throw;
            }
        }
    }
}
