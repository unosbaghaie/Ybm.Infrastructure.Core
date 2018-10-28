using System;
using System.Collections.Generic;

namespace ProductDomainModels.Models
{
    public partial class ProductPrice
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public bool IsDefault { get; set; }
        public DateTime StartedAt { get; set; }
        public DateTime EndsAt { get; set; }
        public decimal Price { get; set; }
        public int Tax { get; set; }

        public Product Product { get; set; }
    }
}
