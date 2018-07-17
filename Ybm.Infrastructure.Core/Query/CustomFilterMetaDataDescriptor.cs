using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ybm.Infrastructure.Core
{
    public class CustomFilterMetaDataDescriptor
    {
        public string ControlType { get; set; }
        public string Member { get; set; }
        public string Name { get; set; }
        public string Value { get; set; }
        public HashSet<CustomKeyValuePair> Values { get; set; }
        public string MemberType { get; set; }
        public string Operator { get; set; }

    }
}
