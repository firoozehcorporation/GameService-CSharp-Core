using System;
using System.Collections.Generic;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.GSLive;

namespace FiroozehGameService.Handlers
{
    public class RealTimeEventHandlers : CommandEventHandler
    {
        public static EventHandler<JoinEvent> onJoinRoom;
        public static EventHandler<Leave> onLeaveRoom;
        public static EventHandler<MessageReceiveEvent> onMessageReceive;
        public static EventHandler<List<Member>> onRoomMembersDetail;
    }
}