// <copyright file="DownloadErrorArgs.cs" company="Firoozeh Technology LTD">
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

using FiroozehGameService.Models.Internal;

namespace FiroozehGameService.Models.EventArgs
{
    /// <summary>
    /// Represents DownloadErrorArgs
    /// </summary>
    public class DownloadErrorArgs : System.EventArgs
    {
        /// <summary>
        /// Gets the Asset Info
        /// </summary>
        /// <value>the Asset Info</value>
        public AssetInfo AssetInfo { internal set; get; }
        
        
        /// <summary>
        /// Gets the Save Path
        /// </summary>
        /// <value>the Save Path</value>
        public string SavePath { internal set; get; }
        
        
        /// <summary>
        /// Gets the Error
        /// </summary>
        /// <value>the Error</value>
        public string Error { internal set; get; }
    }
}