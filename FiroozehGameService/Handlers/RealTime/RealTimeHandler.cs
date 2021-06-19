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
using FiroozehGameService.Core.Providers.GSLive;
using FiroozehGameService.Core.Socket;
using FiroozehGameService.Handlers.RealTime.RequestHandlers;
using FiroozehGameService.Handlers.RealTime.ResponseHandlers;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Models.Internal;
using FiroozehGameService.Utils;
using FiroozehGameService.Utils.Serializer;
using Packet = FiroozehGameService.Models.GSLive.RT.Packet;

namespace FiroozehGameService.Handlers.RealTime
{
    internal class RealTimeHandler
    {
        internal RealTimeHandler(StartPayload payload)
        {
            CurrentRoom = payload.Room;
            AuthHash = payload.Area.Hash;

            _udpClient = new GsUdpClient(payload.Area);
            _udpClient.DataReceived += OnDataReceived;
            _udpClient.Error += OnError;

            _observer = new GsLiveSystemObserver(GSLiveType.RealTime);
            _dataObserver = new RealtimeDataObserver();
            _isDisposed = false;
            PlayerHash = -1;

            // Set Internal Event Handlers
            RealTimeEventHandlers.Authorized += OnAuth;
            RealTimeEventHandlers.MemberId += OnMemberId;
            RealTimeEventHandlers.GProtocolConnected += OnConnected;
            RealTimeEventHandlers.LeftDispose += OnLeftDispose;
            ObserverCompacterUtil.SendObserverEventHandler += SendObserverEventHandler;
            RealtimeDataObserver.Caller += DataGetter;

            InitRequestMessageHandlers();
            InitResponseMessageHandlers();

            DebugUtil.LogNormal<RealTimeHandler>(DebugLocation.RealTime, "Constructor", "RealTimeHandler init");
        }

        private static void OnLeftDispose(object sender, EventArgs e)
        {
            DebugUtil.LogNormal<RealTimeHandler>(DebugLocation.RealTime, "OnLeftDispose",
                "Connection Gracefully Closed By Server, so dispose Realtime...");

            CoreEventHandlers.Dispose?.Invoke(null, new DisposeData
            {
                Type = GSLiveType.RealTime, IsGraceful = false
            });
        }


        private void SendObserverEventHandler(object sender, byte[] data)
        {
            Request(ObserverHandler.Signature, GProtocolSendType.UnReliable, data, canSendBigSize: true);
        }


        private static void DataGetter(object sender, EventArgs e)
        {
            Rtt = _udpClient.GetRtt();
            PacketLost = _udpClient.GetPacketLost();
        }


        private static void OnMemberId(object sender, string id)
        {
            MemberId = id;
        }

        private void OnConnected(object sender, EventArgs e)
        {
            // Send OnAuth When Connected
            DebugUtil.LogNormal<RealTimeHandler>(DebugLocation.RealTime, "OnConnected",
                "RealTimeHandler GProtocol Connected");
            Request(AuthorizationHandler.Signature, GProtocolSendType.Reliable, isCritical: true);
        }


        private void OnAuth(object sender, long playerHash)
        {
            // this is Reconnect
            if (PlayerHash != -1)
            {
                RealTimeEventHandlers.Reconnected?.Invoke(null, ReconnectStatus.Connected);
                return;
            }

            PlayerHash = playerHash;
            GsLiveRealTime.InAutoMatch = false;

            ObserverCompacterUtil.Init();
            Request(SnapShotHandler.Signature, GProtocolSendType.Reliable, isCritical: true);
            GsSerializer.CurrentPlayerJoinRoom?.Invoke(this, null);

            DebugUtil.LogNormal<RealTimeHandler>(DebugLocation.RealTime, "OnAuth", "RealTimeHandler OnAuth Done");
        }

