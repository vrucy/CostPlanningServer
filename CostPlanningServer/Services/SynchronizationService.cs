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
        public async Task SyncDataAllCategories( string deviceId)
        {
            var categories = _context.Categories;
            foreach (var c in categories)
            {
                var category = new SyncData<Order>()
                {
                    DeviceId = deviceId,
                    ItemId = c.Id
                };
                await _context.SyncDataOrder.AddAsync(category);
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

        public Task SyncDataCategory(Category category, string deviceId)
        {
            throw new System.NotImplementedException();
        }

        public Task SyncDataOrder(Order order, string deviceId)
        {
            throw new System.NotImplementedException();
        }
    }
}
