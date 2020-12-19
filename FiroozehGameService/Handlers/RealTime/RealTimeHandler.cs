// <copyright file="RealTimeHandler.cs" company="Firoozeh Technology LTD">
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
using FiroozehGameService.Core;
using FiroozehGameService.Core.Socket;
using FiroozehGameService.Handlers.RealTime.RequestHandlers;
using FiroozehGameService.Handlers.RealTime.ResponseHandlers;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;
using FiroozehGameService.Utils.Serializer;
using Packet = FiroozehGameService.Models.GSLive.RT.Packet;

namespace FiroozehGameService.Handlers.RealTime
{
    internal class RealTimeHandler : IDisposable
    {
        internal RealTimeHandler(StartPayload payload)
        {
            CurrentRoom = payload.Room;
            _udpClient = new GsUdpClient(payload.Area);
            _udpClient.DataReceived += OnDataReceived;
            _udpClient.Error += OnError;

            _observer = new GsLiveSystemObserver(GSLiveType.RealTime);
            _isDisposed = false;

            // Set Internal Event Handlers
            CoreEventHandlers.Authorized += OnAuth;
            CoreEventHandlers.OnMemberId += OnMemberId;
            CoreEventHandlers.GProtocolConnected += OnConnected;
            CoreEventHandlers.Ping += Ping;
            PingUtil.RequestPing += RequestPing;
            ObserverCompacterUtil.SendObserverEventHandler += SendObserverEventHandler;

            InitRequestMessageHandlers();
            InitResponseMessageHandlers();

            DebugUtil.LogNormal<RealTimeHandler>(DebugLocation.RealTime,"Constructor","RealTimeHandler init");
        }

        public void Dispose()
        {
            if (_isDisposed)
            {
                DebugUtil.LogNormal<RealTimeHandler>(DebugLocation.RealTime,"Dispose","RealTimeHandler Already Disposed");
                return;
            }
            
            _isDisposed = true;
            PlayerHash = 0;


            _udpClient?.StopReceiving();
            _observer?.Dispose();
            PingUtil.Dispose();

            ObserverCompacterUtil.Dispose();
            
            DebugUtil.LogNormal<RealTimeHandler>(DebugLocation.RealTime,"Dispose","RealTimeHandler Dispose Done");

            GsSerializer.CurrentPlayerLeftRoom?.Invoke(this, null);
            CoreEventHandlers.Dispose?.Invoke(this, null);
        }


        private void SendObserverEventHandler(object sender, byte[] data)
        {
            Request(ObserverHandler.Signature, GProtocolSendType.UnReliable, data, canSendBigSize: true);
        }

        private static void OnMemberId(object sender, string id)
        {
            MemberId = id;
        }


        private void RequestPing(object sender, EventArgs e)
        {
            if(IsAvailable) Request(GetPingHandler.Signature, GProtocolSendType.Reliable, isCritical: true);
        }

        internal static short GetPing()
        {
            return (short) PingUtil.GetLastPing();
        }

        private void Ping(object sender, APacket packet)
        {
            if (sender.GetType() != typeof(PingResponseHandler)) return;
            var currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var sendTime = (packet as Packet)?.ClientSendTime;
            if (sendTime == null) return;

            var diff = PingUtil.Diff(currentTime, sendTime.Value);
            PingUtil.SetLastPing(diff);
        }

        private void OnConnected(object sender, EventArgs e)
        {
            // Send Auth When Connected
            DebugUtil.LogNormal<RealTimeHandler>(DebugLocation.RealTime,"OnConnected","RealTimeHandler GProtocol Connected");
            Request(AuthorizationHandler.Signature, GProtocolSendType.Reliable, isCritical: true);
        }


        private void OnAuth(object sender, object playerHash)
        {
            if (sender.GetType() != typeof(AuthResponseHandler)) return;
            
            // this is Reconnect
            if(PlayerHash != 0) RealTimeEventHandlers.Reconnected?.Invoke(null,ReconnectStatus.Connected);
            
            PlayerHash = (ulong) playerHash;
            
            DebugUtil.LogNormal<RealTimeHandler>(DebugLocation.RealTime,"OnAuth","RealTimeHandler Auth Done");

            PingUtil.Init();
            ObserverCompacterUtil.Init();

            // Get Only in First Connect
            if (PlayerHash != 0) return;
            Request(SnapShotHandler.Signature, GProtocolSendType.Reliable, isCritical: true);
            GsSerializer.CurrentPlayerJoinRoom?.Invoke(this, null);
        }

