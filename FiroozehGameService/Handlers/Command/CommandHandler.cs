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
using FiroozehGameService.Handlers.Command.RequestHandlers.Voice;
using FiroozehGameService.Handlers.Command.ResponseHandlers;
using FiroozehGameService.Handlers.Command.ResponseHandlers.Chat;
using FiroozehGameService.Handlers.Command.ResponseHandlers.Voice;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;
using FiroozehGameService.Utils.Encryptor;

namespace FiroozehGameService.Handlers.Command
{
    internal class CommandHandler
    {
        internal CommandHandler()
        {
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

                IsInitializing = false;

                _retryConnectCounter = 0;
                _retryPingCounter = 0;

                _cancellationToken?.Cancel(false);
                _observer?.Dispose();
                PingUtil.Dispose();

                _connGateway?.StopReceiving(isGraceful);
            }
            catch (Exception)
            {
                // ignored
            }
            finally
            {
                _connGateway = null;
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
            finally
            {
                _retryPingCounter = 0;
                _isPingRequested = false;
            }
        }

        private async void RequestPing(object sender, EventArgs e)
        {
            if (_isPingRequested && _retryPingCounter >= 3)
            {
                DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command, "RequestPing",
                    "CommandHandler -> Server Not Response Ping, Reconnecting...");

                _isPingRequested = false;
                _retryPingCounter = 0;

                IsInitializing = false;

                Init();
                return;
            }

            var currentTime = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds();
            _isPingRequested = true;
            _retryPingCounter++;

            await RequestAsync(MirrorHandler.Signature, currentTime);
        }

        private void OnGsTcpClientError(object sender, GameServiceException exception)
        {
            if (_isDisposed) return;

            _retryConnectCounter++;

            DebugUtil.LogError<CommandHandler>(DebugLocation.Command, "OnGsTcpClientError",
                "CommandHandler Reconnect Retry " + _retryConnectCounter + " , Wait to Connect...");

            _isPingRequested = false;
            _retryPingCounter = 0;

            IsInitializing = false;

            Init();
        }

        private async void OnGsTcpClientConnected(object sender, object e)
        {
            DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command, "OnGsTcpClientConnected",
                "CommandHandler -> Connected,Waiting for Handshakes...");

            _retryConnectCounter = 0;

            _connGateway?.StartReceiving();

            await RequestAsync(AuthorizationHandler.Signature);

            DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command, "OnGsTcpClientConnected",
                "CommandHandler Init done");
        }

        private void OnAuth(object sender, string playerHash)
        {
            DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command, "OnAuth", "CommandHandler OnAuth Done");

            IsInitializing = false;
            PlayerHash = playerHash;
            _connGateway?.StartSending();
            PingUtil.Start();

            if (_isFirstInit) return;
            _isFirstInit = true;

            try
            {
                if (GameService.HandlerType == EventHandlerType.NativeContext)
                    CoreEventHandlers.SuccessfullyLogined?.Invoke(null, null);
                else
                    GameService.SynchronizationContext?.Send(
                        delegate { CoreEventHandlers.SuccessfullyLogined?.Invoke(null, null); }, null);
            }
            catch (Exception)
            {
                // ignored
            }
        }

        private async void OnPing(object sender, object packet)
        {
            await RequestAsync(PingPongHandler.Signature);
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
            _requestHandlers.Add(SendChannelPrivateMessageHandler.Signature, new SendChannelPrivateMessageHandler());
            _requestHandlers.Add(SendChannelPublicMessageHandler.Signature, new SendChannelPublicMessageHandler());
            _requestHandlers.Add(SubscribeChannelHandler.Signature, new SubscribeChannelHandler());
            _requestHandlers.Add(UnsubscribeChannelHandler.Signature, new UnsubscribeChannelHandler());

            _requestHandlers.Add(MirrorHandler.Signature, new MirrorHandler());

            _requestHandlers.Add(RemoveChatHandler.Signature, new RemoveChatHandler());
            _requestHandlers.Add(RemoveChatsHandler.Signature, new RemoveChatsHandler());
            _requestHandlers.Add(RemoveAllChatsHandler.Signature, new RemoveAllChatsHandler());
            _requestHandlers.Add(RemoveMemberChatsHandler.Signature, new RemoveMemberChatsHandler());

            _requestHandlers.Add(EditChatHandler.Signature, new EditChatHandler());

            _requestHandlers.Add(GetPrivateRecentMessagesRequestHandler.Signature,
                new GetPrivateRecentMessagesRequestHandler());

            _requestHandlers.Add(PushEventHandler.Signature, new PushEventHandler());
            _requestHandlers.Add(BufferedPushEventsHandler.Signature, new BufferedPushEventsHandler());
            _requestHandlers.Add(GetRoomsInfoHandler.Signature, new GetRoomsInfoHandler());
            _requestHandlers.Add(EditRoomHandler.Signature, new EditRoomHandler());

            _requestHandlers.Add(CreateVoiceChannelHandler.Signature, new CreateVoiceChannelHandler());
            _requestHandlers.Add(JoinVoiceChannelHandler.Signature, new JoinVoiceChannelHandler());
            _requestHandlers.Add(LeaveVoiceChannelHandler.Signature, new LeaveVoiceChannelHandler());
            _requestHandlers.Add(DestroyVoiceChannelHandler.Signature, new DestroyVoiceChannelHandler());
            _requestHandlers.Add(DeafenVoiceChannelHandler.Signature, new DeafenVoiceChannelHandler());
            _requestHandlers.Add(MuteLocalVoiceChannelHandler.Signature, new MuteLocalVoiceChannelHandler());
            _requestHandlers.Add(GetVoiceChannelInfoHandler.Signature, new GetVoiceChannelInfoHandler());
            _requestHandlers.Add(KickMemberVoiceChannelHandler.Signature, new KickMemberVoiceChannelHandler());
            _requestHandlers.Add(OfferVoiceChannelHandler.Signature, new OfferVoiceChannelHandler());
            _requestHandlers.Add(TrickleVoiceChannelHandler.Signature, new TrickleVoiceChannelHandler());
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
            _responseHandlers.Add(PrivateChatResponseHandler.ActionCommand, new PrivateChatResponseHandler());
            _responseHandlers.Add(PublicChatResponseHandler.ActionCommand, new PublicChatResponseHandler());
            _responseHandlers.Add(SubscribeChannelResponseHandler.ActionCommand, new SubscribeChannelResponseHandler());
            _responseHandlers.Add(UnSubscribeChannelResponseHandler.ActionCommand,
                new UnSubscribeChannelResponseHandler());

            _responseHandlers.Add(MirrorResponseHandler.ActionCommand,
                new MirrorResponseHandler());

            _responseHandlers.Add(RemoveChatResponseHandler.ActionCommand, new RemoveChatResponseHandler());
            _responseHandlers.Add(RemoveChatsResponseHandler.ActionCommand, new RemoveChatsResponseHandler());
            _responseHandlers.Add(RemoveAllChatResponseHandler.ActionCommand, new RemoveAllChatResponseHandler());

            _responseHandlers.Add(RemoveMemberChatsResponseHandler.ActionCommand,
                new RemoveMemberChatsResponseHandler());

            _responseHandlers.Add(EditChatResponseHandler.ActionCommand, new EditChatResponseHandler());

            _responseHandlers.Add(PrivateRecentMessagesResponseHandler.ActionCommand,
                new PrivateRecentMessagesResponseHandler());

            _responseHandlers.Add(PushEventResponseHandler.ActionCommand, new PushEventResponseHandler());
            _responseHandlers.Add(BufferedPushEventsResponseHandler.ActionCommand,
                new BufferedPushEventsResponseHandler());
            _responseHandlers.Add(GetRoomsInfoResponseHandler.ActionCommand,
                new GetRoomsInfoResponseHandler());
            _responseHandlers.Add(EditRoomResponseHandler.ActionCommand,
                new EditRoomResponseHandler());

            _responseHandlers.Add(CreateVoiceChannelResponseHandler.ActionCommand,
                new CreateVoiceChannelResponseHandler());
            _responseHandlers.Add(JoinVoiceChannelResponseHandler.ActionCommand, new JoinVoiceChannelResponseHandler());
            _responseHandlers.Add(LeaveVoiceChannelResponseHandler.ActionCommand,
                new LeaveVoiceChannelResponseHandler());
            _responseHandlers.Add(DestroyVoiceChannelResponseHandler.ActionCommand,
                new DestroyVoiceChannelResponseHandler());
            _responseHandlers.Add(DestroyVoiceChannelResponseHandler.ActionCommand,
                new DestroyVoiceChannelResponseHandler());
            _responseHandlers.Add(MuteVoiceChannelResponseHandler.ActionCommand, new MuteVoiceChannelResponseHandler());
            _responseHandlers.Add(GetVoiceChannelInfoResponseHandler.ActionCommand,
                new GetVoiceChannelInfoResponseHandler());
            _responseHandlers.Add(KickMemberVoiceChannelResponseHandler.ActionCommand,
                new KickMemberVoiceChannelResponseHandler());
            _responseHandlers.Add(AnswerVoiceChannelResponseHandler.ActionCommand,
                new AnswerVoiceChannelResponseHandler());
            _responseHandlers.Add(TrickleVoiceChannelResponseHandler.ActionCommand,
                new TrickleVoiceChannelResponseHandler());
            _responseHandlers.Add(VoiceErrorResponseHandler.ActionCommand, new VoiceErrorResponseHandler());
        }

        public void Init()
        {
            if (_connGateway == null)
            {
                switch (GameService.CommandInfo.Protocol)
                {
                    case GsEncryptor.TcpSec:
                        _connGateway = new GsTcpClient();
                        break;
                    case GsEncryptor.WebSocketSec:
                        _connGateway = new GsWebSocketClient();
                        break;
                }

                if (_connGateway != null) _connGateway.DataReceived += OnDataReceived;
            }

            _cancellationToken = new CancellationTokenSource();


            if (IsInitializing) return;

            IsInitializing = true;
            PingUtil.Stop();
            _connGateway?.Init(GameService.CommandInfo, GameService.CommandInfo.Cipher);
        }

        internal static short GetPing()
        {
            return PingUtil.GetLastPing();
        }

        private async Task RequestAsync(string handlerName, object payload = null)
        {
            await SendAsync(_requestHandlers[handlerName]?.HandleAction(payload));
        }

        internal void Send(string handlerName, object payload = null)
        {
            AddToSendQueue(_requestHandlers[handlerName]?.HandleAction(payload));
        }


        private static async Task SendAsync(Packet packet)
        {
            if (IsAvailable()) await _connGateway.SendAsync(packet);
        }

        private void AddToSendQueue(Packet packet)
        {
            if (!_observer.Increase(false))
                throw new GameServiceException("Too Many Requests, You Can Send " + CommandConst.CommandLimit +
                                               " Requests Per Second")
                    .LogException<CommandHandler>(DebugLocation.Command, "AddToSendQueue");

            if (!_isDisposed) _connGateway.AddToSendQueue(packet);
            else
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
            return _connGateway != null && _connGateway.IsConnected();
        }


        #region Fields

        private static GTcpClient _connGateway;
        private readonly GsLiveSystemObserver _observer;
        private CancellationTokenSource _cancellationToken;
        private int _retryConnectCounter;
        private int _retryPingCounter;
        private bool _isDisposed;
        private bool _isFirstInit;
        private bool _isPingRequested;

        internal static bool IsInitializing;

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