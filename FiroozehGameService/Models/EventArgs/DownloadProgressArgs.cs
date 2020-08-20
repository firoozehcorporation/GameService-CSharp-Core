// <copyright file="DownloadProgressArgs.cs" company="Firoozeh Technology LTD">
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

namespace FiroozehGameService.Models.EventArgs
{
    /// <summary>
    ///     Represents DownloadProgressArgs
    /// </summary>
    public class DownloadProgressArgs : System.EventArgs
    {
        /// <summary>
        ///     Gets the File Name.
        /// </summary>
        /// <value>the File Name</value>
        public string FileTag { internal set; get; }

        /// <summary>
        ///     Gets Downloaded File BytesReceived
        /// </summary>
        /// <value>Downloaded File BytesReceived</value>
        public long BytesReceived { internal set; get; }

        /// <summary>
        ///     Gets Download File TotalBytesToReceive
        /// </summary>
        /// <value>Download File TotalBytesToReceive</value>
        public long TotalBytesToReceive { internal set; get; }

        /// <summary>
        ///     Gets Downloaded File ProgressPercentage
        /// </summary>
        /// <value>Downloaded File ProgressPercentage</value>
        public int ProgressPercentage { internal set; get; }
    }
}