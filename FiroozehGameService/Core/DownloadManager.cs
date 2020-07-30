// <copyright file="DownloadManager.cs" company="Firoozeh Technology LTD">
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


using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading.Tasks;
using FiroozehGameService.Builder;
using FiroozehGameService.Core.ApiWebRequest;
using FiroozehGameService.Handlers;
using FiroozehGameService.Models;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.Internal;

/**
* @author Alireza Ghodrati
*/

namespace FiroozehGameService.Core
{
    /// <summary>
    ///     Represents Game Service DownloadManager
    /// </summary>
    internal class DownloadManager
    {
        internal DownloadManager(GameServiceClientConfiguration config)
        {
            _configuration = config;
            _webClients = new Dictionary<string, WebClient>();
        }

        internal async Task StartDownload(string tag)
        {
            try
            {
                var assetInfo = await ApiRequest.GetAssetInfo(_configuration.ClientId, tag);
                await StartDownloadWithInfo(assetInfo);
            }
            catch (Exception e)
            {
                DownloadEventHandlers.DownloadError?.Invoke(this, new DownloadErrorArgs
                {
                    Error = e.Message
                });
            }
        }

        internal async Task StartDownload(string tag, string path)
        {
            try
            {
                var assetInfo = await ApiRequest.GetAssetInfo(_configuration.ClientId, tag);
                StartDownloadWithInfo(assetInfo, path);
            }
            catch (Exception e)
            {
                DownloadEventHandlers.DownloadError?.Invoke(this, new DownloadErrorArgs
                {
                    Error = e.Message
                });
            }
        }


        internal async Task StartDownloadWithInfo(AssetInfo info)
        {
            try
            {
                if (_webClients.ContainsKey(info.AssetInfoData.Name))
                {
                    DownloadEventHandlers.DownloadError?.Invoke(this, new DownloadErrorArgs
                    {
                        AssetInfo = info,
                        Error = "Tag \"" + info.AssetInfoData.Name + "\" is Already in Download Queue!"
                    });
                    return;
                }
                
                var client = new WebClient();
                _webClients.Add(info.AssetInfoData.Name,client);
                // Set Events
                client.DownloadProgressChanged += (s, progress) =>
                {
                    DownloadEventHandlers.DownloadProgress?.Invoke(this, new DownloadProgressArgs
                    {
                        FileTag = info.AssetInfoData.Name,
                        BytesReceived = progress.BytesReceived,
                        TotalBytesToReceive = progress.TotalBytesToReceive,
                        ProgressPercentage = progress.ProgressPercentage
                    });
                };

                client.DownloadDataCompleted += (sender, args) =>
                {
                    if (args.Cancelled)
                    {
                        DownloadEventHandlers.DownloadCancelled?.Invoke(this,new DownloadCancelledArgs {AssetInfo = info});
                        return;
                    }
                    client.Dispose();
                    _webClients.Remove(info.AssetInfoData.Name);
                    DownloadEventHandlers.DownloadCompleted?.Invoke(this, new DownloadCompleteArgs
                    {
                        DownloadedAssetAsBytes = args.Result
                    });
                };

                await client.DownloadDataTaskAsync(info.AssetInfoData.Link);
            }
            catch (Exception e)
            {
                _webClients.Remove(info.AssetInfoData.Name);
                DownloadEventHandlers.DownloadError?.Invoke(this, new DownloadErrorArgs
                {
                    AssetInfo = info,
                    Error = e.Message
                });
            }
        }

        internal void StartDownloadWithInfo(AssetInfo info, string path)
        {
            var completeAddress = path + '/' + info.AssetInfoData.Name;
            try
            {
                if (_webClients.ContainsKey(info.AssetInfoData.Name))
                {
                    DownloadEventHandlers.DownloadError?.Invoke(this, new DownloadErrorArgs
                    {
                        AssetInfo = info,
                        SavePath = completeAddress,
                        Error = "Tag \"" + info.AssetInfoData.Name + "\" is Already in Download Queue!"
                    });
                    return;
                }
                
                var client = new WebClient();
                _webClients.Add(info.AssetInfoData.Name,client);
                // Set Events
                client.DownloadProgressChanged += (s, progress) =>
                {
                    DownloadEventHandlers.DownloadProgress?.Invoke(this, new DownloadProgressArgs
                    {
                        FileTag = info.AssetInfoData.Name,
                        BytesReceived = progress.BytesReceived,
                        TotalBytesToReceive = progress.TotalBytesToReceive,
                        ProgressPercentage = progress.ProgressPercentage
                    });
                };
                
                client.DownloadFileCompleted += (s, e) =>
                {
                    if (e.Cancelled)
                    {
                        RemoveCancelledFile(completeAddress);
                        DownloadEventHandlers.DownloadCancelled?.Invoke(this,new DownloadCancelledArgs {AssetInfo = info , SavePath = path});
                        return;
                    }
                    
                    client.Dispose();
                    _webClients.Remove(info.AssetInfoData.Name);
                    DownloadEventHandlers.DownloadCompleted?.Invoke(this, new DownloadCompleteArgs
                    {
                        DownloadedAssetPath = completeAddress
                    });
                };
                
                client.DownloadFileAsync(new Uri(info.AssetInfoData.Link), completeAddress);
            }
            catch (Exception e)
            {
                _webClients.Remove(info.AssetInfoData.Name);
                DownloadEventHandlers.DownloadError?.Invoke(this, new DownloadErrorArgs
                {
                    AssetInfo = info,
                    SavePath = completeAddress,
                    Error = e.Message
                });
            }
        }


        internal void CancelAllDownloads()
        {
            foreach (var client in _webClients)
            {
                client.Value?.CancelAsync();
                client.Value?.Dispose();
            }
            _webClients.Clear();
        }

        internal void CancelDownload(string tag)
        {
            if(!_webClients.ContainsKey(tag))
                throw new GameServiceException("The Tag \"" + tag + "\" is Not Exist In Download Queue!");
           
            _webClients[tag]?.CancelAsync();
            _webClients[tag]?.Dispose();
            _webClients.Remove(tag);
        }
        
        
        internal void CancelDownload(AssetInfo info)
        {
            var tag = info.AssetInfoData.Name;
            if(!_webClients.ContainsKey(tag))
                throw new GameServiceException("The Tag \"" + tag + "\" is Not Exist In Download Queue!");
           
            _webClients[tag]?.CancelAsync();
            _webClients[tag]?.Dispose();
            _webClients.Remove(tag);
        }


        private static void RemoveCancelledFile(string fullPath) => File.Delete(fullPath);
        

        #region DownloadRegion

        private readonly GameServiceClientConfiguration _configuration;
        private readonly Dictionary<string, WebClient> _webClients;

        #endregion
    }
}