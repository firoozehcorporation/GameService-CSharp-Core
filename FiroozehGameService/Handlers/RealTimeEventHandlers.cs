using System;
using System.Collections.Generic;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.GSLive;

namespace FiroozehGameService.Handlers
{
    public class RealTimeEventHandlers : CommandEventHandler
    {
        public static EventHandler<JoinEvent> OnJoinRoom;
        public static EventHandler<Leave> OnLeaveRoom;
        public static EventHandler<MessageReceiveEvent> OnMessageReceive;
        public static EventHandler<List<Member>> OnRoomMembersDetail;
    }
}