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
using System.Threading.Tasks;
using FiroozehGameService.Handlers;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Core.Socket.ClientHelper
{
    /// <summary>
    ///     TcpClientWithTimeout is used to open a TcpClient connection, with a
    ///     user definable connection timeout in milliseconds (1000=1second)
    ///     Use it like this:
    ///     TcpClient connection = new TcpClientWithTimeout('127.0.0.1',80,1000).Connect();
    /// </summary>
    internal class TcpClientWithTimeout
    {
        private readonly string _hostname;
        private readonly int _port;
        private readonly int _timeoutWaitMilliseconds;
        private const int TimeoutThreadWaitMilliseconds = 5000;
        private bool _connected;
        private TcpClient _connection;
        private Exception _exception;


        internal TcpClientWithTimeout(string hostname, int port,int timeoutWaitMilliseconds)
        {
            _hostname = hostname;
            _port = port;
            _timeoutWaitMilliseconds = timeoutWaitMilliseconds;
        }

        internal async Task Connect(GSLiveType type)
        {
            _connected = false;
            _exception = null;

            DebugUtil.LogNormal<TcpClientWithTimeout>(DebugLocation.Internal,"BeginConnect","Wait " + _timeoutWaitMilliseconds + " Before Connect");
               
            await Task.Delay(_timeoutWaitMilliseconds);

            var thread = new Thread(BeginConnect) {IsBackground = true};
            thread.Start();

            thread.Join(TimeoutThreadWaitMilliseconds);
            
            if (_connected)
            {
                thread.Abort();
                CoreEventHandlers.OnTcpClientConnected?.Invoke(type,_connection);
                return;
            }

            if (_exception != null)
            {
                thread.Abort();
                CoreEventHandlers.OnGsTcpClientError?.Invoke(type, new GameServiceException(_exception.Message));
                return;
            }
            
            thread.Abort();
            CoreEventHandlers.OnGsTcpClientError?.Invoke(type,new GameServiceException( $"TcpClient connection to {_hostname}:{_port} timed out"));
        }

        private void BeginConnect()
        {
            try
            {
                DebugUtil.LogNormal<TcpClientWithTimeout>(DebugLocation.Internal,"BeginConnect","Connect To " + _hostname);
                
                _connection = new TcpClient(_hostname,_port);
                _connected = true;
            }
            catch (Exception ex)
            {
                _exception = ex;
            }
        }
    }
}