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
        public static EventHandler<List<Room>> AvailableRoomsReceived;
        public static EventHandler<AutoMatchEvent> AutoMatchUpdated;
        
        // Invite EventHandlers
        public static EventHandler<List<Invite>> InviteInboxReceived;
        public static EventHandler<Invite> NewInviteReceived;
        public static EventHandler<List<User>> FindUserReceived;
        public static EventHandler InvitationSent;
       
    }
}
