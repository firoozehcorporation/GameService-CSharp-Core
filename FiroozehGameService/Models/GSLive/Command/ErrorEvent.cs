// <copyright file="ErrorEvent.cs" company="Firoozeh Technology LTD">
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


using FiroozehGameService.Models.Enums.GSLive;

/**
* @author Alireza Ghodrati
*/


namespace FiroozehGameService.Models.GSLive.Command
{
    /// <summary>
    /// Represents ErrorEvent When Error Happened in GSLive
    /// </summary>
    public class ErrorEvent
    {
        /// <summary>
        /// Gets the Error Happened Type
        /// </summary>
        /// <value>Error Happened Type</value>
        public GSLiveType Type { internal set; get; }
        
        /// <summary>
        /// Gets the Error Text
        /// </summary>
        /// <value>the Error Text</value>
        public string Error { internal set; get; }
    }
}