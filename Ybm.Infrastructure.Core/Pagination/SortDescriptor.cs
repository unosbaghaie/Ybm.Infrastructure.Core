using System;
using System.Collections.Generic;
using System.Text;

namespace Ybm.Infrastructure.Core.Pagination
{
    public class SortDescriptor
    {
        public string field { get; set; }
        public string dir { get; set; } = "asc";
    }
}
