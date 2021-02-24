// <copyright file="DownloadCompleteArgs.cs" company="Firoozeh Technology LTD">
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

using FiroozehGameService.Core;

namespace FiroozehGameService.Models.EventArgs
{
    /// <summary>
    ///     Represents DownloadCompleteArgs
    /// </summary>
    public class DownloadCompleteArgs
    {
        /// <summary>
        ///     Gets the Downloaded Asset As Bytes
        ///     Notes : Only Available When Use this Function <see cref="GameService.Assets.DownloadAsset(string)" />
        /// </summary>
        /// <value>the Downloaded Assets As Bytes</value>
        public byte[] DownloadedAssetAsBytes { internal set; get; }


        /// <summary>
        ///     Gets the Downloaded Asset Path
        ///     Notes : Only Available When Use this Function <see cref="GameService.Assets.DownloadAsset(string,string)" />
        /// </summary>
        /// <value>the Downloaded Asset Path </value>
        public string DownloadedAssetPath { internal set; get; }
    }
}