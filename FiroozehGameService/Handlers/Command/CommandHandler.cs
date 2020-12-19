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
using System.Net.Sockets;
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
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;

namespace FiroozehGameService.Handlers.Command
{
    internal class CommandHandler : IDisposable
    {
        public CommandHandler()
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
            CoreEventHandlers.Ping += OnPing;
            CoreEventHandlers.Authorized += OnAuth;
            CoreEventHandlers.OnGsTcpClientConnected += OnGsTcpClientConnected;
            CoreEventHandlers.OnGsTcpClientError += OnGsTcpClientError;

            DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command,"Constructor","CommandHandler Initialized");
        }
        
        private async void OnGsTcpClientError(object sender, GameServiceException exception)
        {
            if((GSLiveType) sender != GSLiveType.Command) return;
            if (_isDisposed) return;

            exception.LogException<CommandHandler>(DebugLocation.Command, "OnGsTcpClientError");
            
            _retryConnectCounter++;
            
            DebugUtil.LogError<CommandHandler>(DebugLocation.Command,"OnGsTcpClientError","CommandHandler Reconnect Retry " + _retryConnectCounter + " , Wait to Connect...");

            await Init();
        }

        private async void OnGsTcpClientConnected(object sender, TcpClient e)
        {
            if((GSLiveType) sender != GSLiveType.Command) return;
            
            DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command,"OnGsTcpClientConnected","CommandHandler -> Connected,Waiting for Handshakes...");

            Task.Run(async () => { await _tcpClient.StartReceiving(); }, _cancellationToken.Token);
            await RequestAsync(AuthorizationHandler.Signature, isCritical: true);
            _retryConnectCounter = 0;
            
            DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command,"OnGsTcpClientConnected","CommandHandler Init done");
        }

        public void Dispose()
        {
            _isDisposed = true;
            _isFirstInit = false;
            _tcpClient?.StopReceiving();
            _observer?.Dispose();
            _cancellationToken?.Cancel(false);
            DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command,"Dispose","CommandHandler Dispose Done");
        }

        private void OnAuth(object sender, object playerHash)
        {
            if (sender.GetType() != typeof(AuthResponseHandler)) return;
            PlayerHash = (string) playerHash;
            
            if (_isFirstInit) return;
            _isFirstInit = true;
            
            DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command,"OnAuth","CommandHandler Auth Done");
            
            CoreEventHandlers.SuccessfullyLogined?.Invoke(null, null);
        }

        private async void OnPing(object sender, APacket packet)
        {
            if (sender.GetType() != typeof(PingResponseHandler)) return;
            await RequestAsync(PingPongHandler.Signature, isCritical: true);
            DebugUtil.LogNormal<CommandHandler>(DebugLocation.Command,"OnPing","CommandHandler Ping Called");
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
        }

        public async Task Init()
        {
            _cancellationToken = new CancellationTokenSource();
            await _tcpClient.Init(GameService.CommandInfo);
        }


        internal void Request(string handlerName, object payload = null, bool isCritical = false)
        {
            Send(_requestHandlers[handlerName]?.HandleAction(payload), isCritical);
        }

        internal async Task RequestAsync(string handlerName, object payload = null, bool isCritical = false,bool dontCheckAvailability = false)
        {
            await SendAsync(_requestHandlers[handlerName]?.HandleAction(payload), isCritical,dontCheckAvailability);
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
                .LogException<CommandHandler>(DebugLocation.Command,"SendAsync");
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
                exception.LogException<CommandHandler>(DebugLocation.Command,"OnDataReceived");
            }
        }

        #region Fields

        private static GsTcpClient _tcpClient;
        private readonly GsLiveSystemObserver _observer;
        private CancellationTokenSource _cancellationToken;
        private int _retryConnectCounter;
        private bool _isDisposed;
        private bool _isFirstInit;

        public static string PlayerHash { private set; get; }

        public static string GameId => GameService.CurrentInternalGame?._Id;
        public static string UserToken => GameService.UserToken;

        public static bool IsAvailable =>
            _tcpClient?.IsAvailable ?? false;

        private readonly Dictionary<int, IResponseHandler> _responseHandlers =
            new Dictionary<int, IResponseHandler>();

        private readonly Dictionary<string, IRequestHandler> _requestHandlers =
            new Dictionary<string, IRequestHandler>();

        #endregion
    }
}