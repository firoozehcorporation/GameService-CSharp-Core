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
            _tcpClient = new GsTcpClient(Models.Consts.Command.CommandArea);
            _tcpClient.DataReceived += OnDataReceived;
            _tcpClient.Error += OnError;

            _cancellationToken = new CancellationTokenSource();
            _observer = new GsLiveSystemObserver(GSLiveType.Core);
            _isDisposed = false;
            _isFirstInit = false;


            InitRequestMessageHandlers();
            InitResponseMessageHandlers();

            // Set Internal Event Handlers
            CoreEventHandlers.Ping += OnPing;
            CoreEventHandlers.Authorized += OnAuth;

            LogUtil.Log(this, "CommandHandler Initialized with "
                              + _requestHandlers.Count + " Request Handlers & "
                              + _responseHandlers.Count + " Response Handlers");
        }

        public void Dispose()
        {
            _isDisposed = true;
            _isFirstInit = false;
            _tcpClient?.StopReceiving();
            _observer.Dispose();
            _cancellationToken.Cancel(false);
            LogUtil.Log(this, "CommandHandler Dispose");
        }

        private void OnAuth(object sender, string playerHash)
        {
            if (sender.GetType() != typeof(AuthResponseHandler)) return;
            PlayerHash = playerHash;
            LogUtil.Log(null, "CommandHandler OnAuth");

            if (_isFirstInit) return;
            _isFirstInit = true;
            CoreEventHandlers.SuccessfullyLogined?.Invoke(null, null);
        }

        private async void OnPing(object sender, APacket packet)
        {
            if (sender.GetType() != typeof(PingResponseHandler)) return;
            await RequestAsync(PingPongHandler.Signature);
            LogUtil.Log(this, "CommandHandler OnPing");
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
            if (_tcpClient.Init())
            {
                Task.Run(async () => { await _tcpClient.StartReceiving(); }, _cancellationToken.Token);
                await RequestAsync(AuthorizationHandler.Signature);
                LogUtil.Log(this, "CommandHandler Init done");
            }
            else
            {
                LogUtil.Log(this, "CommandHandler Init done With TimeOut");
            }
        }


        internal void Request(string handlerName, object payload = null)
        {
            Send(_requestHandlers[handlerName]?.HandleAction(payload));
        }

        internal async Task RequestAsync(string handlerName, object payload = null)
        {
            await SendAsync(_requestHandlers[handlerName]?.HandleAction(payload));
        }


        private void Send(Packet packet)
        {
            if (!_observer.Increase()) return;
            _tcpClient.Send(packet);
        }

        private async Task SendAsync(Packet packet)
        {
            if (!_observer.Increase()) return;
            if (IsAvailable) await _tcpClient.SendAsync(packet);
            else throw new GameServiceException("GameService Not Available");
        }

        private async void OnError(object sender, ErrorArg e)
        {
            LogUtil.LogError(this, "CommandHandler OnError > " + e.Error + ", isDisposed : " + _isDisposed);
            if (_isDisposed) return;
            await Init();
        }


        private void OnDataReceived(object sender, SocketDataReceived e)
        {
            try
            {
                var packet = (Packet) e.Packet;
                LogUtil.Log(this, "CommandHandler OnDataReceived < " + e.Packet);

                if (ActionUtil.IsInternalAction(packet.Action, GSLiveType.Core))
                    _responseHandlers.GetValue(packet.Action)?.HandlePacket(packet);
                else
                    GameService.SynchronizationContext?.Send(
                        delegate { _responseHandlers.GetValue(packet.Action)?.HandlePacket(packet); }, null);
            }
            catch (Exception exception)
            {
                LogUtil.LogError(this, "CommandHandler OnDataReceived ERR : " + exception);
            }
        }

        #region Fields

        private static GsTcpClient _tcpClient;
        private readonly GsLiveSystemObserver _observer;
        private CancellationTokenSource _cancellationToken;
        private bool _isDisposed;
        private bool _isFirstInit;

        public static string PlayerHash { private set; get; }

        public static string GameId => GameService.CurrentGame?._Id;
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