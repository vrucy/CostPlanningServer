using System.ComponentModel.DataAnnotations.Schema;

namespace CostPlanningServer.Model
{
    public class SyncData<T> where T: class
    {
        public int Id { get; set; }
        public string DeviceId { get; set; }
        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public T Item { get; set; }
    }
}
