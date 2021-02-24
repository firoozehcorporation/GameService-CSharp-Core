// <copyright file="CoreEventHandlers.cs" company="Firoozeh Technology LTD">
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
using System.Net.Sockets;
using FiroozehGameService.Models;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;

/**
* @author Alireza Ghodrati
*/

namespace FiroozehGameService.Handlers
{
    /// <summary>
    ///     Represents CoreEventHandlers In MultiPlayer System
    /// </summary>
    public class CoreEventHandlers
    {
        internal static EventHandler GProtocolConnected;
        internal static EventHandler<object> Authorized;
        internal static EventHandler<string> OnMemberId;
        internal static EventHandler<TcpClient> OnGsTcpClientConnected;
        internal static EventHandler<TcpClient> OnTcpClientConnected;
        internal static EventHandler<GameServiceException> OnGsTcpClientError;
        internal static EventHandler<StartPayload> GsLiveSystemStarted;
        internal static EventHandler<APacket> Ping;
        internal static EventHandler<Packet> OnMirror;
        internal static EventHandler OnLeftDispose;
        internal static EventHandler Dispose;

        /// <summary>
        ///     Calls When Your Game Successfully Connected To GameService
        /// </summary>
        public static EventHandler SuccessfullyLogined;

        /// <summary>
        ///     Calls When An New Error Received From Server
        /// </summary>
        public static EventHandler<ErrorEvent> Error;
    }
}