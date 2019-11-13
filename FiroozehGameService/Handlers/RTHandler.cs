using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FiroozehGameService.Core;
using FiroozehGameService.Core.Socket;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.RT;
using Newtonsoft.Json;
using AuthPayload = FiroozehGameService.Models.GSLive.RT.AuthPayload;
using Leave = FiroozehGameService.Models.GSLive.Leave;
using Message = FiroozehGameService.Models.GSLive.Message;
using Packet = FiroozehGameService.Models.GSLive.RT.Packet;

namespace FiroozehGameService.Handlers
{
    internal class RTHandler
    {
        #region RTHandlerRegion
        private static GsUdpClient _udpClient;
        private Task _clientTask;
        private readonly CancellationTokenSource _cancellationToken;
        private string _playerHash;
        private static string RoomId => GameService.CurrentGame?._Id;
        private static string UserToken => GameService.UserToken;
        private static string PlayToken => GameService.PlayToken;
        public bool IsAvailable { get; private set; }
        #endregion
        
        public RTHandler(Area area)
        {
            _udpClient = new GsUdpClient(area);
            _udpClient.DataReceived += OnDataReceived;
            _udpClient.Error += OnError;
            _cancellationToken = new CancellationTokenSource();
        }

     

        
        public void Dispose()
        {
            _udpClient.StopReceiving();
            _cancellationToken.Cancel(true);
        }
        
        public async Task<bool> Init()
        {
            await _udpClient.Init();
            await Auth();
            _clientTask = Task.Run(async() => { await _udpClient.StartReceiving(); }, _cancellationToken.Token);
            IsAvailable = true;
            return true;
        }


        private static async Task Auth()
        {
            if (_udpClient.IsAvailable)
                await DoRequest(new AuthPayload(RoomId,UserToken),RT.ActionAuth);
        }
        
        private async Task SendPingPong()
        {
            await DoRequest(new PingPongPayload(RoomId,_playerHash), RT.ActionPingPong);
        }
        
        
        public async Task LeaveRoom()
        {
            if(!_udpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(new DataPayload(RT.OnLeave,RoomId,_playerHash),RT.ActionData);
            Dispose();
        }
        
        public async Task SendPublicMessage(string message)
        {
            if(!_udpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(new DataPayload(RT.SendPublicMessage,RoomId,_playerHash,payload:message),RT.ActionData);
        }

        public async Task SendPrivateMessage(string receiverId,string message)
        {
            if(!_udpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(new DataPayload(RT.SendPrivateMessage,RoomId,_playerHash,receiverId,message),RT.ActionData);
        }
        
        
        public async Task GetMembersDetail()
        {
            if(!_udpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(new DataPayload(RT.OnMembersDetail,RoomId,_playerHash),RT.ActionData);
        }


        

        private static async Task DoRequest(Payload payload , int action)
        {
            await RequestRoomFunctions(action,payload);
        }
        
        private static async Task RequestRoomFunctions(int action, Payload payload = null)
        {
            var packet = new Packet(action,JsonConvert.SerializeObject(payload));
            await Send(packet);
        }
       
        private static async Task Send(Packet packet)
        {
            var json = JsonConvert.SerializeObject(packet);
            var data = Encoding.UTF8.GetBytes(json);
            await _udpClient.Send(data);
        }
        
        
        private static void OnError(object sender, ErrorArg e)
        {
            
        }

        private async void OnDataReceived(object sender, SocketDataReceived e)
        {
            var packet = JsonConvert.DeserializeObject<Packet>(e.Data);

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
                IsAvailable = true;
                _playerHash = payload.Message;
                // TODO invoke OnSuccess()
            }
            else
            {
                // TODO invoke OnRealTimeError("ServerError")
            }
                
        }
        
        
        
    }
}