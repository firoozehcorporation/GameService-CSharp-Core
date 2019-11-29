using System;
using System.Collections.Generic;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.TB;

namespace FiroozehGameService.Handlers
{
    public class TurnBasedEventHandlers : CommandEventHandler
    {
        public static EventHandler<JoinEvent> OnJoinRoom;
        public static EventHandler<Leave> OnLeaveRoom;
        public static EventHandler<Turn> OnTakeTurn;
        public static EventHandler<Member> OnChooseNext;
        public static EventHandler<Finish> OnFinish;
        public static EventHandler<Complete> OnComplete;
        public static EventHandler<List<Member>> OnRoomMembersDetail;
    }
}