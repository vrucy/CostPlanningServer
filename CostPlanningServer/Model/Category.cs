using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostPlanningServer.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public ICollection<SyncData<Category>> SyncUser { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
