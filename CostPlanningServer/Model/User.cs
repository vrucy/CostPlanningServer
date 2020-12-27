using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace CostPlanningServer.Model
{
    public class User
    {
        [Key]
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int ServerId { get; set; }
        public ICollection<Order> Orders { get; set; }
    }
}
