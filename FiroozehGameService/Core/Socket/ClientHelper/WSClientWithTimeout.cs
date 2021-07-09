// <copyright file="WsClientWithTimeout.cs" company="Firoozeh Technology LTD">
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
using System.Threading;
using FiroozehGameService.Handlers;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Utils;
using WebSocketSharp;

namespace FiroozehGameService.Core.Socket.ClientHelper
{
    internal class WsClientWithTimeout
    {
        private const int TimeoutThreadWaitMilliseconds = 5000;
        private readonly string _hostname;
        private readonly int _port;
        private readonly int _timeoutWaitMilliseconds;
        private bool _connected;
        private WebSocket _connection;
        private Exception _exception;


        internal WsClientWithTimeout(string hostname, int port, int timeoutWaitMilliseconds)
        {
            _hostname = hostname;
            _port = port;
            _timeoutWaitMilliseconds = timeoutWaitMilliseconds;
        }

        internal void Connect(GSLiveType type, WebSocket webSocket)
        {
            _connection = webSocket;
            _connected = false;
            _exception = null;

            Thread.Sleep(_timeoutWaitMilliseconds);

            var thread = new Thread(BeginConnect)
            {
                Priority = ThreadPriority.Highest,
                IsBackground = true
            };
            thread.Start();

            thread.Join(TimeoutThreadWaitMilliseconds);

            if (_connected)
            {
                thread.Interrupt();

                if (type == GSLiveType.Command)
                    CommandEventHandlers.CommandClientConnected?.Invoke(type, new GTcpConnection
                    {
                        IsWs = true, WebSocket = _connection
                    });
                else
                    TurnBasedEventHandlers.TurnBasedClientConnected?.Invoke(type, new GTcpConnection
                    {
                        IsWs = true, WebSocket = _connection
                    });

                return;
            }

            if (_exception != null)
            {
                thread.Interrupt();

                if (type == GSLiveType.Command)
                    CommandEventHandlers.GsCommandClientError?.Invoke(null,
                        new GameServiceException(_exception.Message));
                else
                    TurnBasedEventHandlers.GsTurnBasedClientError?.Invoke(null,
                        new GameServiceException(_exception.Message));

                return;
            }

            thread.Interrupt();

            if (type == GSLiveType.Command)
                CommandEventHandlers.GsCommandClientError?.Invoke(null,
                    new GameServiceException($"WS connection to {_hostname}:{_port} timed out"));
            else
                TurnBasedEventHandlers.GsTurnBasedClientError?.Invoke(null,
                    new GameServiceException($"WS connection to {_hostname}:{_port} timed out"));
        }

        private void BeginConnect()
        {
            try
            {
                DebugUtil.LogNormal<WsClientWithTimeout>(DebugLocation.Internal, "BeginConnect",
                    $"Connecting To {_hostname}:{_port} ...");

                _connection?.Connect();
                _connected = true;
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }
    }
}