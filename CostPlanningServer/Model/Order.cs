using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CostPlanningServer.Model
{
    public class Order
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public int UserId { get; set; }
        public double Cost { get; set; }
        public bool IsWriteToDb { get; set; }
        public string Description { get; set; }
        public User User { get; set; }
        public int CategoryId { get; set; }
        public Category Category { get; set; }
    }
}
