using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums;

namespace FiroozehGameService.Core.ApiWebRequest
{
    internal class GsWebRequest
    {
        private static readonly HttpClient Client = new HttpClient();
       
        internal static async Task<HttpResponseMessage> Get(string url,Dictionary<string,string> headers = null)
        {
            return await DoRequest(url,GsWebRequestMethod.Get,null,headers);
        }
        
        internal static async Task<HttpResponseMessage> Put(string url,string body,Dictionary<string,string> headers = null)
        {
            return await DoRequest(url,GsWebRequestMethod.Put, body, headers);
        }
        
        internal static async Task<HttpResponseMessage> Post(string url,string body = null,Dictionary<string,string> headers = null)
        {
            return await DoRequest(url, GsWebRequestMethod.Post, body, headers);
        }
        
        internal static async Task<HttpResponseMessage> Delete(string url,Dictionary<string,string> headers = null)
        {
            return await DoRequest(url, GsWebRequestMethod.Delete, null, headers);
        }

        private static HttpClient Init(Dictionary<string,string> headers = null)
        {
            if (headers == null) return Client;
            Client.DefaultRequestHeaders.Clear();
            foreach (var header in headers)
                Client.DefaultRequestHeaders.Add(header.Key, header.Value);
            return Client;
        }

              
        private static async Task<HttpResponseMessage> DoRequest(string url, GsWebRequestMethod method= GsWebRequestMethod.Get, string body = null,
            Dictionary<string, string> headers = null)
        {
            var httpClient = Init(headers);

            StringContent content = null;
            if(body != null) content = new StringContent(body, Encoding.UTF8, "application/json");
          
            switch (method)
                {
                    case GsWebRequestMethod.Get:
                        return await httpClient.GetAsync(url);
                    case GsWebRequestMethod.Post:
                        return await httpClient.PostAsync(url,content);
                    case GsWebRequestMethod.Put:
                        return await httpClient.PutAsync(url,content);
                    case GsWebRequestMethod.Delete:
                        return await httpClient.DeleteAsync(url);
                    default:
                        throw new GameServiceException();
                }
        }
    }
}