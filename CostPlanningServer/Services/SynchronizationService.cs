using CostPlanningServer.DataBase;
using CostPlanningServer.Interface;
using CostPlanningServer.Model;
using System.Collections.Generic;
using System.Linq;
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

        public async Task SyncDataCategories(List<Category> categories, string deviceId)
        {
            try
            {
                foreach (var item in categories)
                {
                    var c = new SyncData<Category>()
                    {
                        ItemId = item.Id,
                        DeviceId = deviceId
                    };
                    await _context.SyncDataCategory.AddAsync(c);
                }
                await _context.SaveChangesAsync();
            }
            catch (System.Exception e)
            {

                throw;
            }
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

        public async Task<List<Order>> SyncOrders(string deviceId)
        {
            var ordersForSync = new List<Order>();
            var userForSync = new List<User>();
            var allOrdersId = _context.Orders.Select(c => c.Id);
            var userOrdersId = _context.SyncDataOrder.Where(c => c.DeviceId.Equals(deviceId)).Select(x => x.ItemId);
            //var orders = new List<Order>();

            var res = allOrdersId.Except(userOrdersId);
            if (res.Any())
            {
                foreach (var item in res)
                {
                    var order = _context.Orders.FirstOrDefault(x => x.Id == item);
                    ordersForSync.Add(order);
                }

                await SyncDataOrders(ordersForSync, deviceId);
            }
            return ordersForSync;
        }
        public async Task<List<Category>> SyncCategories(string deviceId)
        {
            var categoriesForSync = new List<Category>();
            var userForSync = new List<User>();
            var allCategoriesId = _context.Categories.Select(c => c.Id);
            var userCategoriesId = _context.SyncDataCategory.Where(c => c.DeviceId.Equals(deviceId)).Select(x => x.ItemId);

            var res = allCategoriesId.Except(userCategoriesId);
            if (res.Any())
            {
                var categories = new List<Category>();
                foreach (var item in res)
                {
                    var category = _context.Categories.FirstOrDefault(x => x.Id == item);
                    categories.Add(category);
                }
                await SyncDataCategories(categories, deviceId);
            }
            return categoriesForSync;
        }
    }
}
