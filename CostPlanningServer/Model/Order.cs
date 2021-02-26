using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostPlanningServer.Model
{
    public class Order
    {
        [Key]
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public double Cost { get; set; }
        public string Description { get; set; }
        public bool IsVisible { get; set; }
        public int ServerId { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
        public ICollection<SyncUser<Order>> SyncUser { get; set; }
    }
}
