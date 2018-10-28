using System;
using System.Collections.Generic;

namespace ProductDomainModels.Models
{
    public partial class ProductTag
    {
        public int Id { get; set; }
        public int ProductId { get; set; }
        public int TagId { get; set; }

        public Product Product { get; set; }
        public Tag Tag { get; set; }
    }
}
