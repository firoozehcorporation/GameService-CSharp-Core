using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace FiroozehGameService.Core.ApiWebRequest
{
    internal class GsWebRequest
    {
        public async Task<Stream> Get(string url,Dictionary<string,string> headers = default)
        {
            var webRequest = Init(url, headers);
            using (var response = await webRequest.GetResponseAsync())
                return response.GetResponseStream();
        }
        
        public async Task<Stream> Put(string url,string body,Dictionary<string,string> headers = default)
        {
            var webRequest = Init(url, headers);
            webRequest.Method = "PUT";
            var encodedBody = Encoding.UTF8.GetBytes(body);
            await webRequest.GetRequestStream().WriteAsync(encodedBody, 0, encodedBody.Length);
           
            using (var response = await webRequest.GetResponseAsync())
                return response.GetResponseStream();
        }
        
        public async Task<Stream> Post(string url,string body,Dictionary<string,string> headers = default)
        {
            var webRequest = Init(url, headers);
            webRequest.Method = "POST";
            var encodedBody = Encoding.UTF8.GetBytes(body);
            await webRequest.GetRequestStream().WriteAsync(encodedBody, 0, encodedBody.Length);
           
            using (var response = await webRequest.GetResponseAsync())
                return response.GetResponseStream();
        }
        
        public async Task<Stream> Delete(string url,Dictionary<string,string> headers = default)
        {
            var webRequest = Init(url, headers);
            webRequest.Method = "DELETE";
           
            using (var response = await webRequest.GetResponseAsync())
                return response.GetResponseStream();
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