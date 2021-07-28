// <copyright file="PushEventBufferType.cs" company="Firoozeh Technology LTD">
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

namespace FiroozehGameService.Models.Enums.GSLive
{
    /// <summary>
    ///     Represents Push Event Buffer Type Class in GameService
    /// </summary>
    [Serializable]
    public enum PushEventBufferType
    {
        /// <summary>
        ///     No Buffering Push Event Buffer Type
        /// </summary>
        NoBuffering = 1,

        /// <summary>
        ///     With Buffering Push Event Buffer Type
        /// </summary>
        WithBuffering = 2
    }
}