        private void InitRequestMessageHandlers()
        {
            _requestHandlers.Add(AuthorizationHandler.Signature, new AuthorizationHandler());
            _requestHandlers.Add(GetMemberHandler.Signature, new GetMemberHandler());
            _requestHandlers.Add(LeaveRoomHandler.Signature, new LeaveRoomHandler());
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
            _responseHandlers.Add(MemberDetailsResponseHandler.ActionCommand, new MemberDetailsResponseHandler());
            _responseHandlers.Add(PrivateMessageResponseHandler.ActionCommand, new PrivateMessageResponseHandler());
            _responseHandlers.Add(PublicMessageResponseHandler.ActionCommand, new PublicMessageResponseHandler());
            _responseHandlers.Add(NewEventResponseHandler.ActionCommand, new NewEventResponseHandler());
            _responseHandlers.Add(SnapShotResponseHandler.ActionCommand, new SnapShotResponseHandler());
            _responseHandlers.Add(RoomInfoResponseHandler.ActionCommand, new RoomInfoResponseHandler());
            _responseHandlers.Add(ObserverResponseHandler.ActionCommand, new ObserverResponseHandler());
        }


        internal void Request(string handlerName, GProtocolSendType type, object payload = null,
            bool isCritical = false, bool canSendBigSize = false, bool isEvent = false)
        {
            Send(_requestHandlers[handlerName]?.HandleAction(payload), type, isCritical, canSendBigSize, isEvent);
        }


        internal void Init()
        {
            _udpClient.Init();
        }

        public void Dispose(bool isGraceful)
        {
            try
            {
                if (_isDisposed)
                {
                    DebugUtil.LogNormal<RealTimeHandler>(DebugLocation.RealTime, "Dispose",
                        "RealTimeHandler Already Disposed");
                    return;
                }

                _isDisposed = true;

                _observer?.Dispose();
                _dataObserver?.Dispose();
                ObserverCompacterUtil.Dispose();

                _udpClient?.StopReceiving(isGraceful);
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                _udpClient = null;
                CurrentRoom = null;
                MemberId = null;
                AuthHash = null;
                PlayerHash = -1;

                RealTimeEventHandlers.Authorized = null;
                RealTimeEventHandlers.MemberId = null;
                RealTimeEventHandlers.GProtocolConnected = null;
                RealTimeEventHandlers.LeftDispose = null;

                try
                {
                    GC.SuppressFinalize(this);
                }
                catch (Exception)
                {
                    // ignored
                }

                DebugUtil.LogNormal<RealTimeHandler>(DebugLocation.RealTime, "Dispose", "RealTimeHandler Dispose Done");
            }
        }

        internal static int GetRoundTripTime()
        {
            if (_udpClient == null) return -1;
            return Rtt;
        }


        internal static long GetPacketLost()
        {
            if (_udpClient == null) return -1;
            return PacketLost;
        }


        private void Send(Packet packet, GProtocolSendType type, bool isCritical = false, bool canSendBigSize = false,
            bool isEvent = false)
        {
            if (!_observer.Increase(isCritical)) return;
            if (IsAvailable()) _udpClient.Send(packet, type, canSendBigSize, isCritical, isEvent);
            else
                throw new GameServiceException("GameService Not Available")
                    .LogException<RealTimeHandler>(DebugLocation.RealTime, "Send");
        }


        private void OnError(object sender, ErrorArg e)
        {
            DebugUtil.LogError<RealTimeHandler>(DebugLocation.RealTime, "OnError", e.Error);
            if (PlayerHash != -1) RealTimeEventHandlers.Reconnected?.Invoke(null, ReconnectStatus.Connecting);
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

                GameService.SynchronizationContext?.Send(
                    delegate { _responseHandlers.GetValue(packet.Action)?.HandlePacket(packet, packet.SendType); },
                    null);
            }
            catch (Exception exception)
            {
                exception.LogException<RealTimeHandler>(DebugLocation.RealTime, "OnDataReceived");
            }
        }

        internal static bool IsAvailable()
        {
            return _udpClient != null && _udpClient.IsConnected();
        }


        #region RTHandlerRegion

        private static GProtocolClient _udpClient;
        internal static Room CurrentRoom;

        private readonly GsLiveSystemObserver _observer;
        private readonly RealtimeDataObserver _dataObserver;
        private bool _isDisposed;


        private static int Rtt { set; get; }

        private static long PacketLost { set; get; }

        internal static string MemberId { private set; get; }
        internal static long PlayerHash { private set; get; }

        internal static string AuthHash { private set; get; }

        internal static string PlayToken => GameService.PlayToken;

        private readonly Dictionary<int, IResponseHandler> _responseHandlers =
            new Dictionary<int, IResponseHandler>();

        private readonly Dictionary<string, IRequestHandler> _requestHandlers =
            new Dictionary<string, IRequestHandler>();

        #endregion
    }
}