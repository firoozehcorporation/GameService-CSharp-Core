using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FiroozehGameService.Core;
using FiroozehGameService.Core.Socket;
using FiroozehGameService.Handlers.TurnBased.RequestHandlers;
using FiroozehGameService.Handlers.TurnBased.ResponseHandlers;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased
{
    internal class TurnBasedHandler
    {
        #region TBHandlerRegion
        private static GsTcpClient _tcpClient;
        private Task _clientTask;
        public static Room CurrentRoom;
        private readonly CancellationTokenSource _cancellationToken;
        public static string PlayerHash;
        public static string RoomId => GameService.CurrentGame?._Id;
        public static string UserToken => GameService.UserToken;
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
            _tcpClient.DataReceived += OnDataReceived;
            _tcpClient.Error += OnError;
            _cancellationToken = new CancellationTokenSource();
            
            InitRequestMessageHandlers();
            InitResponseMessageHandlers();
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
     
        public async Task Request(string handlerName, object payload = null)
            => await Send(_requestHandlers[handlerName].HandleAction(payload));

        
        public async Task Init()
        {
            await _tcpClient.Init();
            await Request(AuthorizationHandler.Signature);
            _clientTask = Task.Run(async() => { await _tcpClient.StartReceiving(); }, _cancellationToken.Token);
        }
       
                
        public void Dispose()
        {
            _tcpClient.StopReceiving();
            _cancellationToken.Cancel(true);
        }
        
               
        private static async Task Send(Packet packet)
        {
            var json = JsonConvert.SerializeObject(packet);
            var data = Encoding.UTF8.GetBytes(json);
            await _tcpClient.Send(data);
        }
        
        
        private static void OnError(object sender, ErrorArg e)
        {
            // TODO Connect Again??
        }

        private void OnDataReceived(object sender, SocketDataReceived e)
        {
            var packet = JsonConvert.DeserializeObject<Packet>(e.Data);
            _responseHandlers.GetValue(packet.Action)?.HandlePacket(packet);
            /*switch (packet.Action)
            {
                    case TB.ActionAuth:
                        PlayerHash = packet.Token;
                        IsAvailable = true;
                        // TODO invoke OnSuccess()
                        break;
                    case TB.ActionPingPong:
                        await SendPingPong();
                        break;
                    case TB.OnJoin:
                        var join = JsonConvert.DeserializeObject<JoinData>(packet.Data);
                        // TODO Invoke OnJoin(join,(JoinType)join.JoinType)
                        break;
                    case TB.OnLeave:
                        var leave = JsonConvert.DeserializeObject<Leave>(packet.Data);
                        // TODO Invoke OnLeave(leave)
                        break;
                    case TB.GetUsers:
                        var members = JsonConvert.DeserializeObject<List<Member>>(packet.Data);
                        // TODO Invoke OnRoomMembersDetail(members)
                        break;
                    case TB.OnChooseNext:
                        var member = JsonConvert.DeserializeObject<Member>(packet.Data);
                        // TODO Invoke OnChooseNext(member)
                        break;
                    case TB.OnComplete:
                        var complete = JsonConvert.DeserializeObject<Complete>(packet.Data);
                        // TODO Invoke OnComplete(complete)
                        break;
                    case TB.OnFinish:
                        var finish = JsonConvert.DeserializeObject<Finish>(packet.Data);
                        // TODO Invoke OnFinish(finish)
                        break;
                    case TB.OnTakeTurn:
                        var turn = JsonConvert.DeserializeObject<Turn>(packet.Data);
                        // TODO Invoke OnTakeTurn(turn)
                        break;
                    case TB.Errors:
                        // TODO Invoke OnTurnBasedError(packet.message)
                        break;
            }
            */
        }

    }
}