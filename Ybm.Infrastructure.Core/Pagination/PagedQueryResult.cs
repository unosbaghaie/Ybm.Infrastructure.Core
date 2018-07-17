using System;
using System.Collections.Generic;
using System.Text;

namespace Ybm.Infrastructure.Core.Pagination
{
    public class PagedQueryResult<T>
    {
        public int TotalItems { get; set; }
        public IEnumerable<T> Items { get; set; }
    }
}
