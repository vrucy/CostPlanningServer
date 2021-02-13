using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CostPlanningServer.Model
{
    public class Category
    {
        [Key]
        public int Id { get; set; }
        public string Name { get; set; }
        public bool IsVisible { get; set; }
        public int ServerId { get; set; }
        public ICollection<SyncUser<Category>> SyncUser { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
