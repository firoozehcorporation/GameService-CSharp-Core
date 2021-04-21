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
        internal TurnBasedHandler(StartPayload payload)
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
            CoreEventHandlers.OnLeftDispose += OnLeftDispose;
            CoreEventHandlers.OnGsTcpClientConnected += OnGsTcpClientConnected;
            CoreEventHandlers.OnGsTcpClientError += OnGsTcpClientError;


            InitRequestMessageHandlers();
            InitResponseMessageHandlers();

            DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased, "Constructor", "TurnBasedHandler Init");
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased, "Dispose",
                    "TurnBasedHandler Already Disposed");
                return;
            }

            _retryConnectCounter = 0;
            _isDisposed = true;
            _tcpClient?.StopReceiving();
            _observer?.Dispose();
            _cancellationToken?.Cancel(true);
            CoreEventHandlers.Dispose?.Invoke(this, null);

            DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased, "Dispose", "TurnBasedHandler Dispose Done");
        }

        private void OnLeftDispose(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(LeaveRoomResponseHandler)) return;

            DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased, "OnLeftDispose",
                "Current Player Left From Server, so dispose TurnBased...");
            Dispose();
        }

        private async void OnGsTcpClientError(object sender, GameServiceException exception)
        {
            if ((GSLiveType) sender != GSLiveType.TurnBased) return;
            if (_isDisposed) return;

            exception.LogException<TurnBasedHandler>(DebugLocation.TurnBased, "OnGsTcpClientError");

            if (PlayerHash != null) TurnBasedEventHandlers.Reconnected?.Invoke(null, ReconnectStatus.Connecting);

            _retryConnectCounter++;

            if (_retryConnectCounter >= TurnBasedConst.MaxRetryConnect)
            {
                DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased, "OnGsTcpClientError",
                    "TurnBasedHandler Reached to MaxRetryConnect , so dispose TurnBased...");
                Dispose();
                return;
            }

            DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased, "OnGsTcpClientError",
                "TurnBasedHandler reconnect Retry " + _retryConnectCounter + " , Wait to Connect...");

            await Init();
        }

        private async void OnGsTcpClientConnected(object sender, TcpClient e)
        {
            if ((GSLiveType) sender != GSLiveType.TurnBased) return;

            DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased, "OnGsTcpClientConnected",
                "TurnBasedHandler -> Connected,Waiting for Handshakes...");


            Task.Run(async () => { await _tcpClient.StartReceiving(); }, _cancellationToken.Token);
            await RequestAsync(AuthorizationHandler.Signature, isCritical: true);

            _retryConnectCounter = 0;

            DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased, "OnGsTcpClientConnected",
                "TurnBasedHandler Init done");
        }

        private static void OnAuth(object sender, object playerHash)
        {
            if (sender.GetType() != typeof(AuthResponseHandler)) return;

            // this is Reconnect
            if (PlayerHash != null) TurnBasedEventHandlers.Reconnected?.Invoke(null, ReconnectStatus.Connected);
            PlayerHash = (string) playerHash;

            DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased, "OnAuth", "TurnBasedHandler Auth Done");
        }

        private async void OnPing(object sender, APacket packet)
        {
            if (sender.GetType() != typeof(PingResponseHandler)) return;
            await RequestAsync(PingPongHandler.Signature, isCritical: true);

            DebugUtil.LogNormal<TurnBasedHandler>(DebugLocation.TurnBased, "OnPing", "TurnBasedHandler Ping Called");
        }

        private void InitRequestMessageHandlers()
        {
            _requestHandlers.Add(AuthorizationHandler.Signature, new AuthorizationHandler());
            _requestHandlers.Add(GetMemberHandler.Signature, new GetMemberHandler());
            _requestHandlers.Add(LeaveRoomHandler.Signature, new LeaveRoomHandler());
            _requestHandlers.Add(PingPongHandler.Signature, new PingPongHandler());
            _requestHandlers.Add(ChooseNextHandler.Signature, new ChooseNextHandler());
            _requestHandlers.Add(CompleteHandler.Signature, new CompleteHandler());
            _requestHandlers.Add(CurrentTurnHandler.Signature, new CurrentTurnHandler());
            _requestHandlers.Add(VoteHandler.Signature, new VoteHandler());
            _requestHandlers.Add(TakeTurnHandler.Signature, new TakeTurnHandler());
            _requestHandlers.Add(PropertyHandler.Signature, new PropertyHandler());
            _requestHandlers.Add(RoomInfoHandler.Signature, new RoomInfoHandler());
            _requestHandlers.Add(SnapshotHandler.Signature, new SnapshotHandler());
        }

        private void InitResponseMessageHandlers()
        {
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
            _responseHandlers.Add(RoomInfoResponseHandler.ActionCommand, new RoomInfoResponseHandler());
            _responseHandlers.Add(SnapShotResponseHandler.ActionCommand, new SnapShotResponseHandler());
        }


        public void Request(string handlerName, object payload = null, bool isCritical = false)
        {
            Send(_requestHandlers[handlerName]?.HandleAction(payload), isCritical);
        }

        public async Task RequestAsync(string handlerName, object payload = null, bool isCritical = false,
            bool dontCheckAvailability = false)
        {
            await SendAsync(_requestHandlers[handlerName]?.HandleAction(payload), isCritical, dontCheckAvailability);
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

        private async Task SendAsync(Packet packet, bool isCritical = false, bool dontCheckAvailability = false)
        {
            if (!_observer.Increase(isCritical)) return;
            if (IsAvailable) await _tcpClient.SendAsync(packet);
            else if (!dontCheckAvailability)
                throw new GameServiceException("GameService Not Available")
                    .LogException<TurnBasedHandler>(DebugLocation.TurnBased, "SendAsync");
        }

        private void OnDataReceived(object sender, SocketDataReceived e)
        {
            try
            {
                var packet = (Packet) e.Packet;

                if (ActionUtil.IsInternalAction(packet.Action, GSLiveType.TurnBased))
                    _responseHandlers.GetValue(packet.Action)?.HandlePacket(packet);
                else
                    GameService.SynchronizationContext?.Send(
                        delegate { _responseHandlers.GetValue(packet.Action)?.HandlePacket(packet); }, null);
            }
            catch (Exception exception)
            {
                exception.LogException<TurnBasedHandler>(DebugLocation.TurnBased, "OnDataReceived");
            }
        }

        #region TBHandlerRegion

        private static GTcpClient _tcpClient;
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