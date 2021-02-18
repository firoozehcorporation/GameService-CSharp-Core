// <copyright file="AssetsProvider.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2021 Firoozeh Technology LTD. All Rights Reserved.
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

using System.Threading.Tasks;
using FiroozehGameService.Builder;
using FiroozehGameService.Core.ApiWebRequest;
using FiroozehGameService.Models;
using FiroozehGameService.Models.BasicApi.Providers;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Internal;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Providers
{
    /// <summary>
    ///     Represents Assets Provider Model In Game Service Basic API
    /// </summary>
    internal class AssetsProvider : IAssetsProvider
    {
        private static GameServiceClientConfiguration Configuration => GameService.Configuration;
        private static DownloadManager DownloadManager => GameService.DownloadManager;

        public async Task<AssetInfo> GetAssetInfo(string assetTag)
        {
            if (Configuration == null)
                throw new GameServiceException("You Must Configuration First").LogException(typeof(AssetsProvider),
                    DebugLocation.Internal, "GetAssetInfo");
            if (string.IsNullOrEmpty(assetTag))
                throw new GameServiceException("assetTag Cant Be EmptyOrNull").LogException(typeof(AssetsProvider),
                    DebugLocation.Internal, "GetAssetInfo");
            return await ApiRequest.GetAssetInfo(Configuration.ClientId, assetTag);
        }

        public async Task DownloadAsset(string tag, string dirPath)
        {
            if (Configuration == null)
                throw new GameServiceException("You Must Configuration First").LogException(typeof(AssetsProvider),
                    DebugLocation.Internal, "DownloadAsset(string,string)");
            if (string.IsNullOrEmpty(tag))
                throw new GameServiceException("DownloadTag Cant Be EmptyOrNull").LogException(typeof(AssetsProvider),
                    DebugLocation.Internal, "DownloadAsset(string,string)");
            if (string.IsNullOrEmpty(dirPath))
                throw new GameServiceException("DownloadDirPath Cant Be EmptyOrNull").LogException(
                    typeof(AssetsProvider),
                    DebugLocation.Internal, "DownloadAsset(string,string)");
            await DownloadManager.StartDownload(tag, dirPath);
        }

        public async Task DownloadAsset(string tag)
        {
            if (Configuration == null)
                throw new GameServiceException("You Must Configuration First").LogException(typeof(AssetsProvider),
                    DebugLocation.Internal, "DownloadAsset(string)");
            if (string.IsNullOrEmpty(tag))
                throw new GameServiceException("DownloadTag Cant Be EmptyOrNull").LogException(typeof(AssetsProvider),
                    DebugLocation.Internal, "DownloadAsset(string)");
            await DownloadManager.StartDownload(tag);
        }

        public void DownloadAsset(AssetInfo info, string dirPath)
        {
            if (Configuration == null)
                throw new GameServiceException("You Must Configuration First").LogException(typeof(AssetsProvider),
                    DebugLocation.Internal, "DownloadAsset(info,string)");
            if (info == null)
                throw new GameServiceException("AssetInfo Cant Be Null").LogException(typeof(AssetsProvider),
                    DebugLocation.Internal, "DownloadAsset(info,string)");
            if (string.IsNullOrEmpty(dirPath))
                throw new GameServiceException("DownloadDirPath Cant Be EmptyOrNull").LogException(
                    typeof(AssetsProvider),
                    DebugLocation.Internal, "DownloadAsset(info,string)");
            DownloadManager.StartDownloadWithInfo(info, dirPath);
        }

        public async Task DownloadAsset(AssetInfo info)
        {
            if (Configuration == null)
                throw new GameServiceException("You Must Configuration First").LogException(typeof(AssetsProvider),
                    DebugLocation.Internal, "DownloadAsset(info)");
            if (info == null)
                throw new GameServiceException("AssetInfo Cant Be Null").LogException(typeof(AssetsProvider),
                    DebugLocation.Internal, "DownloadAsset(info)");
            await DownloadManager.StartDownloadWithInfo(info);
        }

        public void CancelAllDownloadAsset()
        {
            if (Configuration == null)
                throw new GameServiceException("You Must Configuration First").LogException(typeof(AssetsProvider),
                    DebugLocation.Internal, "CancelAllDownloadAsset");
            DownloadManager?.CancelAllDownloads();
        }

        public void CancelDownloadAsset(string tag)
        {
            if (Configuration == null)
                throw new GameServiceException("You Must Configuration First").LogException(typeof(AssetsProvider),
                    DebugLocation.Internal, "CancelDownloadAsset(string)");
            if (string.IsNullOrEmpty(tag))
                throw new GameServiceException("Asset Tag Cant Be EmptyOrNull").LogException(typeof(AssetsProvider),
                    DebugLocation.Internal, "CancelDownloadAsset(string)");
            DownloadManager?.CancelDownload(tag);
        }

        public void CancelDownloadAsset(AssetInfo info)
        {
            if (Configuration == null)
                throw new GameServiceException("You Must Configuration First").LogException(typeof(AssetsProvider),
                    DebugLocation.Internal, "CancelDownloadAsset(info)");
            if (info == null)
                throw new GameServiceException("AssetInfo Cant Be Null").LogException(typeof(AssetsProvider),
                    DebugLocation.Internal, "CancelDownloadAsset(info)");
            DownloadManager?.CancelDownload(info);
        }
    }
}