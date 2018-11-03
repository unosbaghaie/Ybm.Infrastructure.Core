using System;
using System.Collections.Generic;
using System.Text;

namespace Ybm.Infrastructure.Core.Pagination
{
    public class GridState
    {
        public int skip { get; set; }
        public int take { get; set; }

        public HashSet<SortDescriptor> sort { get; set; }
    }
}
