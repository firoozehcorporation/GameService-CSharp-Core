// <copyright file="GsWebRequest.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2019 Firoozeh Technology LTD. All Rights Reserved.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>


/**
* @author Alireza Ghodrati
*/


using System;
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
        private static HttpRequestObserver _observer = new HttpRequestObserver();
        private static HttpClient _client = new HttpClient {Timeout = TimeSpan.FromSeconds(5)};
        private static readonly string UserAgent = "UnitySDK-" + GameService.Version();

        internal static async Task<HttpResponseMessage> Get(string url, Dictionary<string, string> headers = null)
        {
            return await DoRequest(url, GsWebRequestMethod.Get, null, headers);
        }

        internal static async Task<HttpResponseMessage> Put(string url, string body = null,
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
            if (headers == null) return _client;
            _client.DefaultRequestHeaders.Clear();
            foreach (var header in headers)
                _client.DefaultRequestHeaders.Add(header.Key, header.Value);

            _client.DefaultRequestHeaders.Add("User-Agent", UserAgent);
            return _client;
        }


        internal static void Dispose()
        {
            _observer?.Dispose();
            _client?.Dispose();
        }

        private static void CheckDisposed()
        {
            if (!_observer.IsDisposed) return;
            _observer = new HttpRequestObserver();
            _client = new HttpClient();
        }


        private static async Task<HttpResponseMessage> DoRequest(string url,
            GsWebRequestMethod method = GsWebRequestMethod.Get, string body = null,
            Dictionary<string, string> headers = null)
        {
            CheckDisposed();
            if (!_observer.Increase())
                throw new GameServiceException("Too Many Requests, You Can Send " + HttpRequestObserver.MaxRequest +
                                               " Requests Per " + HttpRequestObserver.Reset + " Secs")
                    .LogException(typeof(GsWebRequest), DebugLocation.Http, "DoRequest");

            var httpClient = Init(headers);
            StringContent content = null;
            if (body != null) content = new StringContent(body, Encoding.UTF8, "application/json");

            DebugUtil.LogNormal(typeof(GsWebRequest), DebugLocation.Http, "DoRequest", "GSWebRequest -> URL: " + url);

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