// <copyright file="IAssetsProvider.cs" company="Firoozeh Technology LTD">
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

using System;
using System.Threading.Tasks;
using FiroozehGameService.Models.Internal;

namespace FiroozehGameService.Models.BasicApi.Providers
{
    /// <summary>
    ///     Represents Assets Provider Model In Game Service Basic API
    /// </summary>
    public interface IAssetsProvider
    {
        /// <summary>
        ///     Gets Asset Info With AssetTag
        /// </summary>
        /// <param name="assetTag">(Not NULL)Specifies the Asset tag that Set in Developers Panel.</param>
        Task<AssetInfo> GetAssetInfo(string assetTag);


        /// <summary>
        ///     Download Asset With Tag
        ///     Set DownloadManager Event Handlers To Get Download Status
        /// </summary>
        /// <param name="tag">(Not NULL)Specifies the Asset tag that Set in Developers Panel.</param>
        /// <param name="dirPath">(Not NULL)Specifies the Download File Directory Path </param>
        [Obsolete("This Method is Deprecated,Use DownloadAsset(AssetInfo,string) Instead")]
        Task DownloadAsset(string tag, string dirPath);


        /// <summary>
        ///     Download Asset With Tag
        ///     Set DownloadManager Event Handlers To Get Download Status
        /// </summary>
        /// <param name="tag">(Not NULL)Specifies the Asset tag that Set in Developers Panel.</param>
        [Obsolete("This Method is Deprecated,Use DownloadAsset(AssetInfo) Instead")]
        Task DownloadAsset(string tag);


        /// <summary>
        ///     Download Asset With AssetInfo
        ///     Set DownloadManager Event Handlers To Get Download Status
        /// </summary>
        /// <param name="info">(Not NULL)Specifies the Asset info</param>
        /// <param name="dirPath">(Not NULL)Specifies the Download File Directory Path </param>
        void DownloadAsset(AssetInfo info, string dirPath);


        /// <summary>
        ///     Download Asset With AssetInfo
        ///     Set DownloadManager Event Handlers To Get Download Status
        /// </summary>
        /// <param name="info">(Not NULL)Specifies the Asset Info</param>
        Task DownloadAsset(AssetInfo info);


        /// <summary>
        ///     Cancel All Current Download Assets
        /// </summary>
        void CancelAllDownloadAsset();


        /// <summary>
        ///     Cancel Download Asset With Asset Tag
        /// </summary>
        void CancelDownloadAsset(string tag);


        /// <summary>
        ///     Cancel Download Asset With Asset Info
        /// </summary>
        void CancelDownloadAsset(AssetInfo info);
    }
}