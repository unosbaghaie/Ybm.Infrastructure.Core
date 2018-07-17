using System;
using System.Collections.Generic;
using System.Text;

namespace Ybm.Infrastructure.Core.HttpRequest
{
    public class HttpObject
    {
        public string Url { get; set; }
        public EnumHttpMethod Method { get; set; }
        public string Json { get; set; }


    }
}
