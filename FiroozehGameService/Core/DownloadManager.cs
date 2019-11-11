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




/**
* @author Alireza Ghodrati
*/

using System;
using System.Net;
using System.Threading;
using System.Threading.Tasks;
using FiroozehGameService.Core.ApiWebRequest;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.EventArgs;

namespace FiroozehGameService.Core
{
    public class DownloadManager
    {
        public event EventHandler<DownloadProgressArgs> DownloadProgress;
        public event EventHandler DownloadComplete;
        public event EventHandler<ErrorArg> DownloadError;
        public event EventHandler DownloadCanceled;
        
        private CancellationTokenSource DownloadStreamReaderToken = new CancellationTokenSource();

        private Builder.GameServiceClientConfiguration configuration;
        private bool _enableDownload;
      
        public DownloadManager(Builder.GameServiceClientConfiguration config)
        {
            configuration = config;
        }

        public async Task StartDownload(string tag , string path)
        {
            var download = await ApiRequest.GetDataPackInfo(configuration.ClientId, tag);
            var webRequest = new GsWebRequest();
            var buffer = new byte[1024*10];
            var totalReadBytes = 0;
            var readBytes = 0;
            _enableDownload = true;
            
           using (var response = await GsWebRequest.Get(download.Data.Url))
           using (var stream = response.GetResponseStream())
           {
               while (totalReadBytes != response.ContentLength && _enableDownload)
               {
                   while (readBytes != buffer.Length && _enableDownload)
                   {
                       try
                       {
                           readBytes += await stream.ReadAsync(buffer, buffer.Length-readBytes, 
                               buffer.Length, DownloadStreamReaderToken.Token);
                       }
                       catch(OperationCanceledException)
                       {break;}
                   }
                   totalReadBytes += readBytes;
                   readBytes = 0;
                   
                   if(_enableDownload)
                       DownloadProgress?.Invoke(this,new DownloadProgressArgs
                       {
                           Data = buffer,
                           ProgessSize = totalReadBytes,
                           TotalSize =response.ContentLength
                       });  
               }
               
               if(_enableDownload)
                    DownloadComplete?.Invoke(this,EventArgs.Empty);   
               else
                   DownloadCanceled?.Invoke(this,EventArgs.Empty);
               
               _enableDownload = false;
           }
        }

        public void StopDownload()
        {
            _enableDownload = false;
            DownloadStreamReaderToken.Cancel(true);
        }
    }
}