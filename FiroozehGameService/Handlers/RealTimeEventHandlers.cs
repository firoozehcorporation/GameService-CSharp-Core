using System;
using System.Collections.Generic;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Models.GSLive.RT;

namespace FiroozehGameService.Handlers
{
    public class RealTimeEventHandlers : CommandEventHandler
    {
        public static EventHandler<JoinEvent> JoinedRoom;
        public static EventHandler<Member> LeftRoom;
        public static EventHandler<MessageReceiveEvent> NewMessageReceived;
        public static EventHandler<List<Member>> RoomMembersDetailReceived;
    }
}