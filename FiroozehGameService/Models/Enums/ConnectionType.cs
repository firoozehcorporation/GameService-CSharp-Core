// <copyright file="ConnectionType.cs" company="Firoozeh Technology LTD">
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


namespace FiroozehGameService.Models.Enums
{
    /// <summary>
    ///     Represents ConnectionType Class in GameService
    /// </summary>
    public enum ConnectionType
    {
        /// <summary>
        ///     Connection Not Set , that's Set Automatically By Server
        /// </summary>
        NotSet,

        /// <summary>
        ///     Native Connection
        /// </summary>
        Native,

        /// <summary>
        ///     WebSocket Connection
        /// </summary>
        WebSocket
    }
}