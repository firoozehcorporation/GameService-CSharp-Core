using System;
using System.Collections.Generic;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.TB;

namespace FiroozehGameService.Handlers
{
    public class TurnBasedEventHandlers : CommandEventHandler
    {
        public static EventHandler<JoinData> onJoinRoom;
        public static EventHandler<Leave> onLeaveRoom;
        public static EventHandler<Turn> onTakeTurn;
        public static EventHandler<Member> onChooseNext;
        public static EventHandler<Finish> onFinish;
        public static EventHandler<Complete> onComplete;
        public static EventHandler<List<Member>> onRoomMembersDetail;
    }
}