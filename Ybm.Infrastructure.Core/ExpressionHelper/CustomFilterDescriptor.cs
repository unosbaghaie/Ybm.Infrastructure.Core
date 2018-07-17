using System;
using System.Collections.Generic;
using System.Text;

namespace Ybm.Infrastructure.Core.ExpressionHelper
{
    public class CustomFilterDescriptor
    {

        public object ConvertedValue { get; set; }
        public string Member { get; set; }
        public Type MemberType { get; set; }
        public FilterOperator Operator { get; set; }
        public object Value { get; set; }

        public string Name { get; set; }
    }

    
}
