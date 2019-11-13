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
using FiroozehGameService.Models.GSLive.TB;
using Newtonsoft.Json;
using AuthPayload = FiroozehGameService.Models.Command.AuthPayload;

namespace FiroozehGameService.Handlers
{
    internal class TBHandler
    {
        #region TBHandlerRegion
        private static GsTcpClient _tcpClient;
        private Task _clientTask;
        private readonly CancellationTokenSource _cancellationToken;
        private string _playerHash;
        private static string RoomId => GameService.CurrentGame?._Id;
        private static string UserToken => GameService.UserToken;
        private static string PlayToken => GameService.PlayToken;
        public bool IsAvailable { get; private set; }
        #endregion

        
        public TBHandler(Area area)
        {
            _tcpClient = new GsTcpClient(area);
            _tcpClient.DataReceived += OnDataReceived;
            _tcpClient.Error += OnError;
            _cancellationToken = new CancellationTokenSource();
        }

       
        
        public async Task<bool> Init()
        {
            await _tcpClient.Init();
            await Auth();
            _clientTask = Task.Run(async() => { await _tcpClient.StartReceiving(); }, _cancellationToken.Token);
            IsAvailable = true;
            return true;
        }
        
        
        private async Task Auth()
        {
            if (_tcpClient.IsAvailable)
                await DoRequest(TB.ActionAuth, new AuthPayload(RoomId,UserToken));
        }
        
        public async Task LeaveRoom(string whoIsNext = null)
        {
            if(!_tcpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(TB.OnLeave,new DataPayload { NextId = whoIsNext });
            Dispose();
        }
        
        public async Task TakeTurn(string data = null,string whoIsNext = null)
        {
            if(!_tcpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(TB.OnTakeTurn,new DataPayload { Data = data , NextId = whoIsNext });
        }
        
        
        public async Task ChooseNext(string whoIsNext = null)
        {
            if(!_tcpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(TB.OnChooseNext,new DataPayload { NextId = whoIsNext });
        }
        
        
        public async Task Finish(Dictionary <string,Outcome> outcomes)
        {
            if(!_tcpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(TB.OnFinish,new DataPayload { Outcomes = outcomes });
        }
        
        
        public async Task Complete(string whoIsComplete)
        {
            if(!_tcpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(TB.OnComplete,new DataPayload { Id = whoIsComplete });
        }
        
        public async Task GetMembersDetail()
        {
            if(!_tcpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(TB.GetUsers);
        }
       
        
        private async Task SendPingPong()
        {
            await DoRequest(TB.ActionPingPong);
        }

                
        public void Dispose()
        {
            _tcpClient.StopReceiving();
            _cancellationToken.Cancel(true);
        }
        

        
        private async Task DoRequest(int action, Payload payload = null)
        {
            await RequestRoomFunctions(action,payload);
        }
        
        private async Task RequestRoomFunctions(int action, Payload payload = null,string message = null)
        {
            var packet = new Packet(_playerHash,action,JsonConvert.SerializeObject(payload),message);
            await Send(packet);
        }
       
        private static async Task Send(Packet packet)
        {
            var json = JsonConvert.SerializeObject(packet);
            var data = Encoding.UTF8.GetBytes(json);
            await _tcpClient.Send(data);
        }
        
        
        private static void OnError(object sender, ErrorArg e)
        {
            
        }

        private async void OnDataReceived(object sender, SocketDataReceived e)
        {
            var packet = JsonConvert.DeserializeObject<Packet>(e.Data);
            switch (packet.Action)
            {
                    case TB.ActionAuth:
                        _playerHash = packet.Token;
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
        }

    }
}