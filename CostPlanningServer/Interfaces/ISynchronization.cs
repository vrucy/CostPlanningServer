using CostPlanningServer.Model;
using System.Threading.Tasks;

namespace CostPlanningServer.Interface
{
    public interface ISynchronization
    {
        Task SyncDataAllOrders(string deviceId);
        Task SyncDataAllCategories(string deviceId);
        Task SyncDataCategory(Category category, string deviceId);
        Task SyncDataOrder(Order order, string deviceId);

    }
}
