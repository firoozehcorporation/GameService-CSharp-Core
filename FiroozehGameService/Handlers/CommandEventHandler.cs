using FiroozehGameService.Models.GSLive;
using System;
using System.Collections.Generic;
using FiroozehGameService.Models.BasicApi;
using FiroozehGameService.Models.Command;
using Invite = FiroozehGameService.Models.GSLive.Invite;

namespace FiroozehGameService.Handlers
{
    public class CommandEventHandler : CoreEventHandlers
    {
        public static EventHandler<List<Room>> onGetRooms;
        public static EventHandler<AutoMatchEvent> onAutoMatchUpdate;
        
        // Invite EventHandlers
        public static EventHandler<List<Invite>> onInviteInbox;
        public static EventHandler<Invite> onInviteReceived;
        public static EventHandler<List<User>> onFindUser;
        public static EventHandler onInviteUser;
       
    }
}
