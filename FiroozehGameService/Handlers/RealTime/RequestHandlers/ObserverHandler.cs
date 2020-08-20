using System;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive.RT;

namespace FiroozehGameService.Handlers.RealTime.RequestHandlers
{
    internal class ObserverHandler : BaseRequestHandler
    {
        public static string Signature =>
            "OBSERVER_HANDLER";

        private static Packet DoAction(byte[] buffer)
        {
            return new Packet(RealTimeHandler.PlayerHash, RT.ActionObserver, GProtocolSendType.UnReliable, buffer);
        }

        protected override Packet DoAction(object payload)
        {
            if (!RealTimeHandler.IsAvailable) throw new GameServiceException("GSLiveRealTime Not Available yet");
            if (!CheckAction(payload)) throw new ArgumentException();
            return DoAction(payload as byte[]);
        }

        protected override bool CheckAction(object payload)
        {
            return payload.GetType() == typeof(byte[]);
        }
    }
}