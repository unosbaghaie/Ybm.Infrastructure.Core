using System;
using System.Collections.Generic;
using System.Text;

namespace Ybm.Infrastructure.Core.HttpRequest
{
    public interface IHttpRequest
    {
        string SendRequest(HttpObject httpObject, IDictionary<string, string> parameters, string contentType = "application/json");

    }
}
