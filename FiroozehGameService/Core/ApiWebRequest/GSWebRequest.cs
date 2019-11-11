using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FiroozehGameService.Core.ApiWebRequest
{
    internal class GsWebRequest
    {
        public async Task<HttpWebResponse> Get(string url,Dictionary<string,string> headers = default)
        {
            var webRequest = Init(url, headers);
            webRequest.ContentType = "application/json";

           return (HttpWebResponse)await webRequest.GetResponseAsync();
        }
        
        public async Task<HttpWebResponse> Put(string url,string body,Dictionary<string,string> headers = default)
        {
            var webRequest = Init(url, headers);
            webRequest.Method = "PUT";
            webRequest.ContentType = "application/json";
            
            var encodedBody = Encoding.UTF8.GetBytes(body);
            await webRequest.GetRequestStream().WriteAsync(encodedBody, 0, encodedBody.Length);
           
            return (HttpWebResponse)await webRequest.GetResponseAsync();
        }
        
        public async Task<HttpWebResponse> Post(string url,string body,Dictionary<string,string> headers = default)
        {
            var webRequest = Init(url, headers);
            webRequest.Method = "POST";
            webRequest.ContentType = "application/json";

            var encodedBody = Encoding.UTF8.GetBytes(body);
            await webRequest.GetRequestStream().WriteAsync(encodedBody, 0, encodedBody.Length);
           
            return (HttpWebResponse)await webRequest.GetResponseAsync();
        }
        
        public async Task<HttpWebResponse> Delete(string url,Dictionary<string,string> headers = default)
        {
            var webRequest = Init(url, headers);
            webRequest.Method = "DELETE";
            webRequest.ContentType = "application/json";

           
            return (HttpWebResponse)await webRequest.GetResponseAsync();
        }

        private static HttpWebRequest Init(string url,Dictionary<string,string> headers = default)
        {
            var webRequest = (HttpWebRequest) WebRequest.Create(url);

            if (headers == null) return webRequest;
            foreach (var header in headers)
                webRequest.Headers.Add(header.Key,header.Value);

            return webRequest;
        }
    }
}