using System;
using System.Collections.Generic;

namespace ProductDomainModels.Models
{
    public partial class Order
    {
        public Order()
        {
            OrderItems = new HashSet<OrderItem>();
        }

        public int Id { get; set; }
        public int UserId { get; set; }
        public DateTime? CreatedAt { get; set; }

        public User User { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
    }
}
