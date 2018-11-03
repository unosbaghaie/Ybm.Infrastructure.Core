using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;

namespace Ybm.Infrastructure.Core.HttpRequest
{
    public class YbmHttpRequest : IHttpRequest
    {
        /// <summary>
        /// Post request 
        /// </summary>
        /// <param name="httpObject"></param>
        /// <param name="parameters"></param>
        /// <returns></returns>
        public string SendRequest(HttpObject httpObject,IDictionary<string, string> parameters, string contentType = "application/json")
        {

            var httpWebRequest = (HttpWebRequest)WebRequest.Create(httpObject.Url);
            httpWebRequest.ContentType = contentType;
            httpWebRequest.Method =httpObject.Method.ToString();

            if (parameters != null)
                foreach (var p in parameters)
                {
                    httpWebRequest.Headers.Add(p.Key, p.Value);
                }


            string result = null;

            using (var streamWriter = new StreamWriter(httpWebRequest.GetRequestStream()))
            {
                streamWriter.Write(httpObject.Json);
                streamWriter.Flush();
                streamWriter.Close();
            }
            try
            {
                var httpResponse = (HttpWebResponse)httpWebRequest.GetResponse();
                using (var streamReader = new StreamReader(httpResponse.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }
            catch (WebException wex)
            {
                using (var streamReader = new StreamReader(wex.Response.GetResponseStream()))
                {
                    result = streamReader.ReadToEnd();
                }
            }

            return result;
        }
    }
}
