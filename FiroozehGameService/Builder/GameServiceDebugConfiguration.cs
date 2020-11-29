// <copyright file="GameServiceClientConfiguration.cs" company="Firoozeh Technology LTD">
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


using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Internal;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Builder
{
    /// <summary>
    ///     Represents GameServiceDebugConfiguration For Game Service
    /// </summary>
    public class GameServiceDebugConfiguration
    {
        internal readonly bool EnableException;
        internal readonly bool EnableError;
        internal readonly bool EnableDebug;
        internal readonly DebugLocation[] DebugLocations;


        /// <summary>
        /// Set GameServiceDebugConfiguration Values
        /// </summary>
        /// <param name="enableException">Set Debug Exception Enable</param>
        /// <param name="enableError">Set Debug Error Enable</param>
        /// <param name="enableDebug">Set Debug Debug Enable</param>
        /// <param name="debugLocations">Set DebugLocations , If Not Set Debug All Systems</param>
        /// <exception cref="GameServiceException">May GameServiceException Occur</exception>
        public GameServiceDebugConfiguration(bool enableException, bool enableError, bool enableDebug, DebugLocation[] debugLocations)
        {
            EnableException = enableException;
            EnableError = enableError;
            EnableDebug = enableDebug;
            DebugLocations = debugLocations?.Length == 0 ? new[] {DebugLocation.All} : debugLocations;
        }
    }
}