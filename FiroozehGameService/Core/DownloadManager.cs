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
using System.Net;
using System.Threading.Tasks;
using FiroozehGameService.Core.ApiWebRequest;
using FiroozehGameService.Handlers;
using FiroozehGameService.Models.EventArgs;

/**
* @author Alireza Ghodrati
*/

namespace FiroozehGameService.Core
{
    public class DownloadManager
    {
        #region DownloadRegion
        private readonly Builder.GameServiceClientConfiguration _configuration;
        private readonly WebClient _client = new WebClient();
        #endregion
       
        public DownloadManager(Builder.GameServiceClientConfiguration config)
        {
            _configuration = config;
        }

        internal async Task StartDownload(string tag)
        {
            try
            {
                var download = await ApiRequest.GetDataPackInfo(_configuration.ClientId, tag);
                // Set Events
                _client.DownloadProgressChanged += (s, progress) =>
                    {
                        DownloadEventHandlers.DownloadProgress?.Invoke(this,new DownloadProgressArgs
                        {
                            FileTag = tag,
                            BytesReceived = progress.BytesReceived,
                            TotalBytesToReceive = progress.TotalBytesToReceive,
                            ProgressPercentage = progress.ProgressPercentage
                        });
                    };
                _client.DownloadDataCompleted += (sender, args) =>
                    {
                        DownloadEventHandlers.DownloadCompleted?.Invoke(this,new DownloadCompleteArgs
                        {
                            DownloadedAssetAsBytes = args.Result
                        });
                    }; 
                
                await _client.DownloadDataTaskAsync(download.Data.Link);
                
            }
            catch (Exception e)
            {
                DownloadEventHandlers.DownloadError?.Invoke(this,new ErrorArg
                {
                    Error = e.Message
                });
            }
        }
       
        internal async Task StartDownload(string tag,string path)
        {
            var completeAddress = path + '/' + tag;
            try
            {
                var download = await ApiRequest.GetDataPackInfo(_configuration.ClientId, tag);
                    // Set Events
                _client.DownloadProgressChanged += (s, progress) =>
                        DownloadEventHandlers.DownloadProgress?.Invoke(this,new DownloadProgressArgs
                        {
                            FileTag = tag,
                            BytesReceived = progress.BytesReceived,
                            TotalBytesToReceive = progress.TotalBytesToReceive,
                            ProgressPercentage = progress.ProgressPercentage
                        });
                    

                _client.DownloadFileCompleted += (s, e) =>
                        DownloadEventHandlers.DownloadCompleted?.Invoke(this,new DownloadCompleteArgs
                        {
                            DownloadedAssetPath = completeAddress
                        });

                _client.DownloadFileAsync(new Uri(download.Data.Link),completeAddress);
            }
            catch (Exception e)
            {
                DownloadEventHandlers.DownloadError?.Invoke(this,new ErrorArg
                {
                    Error = e.Message
                });
            }
        }


        internal void StopAllDownloads()
        {
            _client.CancelAsync();
        }
    }
}