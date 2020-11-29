// <copyright file="Debug.cs" company="Firoozeh Technology LTD">
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


/**
* @author Alireza Ghodrati
*/

using System;
using FiroozehGameService.Models.Enums;

namespace FiroozehGameService.Models.EventArgs
{
    /// <summary>
    /// Represents Game Service Debug Class
    /// </summary>
    public class Debug
    {
        /// <summary>
        /// the Debug Type (Normal or Error)
        /// </summary>
        public LogType LogTypeType;

        /// <summary>
        /// the Debug Location
        /// </summary>
        public DebugLocation Where;
        

        /// <summary>
        /// the Debug Data
        /// </summary>
        public string Data;
        
        
        /// <summary>
        /// the Debug Exception
        /// NullAble, Only Available When  Where is Exception
        /// </summary>
        public Exception Exception;
    }
}