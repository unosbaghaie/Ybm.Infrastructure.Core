using System;
using System.Collections.Generic;

namespace ProductDomainModels.Models
{
    public partial class User
    {
        public User()
        {
            Orders = new HashSet<Order>();
        }

        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }

        public ICollection<Order> Orders { get; set; }
    }
}
