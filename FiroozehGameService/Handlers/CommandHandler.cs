using System.Collections.Generic;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using FiroozehGameService.Core;
using FiroozehGameService.Core.GSLive;
using FiroozehGameService.Core.Socket;
using FiroozehGameService.Models;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.EventArgs;
using FiroozehGameService.Models.GSLive;
using Newtonsoft.Json;
using Invite = FiroozehGameService.Models.GSLive.Invite;
using Message = FiroozehGameService.Models.Command.Message;

namespace FiroozehGameService.Handlers
{
    internal class CommandHandler
    {
        #region CommandHandlerRegion
        private static GsTcpClient _tcpClient;
        private Task _clientTask;
        private readonly CancellationTokenSource _cancellationToken;
        
        private RTHandler _rtHandler;
        private TBHandler _tbHandler;
        
        private string _playerHash;
        
        private static string RoomId => GameService.CurrentGame?._Id;
        private static string UserToken => GameService.UserToken;
        private static string PlayToken => GameService.PlayToken;
        
        public bool IsAvailable { get; private set; }
        #endregion
        
        public CommandHandler()
        {
            _tcpClient = new GsTcpClient(Command.CommandArea);
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

        public void Dispose()
        {
            _tcpClient.StopReceiving();
            _cancellationToken.Cancel(true);
        }


        private async Task Auth()
        {
            if (_tcpClient.IsAvailable)
                await DoRequest(new AuthPayload(RoomId,UserToken));
        }
        
        public async Task AutoMatch(GSLiveOption.AutoMatchOption option,RoomType type)
        {
            if(!_tcpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(option, Command.ActionAutoMatch, type);
        }
        
        
        public async Task CreateRoom(GSLiveOption.CreateRoomOption option,RoomType type)
        {
            if(!_tcpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(option, Command.ActionCreateRoom, type);
        }
        
        
        public async Task JoinRoom(string roomId)
        {
            if(!_tcpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(new RoomDetail { Id = roomId },Command.ActionJoinRoom);
        }
        
        
        public async Task GetAvailableRoom(string role,RoomType type)
        {
            if(!_tcpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(new RoomDetail { Role = role },Command.ActionGetRooms, type);
        }
        
        
        public async Task SubscribeChannel(string channelName)
        {
            if(!_tcpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(Command.ActionSubscribe,message:channelName);
        }
        
        
        public async Task UnSubscribeChannel(string channelName)
        {
            if(!_tcpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(Command.ActionUnSubscribe,message:channelName);
        }
        
        
        public async Task SendChannelMessage(string channelName, string data)
        {
            if(!_tcpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            var msg = new Message(false,channelName,null,data);
            await DoRequest(Command.ActionChat,JsonConvert.SerializeObject(msg));
        }

      
        public async Task GetInviteList(RoomType type)
        {
            if(!_tcpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(new RoomDetail { GsLiveType =(int) type },Command.ActionGetInviteList);
        }
        
        
        public async Task InviteUser(string roomId, string userId,RoomType type)
        {
            if(!_tcpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(new RoomDetail { Id = roomId , User = userId , GsLiveType = (int) type },Command.ActionInviteUser);
        }
        
        
        public async Task AcceptInvite(string inviteId,RoomType type)
        {
            if(!_tcpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(new RoomDetail { Invite = inviteId, GsLiveType = (int) type },Command.ActionAcceptInvite);
        }
        
        
        public async Task FindUser(string query, int limit,RoomType type)
        {
            if(!_tcpClient.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            await DoRequest(new RoomDetail { User = query , Max = limit },Command.ActionAcceptInvite);
        }




        private async Task SendPingPong()
        {
            await DoRequest(Command.ActionPing);
        }
        
        
        private async Task DoRequest(GSLiveOption.CreateRoomOption option,int action,RoomType type)
        {
            var roomDetail = new RoomDetail
            {
                Name = option.RoomName,
                Role = option.Role,
                Min = option.MinPlayer,
                Max = option.MaxPlayer,
                IsPrivate = option.IsPrivate,
                Type = action,
                GsLiveType = (int) type
            };
            await RequestRoomFunctions(action, roomDetail);
        }

        private async Task DoRequest(GSLiveOption.AutoMatchOption option,int action,RoomType type)
        {
            var roomDetail = new RoomDetail
            {
                Role = option.Role,
                Min = option.MinPlayer,
                Max = option.MaxPlayer,
                Type = action,
                GsLiveType = (int) type
            };
            await RequestRoomFunctions(action, roomDetail);
        }

        private async Task DoRequest(AuthPayload authPayload)
        {
            var packet = new Packet(null,Command.ActionAuth,JsonConvert.SerializeObject(authPayload));
            await Send(packet);
        }
        
        private async Task DoRequest(RoomDetail detail,int action,RoomType type)
        {
            detail.GsLiveType = (int) type;
            await RequestRoomFunctions(action, detail);
        }

        private async Task DoRequest(RoomDetail detail,int action)
        {
            await RequestRoomFunctions(action, detail);
        }

        private async Task DoRequest(int action ,string data = null ,string message = null)
        {
            var packet = new Packet(_playerHash,action,data,message);
            await Send(packet);
        }

        
        private async Task RequestRoomFunctions(int action, RoomDetail detail = null)
        {
            var packet = new Packet(_playerHash,action,JsonConvert.SerializeObject(detail));
            await Send(packet);
        }
       
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

        private async void OnDataReceived(object sender, SocketDataReceived e)
        {
            var packet = JsonConvert.DeserializeObject<Packet>(e.Data);
            switch (packet.Action)
            {
                case Command.ActionAuth:
                    _playerHash = packet.Token;
                    break;
                case Command.ActionNotification:
                    var notification = JsonConvert.DeserializeObject<Notification>(packet.Data);
                    GameService.OnNotificationReceived(notification);
                    break;
                case Command.ActionAutoMatch:
                    if (packet.Message != null && packet.Message == "waiting_queue")
                    {
                        // TODO -> Invoke OnAutoMatchUpdate(OnWaiting,[])
                    }
                    else
                    {
                        var users = JsonConvert.DeserializeObject<List<User>>(packet.Data);
                        // TODO -> Invoke OnAutoMatchUpdate(OnUserJoined,users)
                    }
                    break;
                case Command.ActionGetRooms:
                    var rooms = JsonConvert.DeserializeObject<List<Room>>(packet.Data);
                    // TODO -> Invoke OnAvailableRooms(rooms)
                    break;
                case Command.ActionChat:
                    var chat = JsonConvert.DeserializeObject<Chat>(packet.Data);
                    // TODO -> Invoke OnChatReceive(chat)
                    break;
                case Command.ActionSubscribe:
                    // TODO -> Invoke OnSubscribeChannel(packet.Message)
                    break;
                case Command.ActionUnSubscribe:
                    // TODO -> Invoke OnUnSubscribeChannel(packet.Message)
                    break;
                case Command.ActionPing:
                    await SendPingPong();
                    break;
                case Command.ActionGetInviteList:
                    var inviteList = JsonConvert.DeserializeObject<List<Invite>>(packet.Data);
                    // TODO -> Invoke OnInviteInbox(inviteList)
                    break;
                case Command.ActionInviteUser:
                    // TODO -> Invoke OnInviteSend()
                    break;
                case Command.ActionFindUser:
                    var usersList = JsonConvert.DeserializeObject<List<User>>(packet.Data);
                    // TODO -> Invoke OnFindUsers(usersList)
                    break;
                case Command.ActionOnInvite:
                    var invite = JsonConvert.DeserializeObject<Invite>(packet.Data);
                    // TODO -> Invoke OnInviteReceive(invite)
                    break;
                case Command.Error:
                    var err = packet.Message;
                    // TODO -> Invoke OnChatReceive(err) or OnTurnBasedError(err)
                    break;
                case Command.ActionJoinRoom:
                    var startPayload = JsonConvert.DeserializeObject<StartPayload>(packet.Data);
                    switch (startPayload.Room.RoomType)
                    {
                        case RoomType.TurnBased:
                            await ConnectToTbServer(startPayload);
                            break;
                        case RoomType.RealTime:
                            await ConnectToRtServer(startPayload);
                            break;
                        case RoomType.NotSet:
                            break;
                        default:
                           break;
                    }
                    break;
                
                
            }
        }


        private async Task ConnectToRtServer(StartPayload payload)
        {
            if (_rtHandler != null && _rtHandler.IsAvailable)
            {
                await _rtHandler.LeaveRoom();
                _rtHandler.Dispose();
                _rtHandler = null;
            }
            _rtHandler = new RTHandler(payload.Area);
            await _rtHandler.Init();
        }
        
        private async Task ConnectToTbServer(StartPayload payload)
        {
            if (_tbHandler != null && _tbHandler.IsAvailable)
            {
                await _tbHandler.LeaveRoom();
                _tbHandler.Dispose();
                _tbHandler = null;
            }
            _tbHandler = new TBHandler(payload.Area);
            await _tbHandler.Init();
        }


    }
}