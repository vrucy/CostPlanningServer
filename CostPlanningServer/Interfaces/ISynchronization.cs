using CostPlanningServer.Model;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace CostPlanningServer.Interface
{
    public interface ISynchronization
    {
        Task SyncDataAllOrders(string deviceId);
        Task SyncDataAllCategories(string deviceId);
        Task SyncDataCategory(Category category, string deviceId);
        Task SyncDataCategories(List<Category> categories, string deviceId);
        Task SyncDataOrder(Order order, string deviceId);
        Task SyncDataOrders(List<Order> orders, string deviceId);
        /// <summary>
        /// Search and return orders what need sync.
        /// </summary>
        /// <param name="deviceId"></param>
        /// <returns>Dictionary what represent order id and isVisible </returns>
        Task<List<Order>> SyncOrders(string deviceId);
        Task<List<Category>> SyncCategories(string deviceId);
    }
}
