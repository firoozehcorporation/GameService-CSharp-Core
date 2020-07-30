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
            _pingUtil = new PingUtil();
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

            LogUtil.Log(this, "RealTime init");
        }

        
        private void SendObserverEventHandler(object sender, byte[] data)
        {
            Request(ObserverHandler.Signature,GProtocolSendType.UnReliable,data);
        }

        private static void OnMemberId(object sender, string id)
        {
            MemberId = id;
        }

        public void Dispose()
        {
            _udpClient?.StopReceiving();
            _observer?.Dispose();
            _pingUtil?.Dispose();
            
            ObserverCompacterUtil.Dispose();
            LogUtil.Log(this, "RealTime Dispose");

            PlayerHash = null;
            _isDisposed = true;
            GsSerializer.CurrentPlayerLeftRoom?.Invoke(this,null);
            CoreEventHandlers.Dispose?.Invoke(this, null);
        }


        private void RequestPing(object sender, EventArgs e)
        {
            Request(GetPingHandler.Signature, GProtocolSendType.Reliable,isCritical : true);
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
            LogUtil.Log(this, "Ping Realtime : " + diff);
        }

        private void OnConnected(object sender, EventArgs e)
        {
            // Send Auth When Connected
            Request(AuthorizationHandler.Signature, GProtocolSendType.Reliable,isCritical : true);
        }


        private void OnAuth(object sender, string playerHash)
        {
            if (sender.GetType() != typeof(AuthResponseHandler)) return;
            PlayerHash = playerHash;
            LogUtil.Log(null, "RealTime OnAuth");
            
            _pingUtil?.Init();
            ObserverCompacterUtil.Init();

            // Get SnapShot After Auth
            Request(SnapShotHandler.Signature,GProtocolSendType.Reliable,isCritical : true);
            
            GsSerializer.CurrentPlayerJoinRoom?.Invoke(this,null);
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
            _requestHandlers.Add(GetPingHandler.Signature, new GetPingHandler());
            _requestHandlers.Add(SendPrivateMessageHandler.Signature, new SendPrivateMessageHandler());
            _requestHandlers.Add(SendPublicMessageHandler.Signature, new SendPublicMessageHandler());
            _requestHandlers.Add(NewEventHandler.Signature, new NewEventHandler());
            _requestHandlers.Add(SnapShotHandler.Signature, new SnapShotHandler());
            _requestHandlers.Add(ObserverHandler.Signature, new ObserverHandler());
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
            _responseHandlers.Add(PingResponseHandler.ActionCommand, new PingResponseHandler());
            _responseHandlers.Add(MemberDetailsResponseHandler.ActionCommand, new MemberDetailsResponseHandler());
            _responseHandlers.Add(PrivateMessageResponseHandler.ActionCommand, new PrivateMessageResponseHandler());
            _responseHandlers.Add(PublicMessageResponseHandler.ActionCommand, new PublicMessageResponseHandler());
            _responseHandlers.Add(NewEventResponseHandler.ActionCommand, new NewEventResponseHandler());
            _responseHandlers.Add(SnapShotResponseHandler.ActionCommand, new SnapShotResponseHandler());
            _responseHandlers.Add(ObserverResponseHandler.ActionCommand, new ObserverResponseHandler());
        }


        internal void Request(string handlerName, GProtocolSendType type, object payload = null,bool isCritical = false)
        {
            Send(_requestHandlers[handlerName]?.HandleAction(payload), type,isCritical);
        }


        internal static void Init()
        {
            _udpClient.Init();
        }


        private void Send(Packet packet, GProtocolSendType type,bool isCritical = false)
        {
            if (!_observer.Increase(isCritical)) return;
            if (!PacketUtil.CheckPacketSize(packet)) throw new GameServiceException("this Packet Is Too Big!");
            if (IsAvailable) _udpClient.Send(packet, type);
            else throw new GameServiceException("GameService Not Available");
        }
        

        private void OnError(object sender, ErrorArg e)
        {
            LogUtil.Log(this, "RealTime Error");
            Dispose();
            //if (_isDisposed) return;
            //Init();
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
                    LogUtil.Log(this, "RealtimeHandler OnDataReceived < " + packet);
                    _responseHandlers.GetValue(packet.Action)?.HandlePacket(packet, packet.SendType);
                }, null);
            }
            catch (Exception exception)
            {
                LogUtil.LogError(this, "RealtimeHandler OnDataReceived ERR : " + exception);
            }
        }


        #region RTHandlerRegion

        private static GsUdpClient _udpClient;
        public static Room CurrentRoom;

        private readonly GsLiveSystemObserver _observer;
        private readonly PingUtil _pingUtil;
        private bool _isDisposed;
        
        
        public static string MemberId { private set; get; }
        public static string PlayerHash { private set; get; }
        public static string PlayToken => GameService.PlayToken;
        public static bool IsAvailable => _udpClient?.IsAvailable ?? false;

        private readonly Dictionary<int, IResponseHandler> _responseHandlers =
            new Dictionary<int, IResponseHandler>();

        private readonly Dictionary<string, IRequestHandler> _requestHandlers =
            new Dictionary<string, IRequestHandler>();

        #endregion
    }
}