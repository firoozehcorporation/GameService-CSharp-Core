// <copyright file="TcpClientWithTimeout.cs" company="Firoozeh Technology LTD">
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
using System.Net.Sockets;
using System.Threading;
using FiroozehGameService.Handlers;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Socket.ClientHelper
{
    internal class TcpClientWithTimeout
    {
        private const int TimeoutThreadWaitMilliseconds = 5000;
        private const short TcpTimeout = 2000;
        private const int BufferCapacity = 8192;
        private readonly string _hostname;
        private readonly int _port;
        private readonly int _timeoutWaitMilliseconds;
        private bool _connected;
        private TcpClient _connection;
        private Exception _exception;


        internal TcpClientWithTimeout(string hostname, int port, int timeoutWaitMilliseconds)
        {
            _hostname = hostname;
            _port = port;
            _timeoutWaitMilliseconds = timeoutWaitMilliseconds;
        }

        internal void Connect(GSLiveType type)
        {
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
                        IsWs = false, TcpClient = _connection
                    });
                else
                    TurnBasedEventHandlers.TurnBasedClientConnected?.Invoke(type, new GTcpConnection
                    {
                        IsWs = false, TcpClient = _connection
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
                    new GameServiceException("TcpClient connection timed out"));
            else
                TurnBasedEventHandlers.GsTurnBasedClientError?.Invoke(null,
                    new GameServiceException("TcpClient connection timed out"));
        }

        private void BeginConnect()
        {
            try
            {
                DebugUtil.LogNormal<TcpClientWithTimeout>(DebugLocation.Internal, "BeginConnect",
                    "Connecting To TCP Edge...");

                _connection = new TcpClient
                {
                    ReceiveTimeout = TcpTimeout,
                    SendTimeout = TcpTimeout,
                    ReceiveBufferSize = BufferCapacity,
                    SendBufferSize = BufferCapacity
                };

                _connection.Connect(_hostname, _port);

                _connected = true;
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }
    }
}