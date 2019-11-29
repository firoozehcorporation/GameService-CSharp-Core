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
        public static EventHandler<List<Room>> OnAvailableRooms;
        public static EventHandler<AutoMatchEvent> OnAutoMatchUpdate;
        
        // Invite EventHandlers
        public static EventHandler<List<Invite>> OnInviteInbox;
        public static EventHandler<Invite> OnInviteReceived;
        public static EventHandler<List<User>> OnFindUser;
        public static EventHandler OnInviteUser;
       
    }
}
