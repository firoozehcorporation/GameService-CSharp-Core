using System;
using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Handlers
{
    public class CoreEventHandlers
    {
        internal static EventHandler<string> OnAuth;
        internal static EventHandler<StartPayload> OnJoinRoom;
        internal static EventHandler<string> OnError;
        internal static EventHandler OnPing;

        public static EventHandler<Notification> OnNotification;
    }
}