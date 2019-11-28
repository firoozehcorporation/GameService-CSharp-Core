using System.Collections.Generic;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums.GSLive.RT;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.RT;
using Newtonsoft.Json;
using Leave = FiroozehGameService.Models.GSLive.Leave;
using Message = FiroozehGameService.Models.GSLive.Message;
using Packet = FiroozehGameService.Models.GSLive.RT.Packet;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal class DataResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => RT.ActionData;

        protected override void HandleResponse(Packet packet)
        {
            var dataPayload = JsonConvert.DeserializeObject<DataPayload>(packet.Payload);
            switch (dataPayload.Action)
            {
                case RT.OnJoin:
                    DoJoin(dataPayload);
                    break;
                case RT.SendPublicMessage:
                    DoPublicMessage(dataPayload);
                    break;
                case RT.SendPrivateMessage:
                    DoPrivateMessage(dataPayload);
                    break;
                case RT.OnMembersDetail:
                    DoMemberDetails(dataPayload);
                    break;
                case RT.OnLeave:
                    DoLeave(dataPayload);
                    break;
            }
          
        }

        private static void DoJoin(DataPayload payload)
        {
            var joinData = JsonConvert.DeserializeObject<JoinData>(payload.Payload);
            RealTimeEventHandlers.onJoinRoom?.Invoke(null, joinData);
        }
        
        private static void DoPublicMessage(DataPayload payload)
        {
            RealTimeEventHandlers.onMessageReceive?.Invoke(null, new MessageReceiveEvent
            {
                MessageType = MessageType.Public,
                Message = new Message
                {
                    RoomId = payload.RoomId,
                    Data = payload.Payload,
                    ReceiverId = payload.ReceiverId,
                    SenderId = payload.UserHash
                }
            });
        }
        
        private static void DoPrivateMessage(DataPayload payload)
        {
            RealTimeEventHandlers.onMessageReceive?.Invoke(null, new MessageReceiveEvent
            {
                MessageType = MessageType.Private,
                Message = new Message
                {
                    RoomId = payload.RoomId,
                    Data = payload.Payload,
                    ReceiverId = payload.ReceiverId,
                    SenderId = payload.UserHash
                }
            });
        }
        
        private static void DoMemberDetails(DataPayload payload)
        {
            RealTimeEventHandlers.onRoomMembersDetail?.Invoke(null,JsonConvert.DeserializeObject<List<Member>>(payload.Payload));
        }
        
        private static void DoLeave(DataPayload payload)
        {
            RealTimeEventHandlers.onLeaveRoom?.Invoke(null,new Leave {RoomId = payload.RoomId, MemberLeave = JsonConvert.DeserializeObject<Member>(payload.Payload)});
        }
    }
}
