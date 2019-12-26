using System;
using System.Collections.Generic;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.TB;

namespace FiroozehGameService.Handlers
{
    public class TurnBasedEventHandlers : CommandEventHandler
    {
        public static EventHandler<JoinEvent> JoinedRoom;
        public static EventHandler<Member> LeftRoom;
        public static EventHandler<Turn> TakeTurn;
        public static EventHandler<Member> ChoosedNext;
        public static EventHandler<Finish> Finished;
        public static EventHandler<Complete> Completed;
        public static EventHandler<List<Member>> RoomMembersDetailReceived;
        public static EventHandler<Member> CurrentTurnMemberReceived;
    }
}