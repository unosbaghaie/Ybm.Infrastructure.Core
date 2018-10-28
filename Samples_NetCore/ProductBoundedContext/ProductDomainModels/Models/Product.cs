using System;
using System.Collections.Generic;

namespace ProductDomainModels.Models
{
    public partial class Product
    {
        public Product()
        {
            OrderItems = new HashSet<OrderItem>();
            ProductPrices = new HashSet<ProductPrice>();
            ProductTags = new HashSet<ProductTag>();
        }

        public int Id { get; set; }
        public int ProductCategoryId { get; set; }
        public string Name { get; set; }
        public decimal? Price { get; set; }

        public ProductCategory ProductCategory { get; set; }
        public ICollection<OrderItem> OrderItems { get; set; }
        public ICollection<ProductPrice> ProductPrices { get; set; }
        public ICollection<ProductTag> ProductTags { get; set; }
    }
}
