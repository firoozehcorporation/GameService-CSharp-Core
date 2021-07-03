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
using System.Threading.Tasks;
using FiroozehGameService.Core;
using FiroozehGameService.Core.Socket;
using FiroozehGameService.Handlers.Command.RequestHandlers;
using FiroozehGameService.Handlers.Command.RequestHandlers.Chat;
using FiroozehGameService.Handlers.Command.ResponseHandlers;
using FiroozehGameService.Handlers.Command.ResponseHandlers.Chat;
using FiroozehGameService.Models;
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
            _tcpClient = new GsTcpClient();
            _tcpClient.DataReceived += OnDataReceived;

            _cancellationToken = new CancellationTokenSource();
            _observer = new GsLiveSystemObserver(GSLiveType.Command);
            _isDisposed = false;
            _isFirstInit = false;

            InitRequestMessageHandlers();
            InitResponseMessageHandlers();

            // Set Internal Event Handlers
            CommandEventHandlers.CommandPing += OnPing;
            CommandEventHandlers.CommandAuthorized += OnAuth;
            CommandEventHandlers.Mirror += OnMirror;
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
                CommandEventHandlers.Mirror = null;

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
            var currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            var lastCurrentTime = long.Parse(packet.Data);
            _isPingRequested = false;

            PingUtil.SetLastPing(currentTime, lastCurrentTime);
        }

        private async void RequestPing(object sender, EventArgs e)
        {
            if (_isPingRequested)
            {
                DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command, "RequestPing",
                    "CommandHandler -> Server Not Response Ping, Reconnecting...");

                await Init();
                _isPingRequested = false;
                return;
            }

            var currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            _isPingRequested = true;

            await RequestAsync(TimeHandler.Signature, currentTime, true);
        }

        private async void OnGsTcpClientError(object sender, GameServiceException exception)
        {
            if (_isDisposed) return;

            exception.LogException<CommandHandler>(DebugLocation.Command, "OnGsTcpClientError");

            _retryConnectCounter++;

            DebugUtil.LogError<CommandHandler>(DebugLocation.Command, "OnGsTcpClientError",
                "CommandHandler Reconnect Retry " + _retryConnectCounter + " , Wait to Connect...");

            await Init();
        }

        private async void OnGsTcpClientConnected(object sender, object e)
        {
            DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command, "OnGsTcpClientConnected",
                "CommandHandler -> Connected,Waiting for Handshakes...");

            _retryConnectCounter = 0;

            _tcpClient?.StartReceiving();

            await Task.Delay(100);

            await RequestAsync(AuthorizationHandler.Signature, isCritical: true);

            _tcpClient?.SetEncryptionStatus(true);


            DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command, "OnGsTcpClientConnected",
                "CommandHandler Init done");
        }

        private void OnAuth(object sender, string playerHash)
        {
            PlayerHash = playerHash;

            if (_isFirstInit) return;

            _isFirstInit = true;
            PingUtil.Init();

            DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command, "OnAuth", "CommandHandler OnAuth Done");

            CoreEventHandlers.SuccessfullyLogined?.Invoke(null, null);
        }

        private async void OnPing(object sender, object packet)
        {
            await RequestAsync(PingPongHandler.Signature, isCritical: true);
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

            _requestHandlers.Add(TimeHandler.Signature, new TimeHandler());
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
        }

        public async Task Init()
        {
            _cancellationToken = new CancellationTokenSource();

            _tcpClient.SetEncryptionStatus(false);
            await _tcpClient.Init(GameService.CommandInfo, GameService.CommandInfo.Cipher);
        }

        internal static short GetPing()
        {
            return PingUtil.GetLastPing();
        }

        internal void Request(string handlerName, object payload = null, bool isCritical = false)
        {
            Send(_requestHandlers[handlerName]?.HandleAction(payload), isCritical);
        }

        internal async Task RequestAsync(string handlerName, object payload = null, bool isCritical = false,
            bool dontCheckAvailability = false)
        {
            await SendAsync(_requestHandlers[handlerName]?.HandleAction(payload), isCritical, dontCheckAvailability);
        }


        private void Send(Packet packet, bool isCritical = false)
        {
            if (!_observer.Increase(isCritical)) return;
            _tcpClient.Send(packet);
        }

        private async Task SendAsync(Packet packet, bool isCritical = false, bool dontCheckAvailability = false)
        {
            if (!_observer.Increase(isCritical)) return;
            if (IsAvailable()) await _tcpClient.SendAsync(packet);
            else if (!isCritical && !dontCheckAvailability)
                throw new GameServiceException("GameService Not Available")
                    .LogException<CommandHandler>(DebugLocation.Command, "SendAsync");
        }

        private void OnDataReceived(object sender, SocketDataReceived e)
        {
            try
            {
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