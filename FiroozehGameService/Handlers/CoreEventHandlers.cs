using System;
using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Handlers
{
    public class CoreEventHandlers
    {
        internal static EventHandler<string> OnAuth;
        internal static EventHandler<StartPayload> OnStartGsLiveSystem;
        internal static EventHandler OnPing;

        public static EventHandler<ErrorEvent> OnError;
    }
}