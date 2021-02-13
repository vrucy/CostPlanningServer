using System.ComponentModel.DataAnnotations.Schema;

namespace CostPlanningServer.Model
{
    public class SyncUser<T> where T: class
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        [ForeignKey(nameof(Item))]
        public int ItemId { get; set; }
        public T Item { get; set; }
    }
}
