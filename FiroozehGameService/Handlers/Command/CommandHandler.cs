// <copyright file="CommandHandler.cs" company="Firoozeh Technology LTD">
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
using System.Threading;
using FiroozehGameService.Core;
using FiroozehGameService.Core.Socket;
using FiroozehGameService.Handlers.Command.RequestHandlers;
using FiroozehGameService.Handlers.Command.RequestHandlers.Chat;
using FiroozehGameService.Handlers.Command.ResponseHandlers;
using FiroozehGameService.Handlers.Command.ResponseHandlers.Chat;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Handlers.Command
{
    internal class CommandHandler
    {
        internal CommandHandler()
        {
            switch (GameService.Configuration.CommandConnectionType)
            {
                case ConnectionType.Native:
                    _tcpClient = new GsTcpClient();
                    break;
                case ConnectionType.WebSocket:
                    _tcpClient = new GsWebSocketClient();
                    break;
                default:
                    throw new ArgumentOutOfRangeException();
            }

            _tcpClient.DataReceived += OnDataReceived;

            _cancellationToken = new CancellationTokenSource();
            _observer = new GsLiveSystemObserver(GSLiveType.Command);
            _isDisposed = false;
            _isFirstInit = false;

            InitRequestMessageHandlers();
            InitResponseMessageHandlers();

            PingUtil.Init();

            // Set Internal Event Handlers
            CommandEventHandlers.CommandPing += OnPing;
            CommandEventHandlers.CommandAuthorized += OnAuth;
            CommandEventHandlers.CommandMirror += OnMirror;
            PingUtil.RequestPing += RequestPing;
            CommandEventHandlers.GsCommandClientConnected += OnGsTcpClientConnected;
            CommandEventHandlers.GsCommandClientError += OnGsTcpClientError;

            DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command, "Constructor", "CommandHandler Initialized");
        }

        public void Dispose(bool isGraceful)
        {
            try
            {
                _isDisposed = true;
                _isFirstInit = false;
                _isPingRequested = false;

                _cancellationToken?.Cancel(false);
                _observer?.Dispose();
                PingUtil.Dispose();

                _tcpClient?.StopReceiving(isGraceful);
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                _tcpClient = null;
                PlayerHash = null;

                CommandEventHandlers.CommandAuthorized = null;
                CommandEventHandlers.CommandClientConnected = null;
                CommandEventHandlers.GsCommandClientConnected = null;
                CommandEventHandlers.GsCommandClientError = null;
                CommandEventHandlers.CommandPing = null;
                CommandEventHandlers.CommandMirror = null;

                try
                {
                    GC.SuppressFinalize(this);
                }
                catch (Exception)
                {
                    // ignored
                }

                DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command, "Dispose", "CommandHandler Dispose Done");
            }
        }

        private void OnMirror(object sender, Packet packet)
        {
            try
            {
                var currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
                var lastCurrentTime = long.Parse(packet.Data);

                PingUtil.SetLastPing(currentTime, lastCurrentTime);
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                _isPingRequested = false;
            }
        }

        private void RequestPing(object sender, EventArgs e)
        {
            if (_isPingRequested)
            {
                DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command, "RequestPing",
                    "CommandHandler -> Server Not Response Ping, Reconnecting...");

                _isPingRequested = false;

                Init();
                return;
            }

            var currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            _isPingRequested = true;

            Send(MirrorHandler.Signature, currentTime, true);
        }

        private void OnGsTcpClientError(object sender, GameServiceException exception)
        {
            if (_isDisposed) return;

            exception.LogException<CommandHandler>(DebugLocation.Command, "OnGsTcpClientError");

            _retryConnectCounter++;

            DebugUtil.LogError<CommandHandler>(DebugLocation.Command, "OnGsTcpClientError",
                "CommandHandler Reconnect Retry " + _retryConnectCounter + " , Wait to Connect...");

            Init();
        }

        private void OnGsTcpClientConnected(object sender, object e)
        {
            DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command, "OnGsTcpClientConnected",
                "CommandHandler -> Connected,Waiting for Handshakes...");

            _retryConnectCounter = 0;

            _tcpClient?.StartReceiving();

            Thread.Sleep(100);

            Send(AuthorizationHandler.Signature, isCritical: true);

            DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command, "OnGsTcpClientConnected",
                "CommandHandler Init done");
        }

        private void OnAuth(object sender, string playerHash)
        {
            DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command, "OnAuth", "CommandHandler OnAuth Done");

            PlayerHash = playerHash;
            PingUtil.Start();

            if (_isFirstInit) return;
            _isFirstInit = true;
            CoreEventHandlers.SuccessfullyLogined?.Invoke(null, null);
        }

        private void OnPing(object sender, object packet)
        {
            Send(PingPongHandler.Signature, isCritical: true);
            DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command, "OnPing", "CommandHandler Ping Called");
        }


        private void InitRequestMessageHandlers()
        {
            _requestHandlers.Add(AcceptInviteHandler.Signature, new AcceptInviteHandler());
            _requestHandlers.Add(AuthorizationHandler.Signature, new AuthorizationHandler());
            _requestHandlers.Add(AutoMatchHandler.Signature, new AutoMatchHandler());
            _requestHandlers.Add(CancelAutoMatchHandler.Signature, new CancelAutoMatchHandler());
            _requestHandlers.Add(CreateRoomHandler.Signature, new CreateRoomHandler());
            _requestHandlers.Add(FindMemberHandler.Signature, new FindMemberHandler());
            _requestHandlers.Add(GetRoomsHandler.Signature, new GetRoomsHandler());
            _requestHandlers.Add(InviteListHandler.Signature, new InviteListHandler());
            _requestHandlers.Add(InviteUserHandler.Signature, new InviteUserHandler());
            _requestHandlers.Add(JoinRoomHandler.Signature, new JoinRoomHandler());
            _requestHandlers.Add(PingPongHandler.Signature, new PingPongHandler());

            _requestHandlers.Add(GetChannelRecentMessagesRequestHandler.Signature,
                new GetChannelRecentMessagesRequestHandler());
            _requestHandlers.Add(GetChannelsMembersRequestHandler.Signature, new GetChannelsMembersRequestHandler());
            _requestHandlers.Add(GetChannelsSubscribedRequestHandler.Signature,
                new GetChannelsSubscribedRequestHandler());
            _requestHandlers.Add(GetPendingMessagesRequestHandler.Signature, new GetPendingMessagesRequestHandler());
            _requestHandlers.Add(SendChannelPrivateMessageHandler.Signature, new SendChannelPrivateMessageHandler());
            _requestHandlers.Add(SendChannelPublicMessageHandler.Signature, new SendChannelPublicMessageHandler());
            _requestHandlers.Add(SubscribeChannelHandler.Signature, new SubscribeChannelHandler());
            _requestHandlers.Add(UnsubscribeChannelHandler.Signature, new UnsubscribeChannelHandler());

            _requestHandlers.Add(MirrorHandler.Signature, new MirrorHandler());

            _requestHandlers.Add(RemoveChatHandler.Signature, new RemoveChatHandler());
            _requestHandlers.Add(RemoveMemberChatsHandler.Signature, new RemoveMemberChatsHandler());
        }

        private void InitResponseMessageHandlers()
        {
            _responseHandlers.Add(AuthResponseHandler.ActionCommand, new AuthResponseHandler());
            _responseHandlers.Add(AutoMatchResponseHandler.ActionCommand, new AutoMatchResponseHandler());
            _responseHandlers.Add(CancelAutoMatchResponseHandler.ActionCommand, new CancelAutoMatchResponseHandler());
            _responseHandlers.Add(ErrorResponseHandler.ActionCommand, new ErrorResponseHandler());
            _responseHandlers.Add(FindMemberResponseHandler.ActionCommand, new FindMemberResponseHandler());
            _responseHandlers.Add(GetInviteInboxResponseHandler.ActionCommand, new GetInviteInboxResponseHandler());
            _responseHandlers.Add(GetRoomResponseHandler.ActionCommand, new GetRoomResponseHandler());
            _responseHandlers.Add(InviteReceivedInboxResponseHandler.ActionCommand,
                new InviteReceivedInboxResponseHandler());
            _responseHandlers.Add(InviteUserResponseHandler.ActionCommand, new InviteUserResponseHandler());
            _responseHandlers.Add(JoinRoomResponseHandler.ActionCommand, new JoinRoomResponseHandler());
            _responseHandlers.Add(NotificationResponseHandler.ActionCommand, new NotificationResponseHandler());
            _responseHandlers.Add(PingResponseHandler.ActionCommand, new PingResponseHandler());

            _responseHandlers.Add(ChannelsMembersResponseHandler.ActionCommand, new ChannelsMembersResponseHandler());
            _responseHandlers.Add(ChannelRecentResponseHandler.ActionCommand, new ChannelRecentResponseHandler());
            _responseHandlers.Add(ChannelSubscribedResponseHandler.ActionCommand,
                new ChannelSubscribedResponseHandler());
            _responseHandlers.Add(PendingMessagesResponseHandler.ActionCommand, new PendingMessagesResponseHandler());
            _responseHandlers.Add(PrivateChatResponseHandler.ActionCommand, new PrivateChatResponseHandler());
            _responseHandlers.Add(PublicChatResponseHandler.ActionCommand, new PublicChatResponseHandler());
            _responseHandlers.Add(SubscribeChannelResponseHandler.ActionCommand, new SubscribeChannelResponseHandler());
            _responseHandlers.Add(UnSubscribeChannelResponseHandler.ActionCommand,
                new UnSubscribeChannelResponseHandler());

            _responseHandlers.Add(MirrorResponseHandler.ActionCommand,
                new MirrorResponseHandler());

            _responseHandlers.Add(RemoveChatResponseHandler.ActionCommand, new RemoveChatResponseHandler());
            _responseHandlers.Add(RemoveMemberChatsResponseHandler.ActionCommand,
                new RemoveMemberChatsResponseHandler());
        }

        public void Init()
        {
            _cancellationToken = new CancellationTokenSource();

            PingUtil.Stop();
            _tcpClient.Init(GameService.CommandInfo, GameService.CommandInfo.Cipher);
        }

        internal static short GetPing()
        {
            return PingUtil.GetLastPing();
        }

        private void Request(string handlerName, object payload = null, bool isCritical = false)
        {
            Send(_requestHandlers[handlerName]?.HandleAction(payload), isCritical);
        }

        internal void Send(string handlerName, object payload = null, bool isCritical = false)
        {
            AddToSendQueue(_requestHandlers[handlerName]?.HandleAction(payload), isCritical);
        }


        private void Send(Packet packet, bool isCritical = false)
        {
            if (!_observer.Increase(isCritical)) return;
            if (IsAvailable()) _tcpClient.Send(packet);
            else if (!isCritical)
                throw new GameServiceException("GameService Not Available")
                    .LogException<CommandHandler>(DebugLocation.Command, "Send");
        }

        private void AddToSendQueue(Packet packet, bool isCritical = false)
        {
            if (!_observer.Increase(isCritical))
                throw new GameServiceException("Too Many Requests, You Can Send " + CommandConst.CommandLimit +
                                               " Requests Per Second")
                    .LogException<CommandHandler>(DebugLocation.Command, "AddToSendQueue");
            // TODO Check it
            if (!_isDisposed) _tcpClient.AddToSendQueue(packet);
            else if (!isCritical)
                throw new GameServiceException("Command System Already Disposed")
                    .LogException<CommandHandler>(DebugLocation.Command, "AddToSendQueue");
        }

        private void OnDataReceived(object sender, SocketDataReceived e)
        {
            try
            {
                if (e.Packet == null) return;

                var packet = (Packet) e.Packet;

                if (ActionUtil.IsInternalAction(packet.Action, GSLiveType.Command))
                    _responseHandlers.GetValue(packet.Action)?.HandlePacket(packet);
                else
                    GameService.SynchronizationContext?.Send(
                        delegate { _responseHandlers.GetValue(packet.Action)?.HandlePacket(packet); }, null);
            }
            catch (Exception exception)
            {
                exception.LogException<CommandHandler>(DebugLocation.Command, "OnDataReceived");
            }
        }

        internal static bool IsAvailable()
        {
            return _tcpClient != null && _tcpClient.IsConnected();
        }


        #region Fields

        private static GTcpClient _tcpClient;
        private readonly GsLiveSystemObserver _observer;
        private CancellationTokenSource _cancellationToken;
        private int _retryConnectCounter;
        private bool _isDisposed;
        private bool _isFirstInit;
        private bool _isPingRequested;

        internal static string PlayerHash { private set; get; }

        internal static string GameId => GameService.CurrentInternalGame?._Id;
        internal static string UserToken => GameService.UserToken;

        private readonly Dictionary<int, IResponseHandler> _responseHandlers =
            new Dictionary<int, IResponseHandler>();

        private readonly Dictionary<string, IRequestHandler> _requestHandlers =
            new Dictionary<string, IRequestHandler>();

        #endregion
    }
}