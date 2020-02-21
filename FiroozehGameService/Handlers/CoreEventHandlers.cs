using System;
using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers
{
    public class CoreEventHandlers
    {
        internal static EventHandler GProtocolConnected;
        internal static EventHandler<string> Authorized;
        internal static EventHandler<StartPayload> GsLiveSystemStarted;
        internal static EventHandler Ping;
        internal static EventHandler Dispose;

        public static EventHandler SuccessfullyLogined;
        public static EventHandler<ErrorEvent> Error;
    }
}