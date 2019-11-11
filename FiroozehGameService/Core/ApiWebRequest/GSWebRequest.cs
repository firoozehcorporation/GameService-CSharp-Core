using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FiroozehGameService.Core.ApiWebRequest
{
    internal class GsWebRequest
    {
        internal static async Task<HttpWebResponse> Get(string url,Dictionary<string,string> headers = null)
        {
           var webRequest = Init(url, headers);
           return (HttpWebResponse)await webRequest.GetResponseAsync();
        }
        
        internal static async Task<HttpWebResponse> Put(string url,string body,Dictionary<string,string> headers = null)
        {
            var webRequest = Init(url, headers);
            webRequest.Method = "PUT";
            
            var encodedBody = Encoding.UTF8.GetBytes(body);
            await webRequest.GetRequestStream().WriteAsync(encodedBody, 0, encodedBody.Length);
           
            return (HttpWebResponse)await webRequest.GetResponseAsync();
        }
        
        internal static async Task<HttpWebResponse> Post(string url,string body = null,Dictionary<string,string> headers = null)
        {
            var webRequest = Init(url, headers);
            webRequest.Method = "POST";

            var encodedBody = Encoding.UTF8.GetBytes(body);
            await webRequest.GetRequestStream().WriteAsync(encodedBody, 0, encodedBody.Length);
           
            return (HttpWebResponse)await webRequest.GetResponseAsync();
        }
        
        internal static async Task<HttpWebResponse> Delete(string url,Dictionary<string,string> headers = null)
        {
            var webRequest = Init(url, headers);
            webRequest.Method = "DELETE";

            return (HttpWebResponse)await webRequest.GetResponseAsync();
        }

        private static HttpWebRequest Init(string url,Dictionary<string,string> headers = null)
        {
            var webRequest = (HttpWebRequest) WebRequest.Create(url);
            webRequest.ContentType = "application/json";

            if (headers == null) return webRequest;
            foreach (var header in headers)
                webRequest.Headers.Add(header.Key, header.Value);

            return webRequest;
        }
    }
}