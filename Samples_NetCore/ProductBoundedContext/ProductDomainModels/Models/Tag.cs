using System;
using System.Collections.Generic;

namespace ProductDomainModels.Models
{
    public partial class Tag
    {
        public Tag()
        {
            ProductTags = new HashSet<ProductTag>();
        }

        public int Id { get; set; }
        public string TagName { get; set; }

        public ICollection<ProductTag> ProductTags { get; set; }
    }
}
