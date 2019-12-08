using System;
using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Handlers
{
    public class CoreEventHandlers
    {
        internal static EventHandler<string> Authorized;
        internal static EventHandler<StartPayload> GsLiveSystemStarted;
        internal static EventHandler Ping;

        public static EventHandler SuccessfullyLogined;
        public static EventHandler<ErrorEvent> Error;
    }
}