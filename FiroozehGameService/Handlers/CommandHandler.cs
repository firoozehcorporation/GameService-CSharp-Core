using FiroozehGameService.Core;
using FiroozehGameService.Core.Socket;
using FiroozehGameService.Handlers.CommandServer_ResponseHandlers;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.EventArgs;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Reflection;
using System.Linq;

namespace FiroozehGameService.Handlers
{
    internal class CommandHandler : IDisposable
    {
        #region Fields
        private static GsTcpClient _tcpClient;
        private Task _clientTask;
        private readonly CancellationTokenSource _cancellationToken;

        //TODO move RT, TB and Command Handlers to another outer class
        private RTHandler _rtHandler;
        private TBHandler _tbHandler;

        public string PlayerHash { set; get; }

        public static string RoomId => GameService.CurrentGame?._Id;
        public static string UserToken => GameService.UserToken;
        public static string PlayToken => GameService.PlayToken;

        public static bool IsAvailable =>
            _tcpClient?.IsAvailable ?? false;

        private readonly Dictionary<int, ResponseHandler<CommandResponseArgs>> _responseHandlers =
             new Dictionary<int, ResponseHandler<CommandResponseArgs>>();

        private readonly Dictionary<string, IRequestHandler> _requestHandlers =
            new Dictionary<string, IRequestHandler>();
        #endregion

        public CommandHandler()
        {
            _tcpClient = new GsTcpClient(Command.CommandArea);
            _tcpClient.DataReceived += OnDataReceived;
            _tcpClient.Error += OnError;
            _cancellationToken = new CancellationTokenSource();

            InitRequestMessageHandlers();
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

        public async Task<bool> Init()
        {
            await _tcpClient.Init();
            //await Auth();
            _clientTask = Task.Run(async () => { await _tcpClient.StartReceiving(); }, _cancellationToken.Token);
            return true;
        }

        public void AddNewResponseHandler(ResponseHandler<CommandResponseArgs> responseHandler)
            => _responseHandlers.Add(responseHandler.ActionCommand, responseHandler);

        public void RemoveResponseHandler(int actionCommand)
            => _responseHandlers.Remove(actionCommand);

        public ResponseHandler<CommandResponseArgs> GetResponseHandler(int actionCommand)
            => _responseHandlers.GetValue(actionCommand);

        public async Task Request(string handlerName, object payload)
            => await Send(_requestHandlers[handlerName].HandleAction(payload));

        private static async Task Send(Packet packet)
        {
            var json = JsonConvert.SerializeObject(packet);
            var data = Encoding.UTF8.GetBytes(json);
            await _tcpClient.Send(data);
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
            _responseHandlers.GetValue(packet.Action)?.EventHandler?.Invoke(this,
                new CommandResponseArgs { CommandPacket = packet});
        }

        public void Dispose()
        {
            _tcpClient.StopReceiving();
            _cancellationToken.Cancel(true);
        }
    }
}