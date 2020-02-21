using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using FiroozehGameService.Core;
using FiroozehGameService.Core.Socket;
using FiroozehGameService.Handlers.RealTime.RequestHandlers;
using FiroozehGameService.Handlers.RealTime.ResponseHandlers;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;
using Newtonsoft.Json;
using Packet = FiroozehGameService.Models.GSLive.RT.Packet;

namespace FiroozehGameService.Handlers.RealTime
{
    internal class RealTimeHandler : IDisposable
    {
        #region RTHandlerRegion
        private static GsUdpClient _udpClient;
        public static Room CurrentRoom;
        
        private readonly GsLiveSystemObserver _observer;
        private readonly CancellationTokenSource _cancellationToken;
        private bool _isDisposed;

        
        public static string PlayerHash { private set; get; }
        public static string PlayToken => GameService.PlayToken;
        public static bool IsAvailable => _udpClient?.IsAvailable ?? false;
        
        private readonly Dictionary<int, IResponseHandler> _responseHandlers =
            new Dictionary<int, IResponseHandler>();
        private readonly Dictionary<string, IRequestHandler> _requestHandlers =
            new Dictionary<string, IRequestHandler>();
        
        #endregion
        
        internal RealTimeHandler(StartPayload payload)
        {
            CurrentRoom = payload.Room;
            _udpClient = new GsUdpClient(payload.Area);
            _udpClient.DataReceived += OnDataReceived;
            _udpClient.Error += OnError;
            _udpClient.UpdatePwd(RT.Pwd);
            
            
            _cancellationToken = new CancellationTokenSource();
            _observer = new GsLiveSystemObserver(GSLiveType.RealTime);
            _isDisposed = false;
            
            // Set Internal Event Handlers
            CoreEventHandlers.Authorized += OnAuth;
            CoreEventHandlers.GProtocolConnected += OnConnected;
            
            InitRequestMessageHandlers();
            InitResponseMessageHandlers();
        }

        private void OnConnected(object sender, EventArgs e)
        {
            // Send Auth When Connected
            Request(AuthorizationHandler.Signature,GProtocolSendType.Reliable);
        }


        private static void OnAuth(object sender, string playerHash)
        {
            if (sender.GetType() == typeof(AuthResponseHandler))
                PlayerHash = playerHash;
        }

        private void InitRequestMessageHandlers()
        {
            var baseInterface = typeof(IRequestHandler);
            var subclassTypes = Assembly
                .GetAssembly(baseInterface)
                .GetTypes()
                .Where(t => t.GetInterfaces().Contains(baseInterface) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in subclassTypes)
            {
                var p = (string)type.GetProperty("Signature", BindingFlags.Public | BindingFlags.Static).GetValue(null);
                _requestHandlers.Add(p, (IRequestHandler)Activator.CreateInstance(type));
            }
        }
        private void InitResponseMessageHandlers()
        {
            var baseInterface = typeof(IResponseHandler);
            var subclassTypes = Assembly
                .GetAssembly(baseInterface)
                .GetTypes()
                .Where(t => t.GetInterfaces().Contains(baseInterface) && !t.IsInterface && !t.IsAbstract);

            foreach (var type in subclassTypes)
            {
                var p = (int)type.GetProperty("ActionCommand", BindingFlags.Public | BindingFlags.Static).GetValue(null);
                _responseHandlers.Add(p, (IResponseHandler)Activator.CreateInstance(type));
            }
        }
     

        internal void Request(string handlerName, GProtocolSendType type, object payload = null)
            => Send(_requestHandlers[handlerName]?.HandleAction(payload),type);
        
       
        internal static void Init()
        {
            _udpClient.Init();
        }
        
        
        private void Send(Packet packet,GProtocolSendType type)
        {
            if (!_observer.Increase()) return;
            _udpClient.Send(packet,type);
        }
        
              
        private void OnError(object sender, ErrorArg e)
        {
           if(_isDisposed) return;
           Init();
        }

        private void OnDataReceived(object sender, SocketDataReceived e)
        {
            var packet = JsonConvert.DeserializeObject<Packet>(e.Data);
          
            GameService.SynchronizationContext?.Send(delegate {
                _responseHandlers.GetValue(packet.Action)?.HandlePacket(packet,e.Type);
            }, null);
        }
        
        public void Dispose()
        {
            _udpClient?.StopReceiving();
            _observer.Dispose();
            _cancellationToken.Cancel(true);
            CoreEventHandlers.Dispose?.Invoke(this,null);
        }
        
    }
}