        private void InitRequestMessageHandlers()
        {
            _requestHandlers.Add(AuthorizationHandler.Signature, new AuthorizationHandler());
            _requestHandlers.Add(GetMemberHandler.Signature, new GetMemberHandler());
            _requestHandlers.Add(LeaveRoomHandler.Signature, new LeaveRoomHandler());
            _requestHandlers.Add(GetPingHandler.Signature, new GetPingHandler());
            _requestHandlers.Add(SendPrivateMessageHandler.Signature, new SendPrivateMessageHandler());
            _requestHandlers.Add(SendPublicMessageHandler.Signature, new SendPublicMessageHandler());
            _requestHandlers.Add(NewEventHandler.Signature, new NewEventHandler());
            _requestHandlers.Add(SnapShotHandler.Signature, new SnapShotHandler());
            _requestHandlers.Add(RoomInfoHandler.Signature, new RoomInfoHandler());
            _requestHandlers.Add(ObserverHandler.Signature, new ObserverHandler());
        }

        private void InitResponseMessageHandlers()
        {
            _responseHandlers.Add(AuthResponseHandler.ActionCommand, new AuthResponseHandler());
            _responseHandlers.Add(ErrorResponseHandler.ActionCommand, new ErrorResponseHandler());
            _responseHandlers.Add(JoinRoomResponseHandler.ActionCommand, new JoinRoomResponseHandler());
            _responseHandlers.Add(LeaveRoomResponseHandler.ActionCommand, new LeaveRoomResponseHandler());
            _responseHandlers.Add(PingResponseHandler.ActionCommand, new PingResponseHandler());
            _responseHandlers.Add(MemberDetailsResponseHandler.ActionCommand, new MemberDetailsResponseHandler());
            _responseHandlers.Add(PrivateMessageResponseHandler.ActionCommand, new PrivateMessageResponseHandler());
            _responseHandlers.Add(PublicMessageResponseHandler.ActionCommand, new PublicMessageResponseHandler());
            _responseHandlers.Add(NewEventResponseHandler.ActionCommand, new NewEventResponseHandler());
            _responseHandlers.Add(SnapShotResponseHandler.ActionCommand, new SnapShotResponseHandler());
            _responseHandlers.Add(RoomInfoResponseHandler.ActionCommand, new RoomInfoResponseHandler());
            _responseHandlers.Add(ObserverResponseHandler.ActionCommand, new ObserverResponseHandler());
        }


        internal void Request(string handlerName, GProtocolSendType type, object payload = null,
            bool isCritical = false, bool canSendBigSize = false)
        {
            Send(_requestHandlers[handlerName]?.HandleAction(payload), type, isCritical, canSendBigSize);
        }


        internal static void Init()
        {
            _udpClient.Init();
        }


        private void Send(Packet packet, GProtocolSendType type, bool isCritical = false, bool canSendBigSize = false)
        {
            if (!_observer.Increase(isCritical)) return;
            if (IsAvailable) _udpClient.Send(packet, type, canSendBigSize);
            else throw new GameServiceException("GameService Not Available")
                .LogException<RealTimeHandler>(DebugLocation.RealTime,"Send");
        }


        private void OnError(object sender, ErrorArg e)
        {
            DebugUtil.LogError<RealTimeHandler>(DebugLocation.RealTime,"OnError",e.Error);
            if(PlayerHash != 0) RealTimeEventHandlers.Reconnected?.Invoke(null,ReconnectStatus.Connecting);
            if (_isDisposed) return;
            Init();
        }


        private void OnDataReceived(object sender, SocketDataReceived e)
        {
            try
            {
                if (_isDisposed) return;
                var packet = (Packet) e.Packet;
                packet.ClientReceiveTime = e.Time;
                
                GameService.SynchronizationContext?.Send(delegate
                {
                    _responseHandlers.GetValue(packet.Action)?.HandlePacket(packet, packet.SendType);
                }, null);
            }
            catch (Exception exception)
            {
                exception.LogException<RealTimeHandler>(DebugLocation.RealTime,"OnDataReceived");
            }
        }


        #region RTHandlerRegion

        private static GsUdpClient _udpClient;
        public static Room CurrentRoom;

        private readonly GsLiveSystemObserver _observer;
        private bool _isDisposed;


        public static string MemberId { private set; get; }
        public static ulong PlayerHash { private set; get; }
        public static string PlayToken => GameService.PlayToken;
        public static bool IsAvailable => GsUdpClient.IsAvailable;

        private readonly Dictionary<int, IResponseHandler> _responseHandlers =
            new Dictionary<int, IResponseHandler>();

        private readonly Dictionary<string, IRequestHandler> _requestHandlers =
            new Dictionary<string, IRequestHandler>();

        #endregion
    }
}