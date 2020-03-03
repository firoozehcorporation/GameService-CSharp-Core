using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading;
using System.Threading.Tasks;
using FiroozehGameService.Core;
using FiroozehGameService.Core.Socket;
using FiroozehGameService.Handlers.Command.RequestHandlers;
using FiroozehGameService.Handlers.Command.ResponseHandlers;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command
{
    internal class CommandHandler : IDisposable
    {
        #region Fields
        private static GsTcpClient _tcpClient;
        private readonly GsLiveSystemObserver _observer;
        private readonly CancellationTokenSource _cancellationToken;
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

        public CommandHandler()
        {
            _tcpClient = new GsTcpClient(Models.Consts.Command.CommandArea);
            _tcpClient.SetType(GSLiveType.Core);
            _tcpClient.DataReceived += OnDataReceived;
            _tcpClient.Error += OnError;
            
            _cancellationToken = new CancellationTokenSource();
            _observer = new GsLiveSystemObserver(GSLiveType.Core);
            _isDisposed = false;
            _isFirstInit = false;
            
            // Set Internal Event Handlers
            CoreEventHandlers.Ping += OnPing;
            CoreEventHandlers.Authorized += OnAuth;

            InitRequestMessageHandlers();
            InitResponseMessageHandlers();
            
            LogUtil.Log(this,"CommandHandler Initialized");
        }

        private void OnAuth(object sender, string playerHash)
        {
           if (sender.GetType() != typeof(AuthResponseHandler)) return;
            PlayerHash = playerHash;
            _tcpClient.UpdatePwd(playerHash);
            LogUtil.Log(null,"CommandHandler OnAuth");

            if (_isFirstInit) return;
            CoreEventHandlers.SuccessfullyLogined?.Invoke(null, null);
            _isFirstInit = true;

        }

        private async void OnPing(object sender, EventArgs e)
        {
            if (sender.GetType() != typeof(PingResponseHandler)) return;
            await RequestAsync(PingPongHandler.Signature);
            LogUtil.Log(this,"CommandHandler OnPing");
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

        public async Task Init()
        {
            await _tcpClient.Init();
            Task.Run(async () => { await _tcpClient.StartReceiving(); }, _cancellationToken.Token);
            await RequestAsync(AuthorizationHandler.Signature);
            LogUtil.Log(this,"CommandHandler Init");
        }

        
        
        internal void Request(string handlerName, object payload = null)
            => Send(_requestHandlers[handlerName]?.HandleAction(payload));

        internal async Task RequestAsync(string handlerName, object payload = null)
            => await SendAsync(_requestHandlers[handlerName]?.HandleAction(payload));

        
        
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
            LogUtil.LogError(this,"CommandHandler OnError > " + e.Error + ", isDisposed : " + _isDisposed);
            if(_isDisposed) return;
            await Init();
        }

        private void OnDataReceived(object sender, SocketDataReceived e)
        {
            var packet = JsonConvert.DeserializeObject<Packet>(e.Data);

            GameService.SynchronizationContext?.Send(delegate {
                  LogUtil.Log(this,"CommandHandler OnDataReceived < " + e.Data);
                  _responseHandlers.GetValue(packet.Action)?.HandlePacket(packet);
               }, null);
            
        }

        public void Dispose()
        {
            _isDisposed = true;
            _tcpClient?.StopReceiving();
            _observer.Dispose();
            _cancellationToken.Cancel(true);
            LogUtil.Log(this,"CommandHandler Dispose");
         }
    }
}