using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FiroozehGameService.Core;
using FiroozehGameService.Core.Socket;
using FiroozehGameService.Handlers.Command.RequestHandlers;
using FiroozehGameService.Handlers.Command.ResponseHandlers;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.EventArgs;
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
            _tcpClient.DataReceived += OnDataReceived;
            _tcpClient.Error += OnError;
            _cancellationToken = new CancellationTokenSource();
            _observer = new GsLiveSystemObserver(GSLiveType.Core);
            
            // Set Internal Event Handlers
            CoreEventHandlers.OnPing += OnPing;
            CoreEventHandlers.OnAuth += OnAuth;

            InitRequestMessageHandlers();
            InitResponseMessageHandlers();
        }

        private static void OnAuth(object sender, string playerHash)
        {
            if (sender.GetType() != typeof(AuthResponseHandler)) return;
            PlayerHash = playerHash;
            CoreEventHandlers.SuccessfulLogin?.Invoke(null,null);
        }

        private void OnPing(object sender, EventArgs e)
        {
            if (sender.GetType() == typeof(PingResponseHandler))
                Request(PingPongHandler.Signature);
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
            Request(AuthorizationHandler.Signature);
        }

        public void Request(string handlerName, object payload = null)
            => Send(_requestHandlers[handlerName]?.HandleAction(payload));

        private void Send(Packet packet)
        {
            if (!_observer.Increase()) return;
            var json = JsonConvert.SerializeObject(packet , new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
            var data = Encoding.UTF8.GetBytes(json);
            _tcpClient.Send(data);
        }

        private async void OnError(object sender, ErrorArg e)
        {
            // Must Connect Again ...
            Dispose();
            await Init();
        }

        private void OnDataReceived(object sender, SocketDataReceived e)
        {
             var packet = JsonConvert.DeserializeObject<Packet>(e.Data);
            _responseHandlers.GetValue(packet.Action)?.HandlePacket(packet);
        }

        public void Dispose()
        {
            _tcpClient?.StopReceiving();
            _observer.Dispose();
            _cancellationToken.Cancel(true);
            
            CoreEventHandlers.OnPing = null;
            CoreEventHandlers.OnAuth = null;
        }
    }
}