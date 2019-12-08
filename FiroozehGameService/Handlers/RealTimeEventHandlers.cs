using System;
using System.Collections.Generic;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.GSLive;

namespace FiroozehGameService.Handlers
{
    public class RealTimeEventHandlers : CommandEventHandler
    {
        public static EventHandler<JoinEvent> JoinedRoom;
        public static EventHandler<Leave> LeftRoom;
        public static EventHandler<MessageReceiveEvent> NewMessageReceived;
        public static EventHandler<List<Member>> RoomMembersDetailReceived;
    }
}