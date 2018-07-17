using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ybm.Infrastructure.Core
{
    public class QueryModel
    {
        public List<CustomFilterMetaDataDescriptor> CustomFilterMetDataDescriptors = new List<CustomFilterMetaDataDescriptor>();

        public int pageSize { get; set; }

        public int skip { get; set; }
    }
}
