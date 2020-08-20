// <copyright file="DownloadEventHandlers.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2020 Firoozeh Technology LTD. All Rights Reserved.
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
using FiroozehGameService.Models.EventArgs;

/**
* @author Alireza Ghodrati
*/

namespace FiroozehGameService.Handlers
{
    /// <summary>
    ///     Represents DownloadEventHandlers
    /// </summary>
    public class DownloadEventHandlers
    {
        /// <summary>
        ///     Calls When New Download Progress Event Received
        /// </summary>
        public static EventHandler<DownloadProgressArgs> DownloadProgress;

        /// <summary>
        ///     Calls When Download Completed
        /// </summary>
        public static EventHandler<DownloadCompleteArgs> DownloadCompleted;


        /// <summary>
        ///     Calls When Download Cancelled
        /// </summary>
        public static EventHandler<DownloadCancelledArgs> DownloadCancelled;


        /// <summary>
        ///     Calls When An Error Occured
        /// </summary>
        public static EventHandler<DownloadErrorArgs> DownloadError;
    }
}