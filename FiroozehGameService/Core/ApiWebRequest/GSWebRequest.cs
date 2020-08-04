using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.ApiWebRequest
{
    internal static class GsWebRequest
    {
        private static HttpRequestObserver Observer = new HttpRequestObserver();
        private static HttpClient Client = new HttpClient();
        private static readonly string UserAgent = "UnitySDK-" + GameService.Version();

        internal static async Task<HttpResponseMessage> Get(string url, Dictionary<string, string> headers = null)
        {
            return await DoRequest(url, GsWebRequestMethod.Get, null, headers);
        }

        internal static async Task<HttpResponseMessage> Put(string url, string body,
            Dictionary<string, string> headers = null)
        {
            return await DoRequest(url, GsWebRequestMethod.Put, body, headers);
        }

        internal static async Task<HttpResponseMessage> Post(string url, string body = null,
            Dictionary<string, string> headers = null)
        {
            return await DoRequest(url, GsWebRequestMethod.Post, body, headers);
        }

        internal static async Task<HttpResponseMessage> Delete(string url, Dictionary<string, string> headers = null)
        {
            return await DoRequest(url, GsWebRequestMethod.Delete, null, headers);
        }

        internal static async Task<HttpResponseMessage> DoMultiPartPost(string url, byte[] data,
            Dictionary<string, string> headers = null)
        {
            var httpClient = Init(headers);
            var dataContent = new MultipartFormDataContent
            {
                {new ByteArrayContent(data), "file", "file"}
            };
            return await httpClient.PostAsync(url, dataContent);
        }

        private static HttpClient Init(Dictionary<string, string> headers = null)
        {
            if (headers == null) return Client;
            Client.DefaultRequestHeaders.Clear();
            foreach (var header in headers)
                Client.DefaultRequestHeaders.Add(header.Key, header.Value);

            Client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            return Client;
        }


        internal static void Dispose()
        {
            Observer?.Dispose();
            Client?.Dispose();
        }

        private static void CheckDisposed()
        {
            if (!Observer.IsDisposed) return;
            Observer = new HttpRequestObserver();
            Client = new HttpClient();
        }


        private static async Task<HttpResponseMessage> DoRequest(string url,
            GsWebRequestMethod method = GsWebRequestMethod.Get, string body = null,
            Dictionary<string, string> headers = null)
        {

            CheckDisposed();
            if(!Observer.Increase())
                throw new GameServiceException("Too Many Requests, You Can Send " + HttpRequestObserver.MaxRequest + " Requests Per " + HttpRequestObserver.Reset + " Secs");
           
            var httpClient = Init(headers);
            StringContent content = null;
            if (body != null) content = new StringContent(body, Encoding.UTF8, "application/json");

            switch (method)
            {
                case GsWebRequestMethod.Get:
                    return await httpClient.GetAsync(url);
                case GsWebRequestMethod.Post:
                    return await httpClient.PostAsync(url, content);
                case GsWebRequestMethod.Put:
                    return await httpClient.PutAsync(url, content);
                case GsWebRequestMethod.Delete:
                    return await httpClient.DeleteAsync(url);
                default:
                    throw new GameServiceException();
            }
        }
    }
}