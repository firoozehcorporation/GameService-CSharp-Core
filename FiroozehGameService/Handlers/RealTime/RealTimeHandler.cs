using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FiroozehGameService.Core.Socket;
using FiroozehGameService.Handlers.RealTime.RequestHandlers;
using FiroozehGameService.Handlers.RealTime.ResponseHandlers;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.RT;
using Newtonsoft.Json;
using Leave = FiroozehGameService.Models.GSLive.Leave;
using Message = FiroozehGameService.Models.GSLive.Message;
using Packet = FiroozehGameService.Models.GSLive.RT.Packet;

namespace FiroozehGameService.Handlers.RealTime
{
    internal class RealTimeHandler
    {
        #region RTHandlerRegion
        private static GsUdpClient _udpClient;
        private Task _clientTask;
        public static Room CurrentRoom;
        private readonly CancellationTokenSource _cancellationToken;
        public static string PlayerHash;
        public static bool IsAvailable => _udpClient?.IsAvailable ?? false;
        
        private readonly Dictionary<int, IResponseHandler> _responseHandlers =
            new Dictionary<int, IResponseHandler>();

        private readonly Dictionary<string, IRequestHandler> _requestHandlers =
            new Dictionary<string, IRequestHandler>();
        
        #endregion
        
        public RealTimeHandler(StartPayload payload)
        {
            CurrentRoom = payload.Room;
            _udpClient = new GsUdpClient(payload.Area);
            _udpClient.DataReceived += OnDataReceived;
            _udpClient.Error += OnError;
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
        
        public void Dispose()
        {
            _udpClient.StopReceiving();
            _cancellationToken.Cancel(true);
        }
       
        public async Task Init()
        {
            await _udpClient.Init();
            await Request(AuthorizationHandler.Signature,null);
            _clientTask = Task.Run(async() => { await _udpClient.StartReceiving(); }, _cancellationToken.Token);
        }
        
            
        private static async Task Send(Packet packet)
        {
            var json = JsonConvert.SerializeObject(packet);
            var data = Encoding.UTF8.GetBytes(json);
            await _udpClient.Send(data);
        }
        
        
        private static void OnError(object sender, ErrorArg e)
        {
            // TODO Connect Again??
        }

        private void OnDataReceived(object sender, SocketDataReceived e)
        {
            var packet = JsonConvert.DeserializeObject<Packet>(e.Data);
            _responseHandlers.GetValue(packet.Action)?.HandlePacket(packet);

            /*
            switch (packet.Action)
            {
                case RT.ActionData:
                    var dataPayload = JsonConvert.DeserializeObject<DataPayload>(packet.Payload);
                    HandelData(dataPayload);
                    break;
                case RT.ActionStatus:
                    var statusPayload = JsonConvert.DeserializeObject<StatusPayload>(packet.Payload);
                    HandelStatus(statusPayload);
                    break;
                case RT.ActionPingPong:
                    await SendPingPong();
                    break;
            }
            */
        }


        private static void HandelData(DataPayload payload)
        {
            switch (payload.Action)
            {
                 case RT.OnJoin:
                     var join = JsonConvert.DeserializeObject<JoinData>(payload.Payload);
                     // TODO Invoke OnJoin(join,(JoinType)join.JoinType)
                     break;
                case RT.SendPublicMessage:
                    var publicMsg = JsonConvert.DeserializeObject<Message>(payload.Payload);
                    // TODO Invoke OnMessageReceive(publicMsg,MessageType.Public)
                    break;
                case RT.SendPrivateMessage:
                    var privateMsg = JsonConvert.DeserializeObject<Message>(payload.Payload);
                    // TODO Invoke OnMessageReceive(privateMsg,MessageType.Private)
                    break;
                case RT.OnMembersDetail:
                    var memberMsg = JsonConvert.DeserializeObject<Message>(payload.Payload);
                    var members = JsonConvert.DeserializeObject<List<Member>>(memberMsg.Data);
                    // TODO Invoke OnRoomMembersDetail(members)
                    break;
                case RT.OnLeave:
                    var leaveMsg = JsonConvert.DeserializeObject<Message>(payload.Payload);
                    var member = JsonConvert.DeserializeObject<Member>(leaveMsg.Data);
                    var leave = new Leave { RoomId = leaveMsg.RoomId , MemberLeave = member};
                    // TODO Invoke OnLeave(leave)
                    break;
            }
        }
        
        private void HandelStatus(StatusPayload payload)
        {
            if (payload.Status)
            {
                //IsAvailable = true;
                PlayerHash = payload.Message;
                // TODO invoke OnSuccess()
            }
            else
            {
                // TODO invoke OnRealTimeError("ServerError")
            }
                
        }
        
        
        
    }
}