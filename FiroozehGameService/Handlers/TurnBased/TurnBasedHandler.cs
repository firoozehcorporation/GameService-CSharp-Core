using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FiroozehGameService.Core;
using FiroozehGameService.Core.Socket;
using FiroozehGameService.Handlers.TurnBased.RequestHandlers;
using FiroozehGameService.Handlers.TurnBased.ResponseHandlers;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased
{
    internal class TurnBasedHandler : IDisposable
    {
        #region TBHandlerRegion
        private static GsTcpClient _tcpClient;
        public static Room CurrentRoom;
        private readonly GsLiveSystemObserver _observer;
        private CancellationTokenSource _cancellationToken;
        private bool _isDisposed;
        
        public static string PlayerHash { private set; get; }
        public static string PlayToken => GameService.PlayToken;
        public static bool IsAvailable => _tcpClient?.IsAvailable ?? false;
        
        private readonly Dictionary<int, IResponseHandler> _responseHandlers =
            new Dictionary<int, IResponseHandler>();
        private readonly Dictionary<string, IRequestHandler> _requestHandlers =
            new Dictionary<string, IRequestHandler>();

        #endregion

        public TurnBasedHandler(StartPayload payload)
        {
            CurrentRoom = payload.Room;
            _tcpClient = new GsTcpClient(payload.Area);
            _tcpClient.SetType(GSLiveType.TurnBased);
            _tcpClient.DataReceived += OnDataReceived;
            _tcpClient.Error += OnError;            
            
            _cancellationToken = new CancellationTokenSource();
            _observer = new GsLiveSystemObserver(GSLiveType.TurnBased);
            _isDisposed = false;
            
            // Set Internal Event Handlers
            CoreEventHandlers.Ping += OnPing;
            CoreEventHandlers.Authorized += OnAuth;

            
            InitRequestMessageHandlers();
            InitResponseMessageHandlers();
            
            LogUtil.Log(this,"TurnBased Initialized");

        }

        private static void OnAuth(object sender, string playerHash)
        {
            if (sender.GetType() != typeof(AuthResponseHandler)) return;
            PlayerHash = playerHash;
            _tcpClient.UpdatePwd(playerHash);
            LogUtil.Log(null,"TurnBased OnAuth");
        }

        private async void OnPing(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(PingResponseHandler))
                await RequestAsync(PingPongHandler.Signature);
            LogUtil.Log(null,"TurnBased OnPing");
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
     
        
        public void Request(string handlerName, object payload = null)
            => Send(_requestHandlers[handlerName]?.HandleAction(payload));

        public async Task RequestAsync(string handlerName, object payload = null)
            => await SendAsync(_requestHandlers[handlerName]?.HandleAction(payload));


        
        
        
        public async Task Init()
        {
            _cancellationToken = new CancellationTokenSource();
            await _tcpClient.Init();
            Task.Run(async() => { await _tcpClient.StartReceiving(); }, _cancellationToken.Token);
            await RequestAsync(AuthorizationHandler.Signature);
        }
             
               
        
        
        private void Send(Packet packet)
        {
            if (!_observer.Increase()) return;
            _tcpClient.Send(packet);
        }
        
        private async Task SendAsync(Packet packet)
        {
            if (!_observer.Increase()) return;
            await _tcpClient.SendAsync(packet);
        }
        
        
        private async void OnError(object sender, ErrorArg e)
        {
            LogUtil.LogError(this,"TurnBased : " + e.Error);
           if(_isDisposed) return;
            await Init();
        }

        private void OnDataReceived(object sender, SocketDataReceived e)
        {
            var packet = JsonConvert.DeserializeObject<Packet>(e.Data);
            GameService.SynchronizationContext?.Send(delegate {
                _responseHandlers.GetValue(packet.Action)?.HandlePacket(packet);
            }, null);
            
        }
        
        public void Dispose()
        {
            _isDisposed = true;
            _tcpClient?.StopReceiving();
            _observer.Dispose();
            _cancellationToken.Cancel(true);
            CoreEventHandlers.Dispose?.Invoke(this,null);
            LogUtil.Log(this,"TurnBased Dispose");
        }


    }
}