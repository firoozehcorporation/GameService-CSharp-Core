// <copyright file="TurnBasedHandler.cs" company="Firoozeh Technology LTD">
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

using System;
using System.Collections.Generic;
using System.Net.Sockets;
using System.Threading;
using System.Threading.Tasks;
using FiroozehGameService.Core;
using FiroozehGameService.Core.Socket;
using FiroozehGameService.Handlers.TurnBased.RequestHandlers;
using FiroozehGameService.Handlers.TurnBased.ResponseHandlers;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Handlers.TurnBased
{
    internal class TurnBasedHandler : IDisposable
    {
        public TurnBasedHandler(StartPayload payload)
        {
            CurrentRoom = payload.Room;
            _tcpClient = new GsTcpClient(payload.Area);
            _tcpClient.DataReceived += OnDataReceived;

            _cancellationToken = new CancellationTokenSource();
            _observer = new GsLiveSystemObserver(GSLiveType.TurnBased);
            _isDisposed = false;

            // Set Internal Event Handlers
            CoreEventHandlers.Ping += OnPing;
            CoreEventHandlers.Authorized += OnAuth;
            CoreEventHandlers.OnGsTcpClientConnected += OnGsTcpClientConnected;
            CoreEventHandlers.OnGsTcpClientError += OnGsTcpClientError;


            InitRequestMessageHandlers();
            InitResponseMessageHandlers();

            LogUtil.Log(this, "TurnBasedHandler Init");
            DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased,"Constructor","TurnBasedHandler Init");
        }

        private async void KeepAliveChecker(object sender, EventArgs e)
        {
            DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased,"KeepAliveChecker","TurnBasedHandler -> Send KeepAlive Done");
            await RequestAsync(KeepAliveHandler.Signature, null, true,true);
        }

        private async void OnGsTcpClientError(object sender, GameServiceException exception)
        {
            if((GSLiveType) sender != GSLiveType.TurnBased) return;
            if (_isDisposed) return;

            LogUtil.LogError(this, "TurnBasedHandler -> OnGsTcpClientError : " + exception);
            exception.LogException<TurnBasedHandler>(DebugLocation.TurnBased,"OnGsTcpClientError");

            _retryConnectCounter++;
            
            if (_retryConnectCounter >= TurnBasedConst.MaxRetryConnect)
            {
                LogUtil.Log(this, "TurnBasedHandler Reached to MaxRetryConnect , so dispose TurnBased...");
                DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased,"OnGsTcpClientError","TurnBasedHandler Reached to MaxRetryConnect , so dispose TurnBased...");
                Dispose();
                return;
            }
            
            LogUtil.Log(this, "TurnBasedHandler reconnect Retry " + _retryConnectCounter + " , Wait to Connect...");
            DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased,"OnGsTcpClientError","TurnBasedHandler reconnect Retry " + _retryConnectCounter + " , Wait to Connect...");

            await Init();
        }

        private async void OnGsTcpClientConnected(object sender, TcpClient e)
        {
            if((GSLiveType) sender != GSLiveType.TurnBased) return;

            LogUtil.Log(this, "TurnBasedHandler -> Connected,Waiting for Handshakes...");
            DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased,"OnGsTcpClientConnected","TurnBasedHandler -> Connected,Waiting for Handshakes...");

            
            Task.Run(async () => { await _tcpClient.StartReceiving(); }, _cancellationToken.Token);
            await RequestAsync(AuthorizationHandler.Signature, isCritical: true);
         
            _retryConnectCounter = 0;
            
            LogUtil.Log(this, "TurnBasedHandler Init done"); 
            DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased,"OnGsTcpClientConnected","TurnBasedHandler Init done");
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                LogUtil.Log(this, "TurnBasedHandler Already Disposed!");
                DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased,"Dispose","TurnBasedHandler Already Disposed");
                return;
            }

            _retryConnectCounter = 0;
            _isDisposed = true;
            _tcpClient?.StopReceiving();
            _observer?.Dispose();
            _cancellationToken?.Cancel(true);
            CoreEventHandlers.Dispose?.Invoke(this, null);
            
            LogUtil.Log(this, "TurnBasedHandler Dispose");
            DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased,"Dispose","TurnBasedHandler Dispose Done");
        }

        private static void OnAuth(object sender, object playerHash)
        {
            if (sender.GetType() != typeof(AuthResponseHandler)) return;
            PlayerHash = (string) playerHash;
            
            LogUtil.Log(null, "TurnBasedHandler OnAuth");
            DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased,"OnAuth","TurnBasedHandler Auth Done");
        }

        private async void OnPing(object sender, APacket packet)
        {
            if (sender.GetType() != typeof(PingResponseHandler)) return;
            await RequestAsync(PingPongHandler.Signature, isCritical: true);
            
            LogUtil.Log(null, "TurnBasedHandler OnPing");
            DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased,"OnPing","TurnBasedHandler Ping Called");
        }

        private void InitRequestMessageHandlers()
        {
            // this implementation not working on IL2CPP

            /*var baseInterface = typeof(IRequestHandler);
            var subclassTypes = Assembly
                .GetAssembly(baseInterface)
                .GetTypes()
                .Where(t => t.GetInterfaces().Contains(baseInterface) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in subclassTypes)
            {
                var p = (string) type.GetProperty("Signature", BindingFlags.Public | BindingFlags.Static)
                    .GetValue(null);
                _requestHandlers.Add(p, (IRequestHandler) Activator.CreateInstance(type));
            }
            */

            _requestHandlers.Add(AuthorizationHandler.Signature, new AuthorizationHandler());
            _requestHandlers.Add(GetMemberHandler.Signature, new GetMemberHandler());
            _requestHandlers.Add(LeaveRoomHandler.Signature, new LeaveRoomHandler());
            _requestHandlers.Add(PingPongHandler.Signature, new PingPongHandler());
            _requestHandlers.Add(KeepAliveHandler.Signature, new KeepAliveHandler());
            _requestHandlers.Add(ChooseNextHandler.Signature, new ChooseNextHandler());
            _requestHandlers.Add(CompleteHandler.Signature, new CompleteHandler());
            _requestHandlers.Add(CurrentTurnHandler.Signature, new CurrentTurnHandler());
            _requestHandlers.Add(VoteHandler.Signature, new VoteHandler());
            _requestHandlers.Add(TakeTurnHandler.Signature, new TakeTurnHandler());
            _requestHandlers.Add(PropertyHandler.Signature, new PropertyHandler());
            _requestHandlers.Add(SnapshotHandler.Signature, new SnapshotHandler());
        }

        private void InitResponseMessageHandlers()
        {
            // this implementation not working on IL2CPP

            /*var baseInterface = typeof(IResponseHandler);
            var subclassTypes = Assembly
                .GetAssembly(baseInterface)
                .GetTypes()
                .Where(t => t.GetInterfaces().Contains(baseInterface) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in subclassTypes)
            {
                var p = (int) type.GetProperty("ActionCommand", BindingFlags.Public | BindingFlags.Static)
                    .GetValue(null);
                _responseHandlers.Add(p, (IResponseHandler) Activator.CreateInstance(type));
            }
            */

            _responseHandlers.Add(AuthResponseHandler.ActionCommand, new AuthResponseHandler());
            _responseHandlers.Add(ErrorResponseHandler.ActionCommand, new ErrorResponseHandler());
            _responseHandlers.Add(JoinRoomResponseHandler.ActionCommand, new JoinRoomResponseHandler());
            _responseHandlers.Add(LeaveRoomResponseHandler.ActionCommand, new LeaveRoomResponseHandler());
            _responseHandlers.Add(MemberDetailsResponseHandler.ActionCommand, new MemberDetailsResponseHandler());
            _responseHandlers.Add(ChooseNextResponseHandler.ActionCommand, new ChooseNextResponseHandler());
            _responseHandlers.Add(CompleteResponseHandler.ActionCommand, new CompleteResponseHandler());
            _responseHandlers.Add(CurrentTurnResponseHandler.ActionCommand, new CurrentTurnResponseHandler());
            _responseHandlers.Add(VoteResponseHandler.ActionCommand, new VoteResponseHandler());
            _responseHandlers.Add(PingResponseHandler.ActionCommand, new PingResponseHandler());
            _responseHandlers.Add(TakeTurnResponseHandler.ActionCommand, new TakeTurnResponseHandler());
            _responseHandlers.Add(PropertyResponseHandler.ActionCommand, new PropertyResponseHandler());
            _responseHandlers.Add(SnapShotResponseHandler.ActionCommand, new SnapShotResponseHandler());
        }


        public void Request(string handlerName, object payload = null, bool isCritical = false)
        {
            Send(_requestHandlers[handlerName]?.HandleAction(payload), isCritical);
        }

        public async Task RequestAsync(string handlerName, object payload = null, bool isCritical = false,bool dontCheckAvailability = false)
        {
            await SendAsync(_requestHandlers[handlerName]?.HandleAction(payload), isCritical,dontCheckAvailability);
        }


        public async Task Init()
        {
            _cancellationToken = new CancellationTokenSource();
            await _tcpClient.Init(null);
        }


        private void Send(Packet packet, bool isCritical = false)
        {
            if (!_observer.Increase(isCritical)) return;
            _tcpClient.Send(packet);
        }

        private async Task SendAsync(Packet packet, bool isCritical = false,bool dontCheckAvailability = false)
        {
            if (!_observer.Increase(isCritical)) return;
            if (IsAvailable) await _tcpClient.SendAsync(packet);
            else if(!dontCheckAvailability) throw new GameServiceException("GameService Not Available")
                .LogException<TurnBasedHandler>(DebugLocation.TurnBased,"SendAsync");
        }

        private void OnDataReceived(object sender, SocketDataReceived e)
        {
            try
            {
                var packet = (Packet) e.Packet;
                LogUtil.Log(this, "TurnBasedHandler OnDataReceived < " + packet);

                if (ActionUtil.IsInternalAction(packet.Action, GSLiveType.TurnBased))
                    _responseHandlers.GetValue(packet.Action)?.HandlePacket(packet);
                else
                    GameService.SynchronizationContext?.Send(
                        delegate { _responseHandlers.GetValue(packet.Action)?.HandlePacket(packet); }, null);
            }
            catch (Exception exception)
            {
                LogUtil.LogError(this, "TurnBasedHandler OnDataReceived ERR : " + exception);
                exception.LogException<TurnBasedHandler>(DebugLocation.TurnBased,"OnDataReceived");
            }
        }

        #region TBHandlerRegion

        private static GsTcpClient _tcpClient;
        public static Room CurrentRoom;
        private readonly GsLiveSystemObserver _observer;
        private CancellationTokenSource _cancellationToken;
        private int _retryConnectCounter;
        private bool _isDisposed;

        public static string PlayerHash { private set; get; }
        public static string PlayToken => GameService.PlayToken;
        public static bool IsAvailable => _tcpClient?.IsAvailable ?? false;

        private readonly Dictionary<int, IResponseHandler> _responseHandlers =
            new Dictionary<int, IResponseHandler>();

        private readonly Dictionary<string, IRequestHandler> _requestHandlers =
            new Dictionary<string, IRequestHandler>();

        #endregion
    }
}