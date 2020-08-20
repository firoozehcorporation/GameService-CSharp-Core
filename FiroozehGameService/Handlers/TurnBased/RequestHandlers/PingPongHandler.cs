using System;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.TurnBased.RequestHandlers
{
    internal class PingPongHandler : BaseRequestHandler
    {
        public static string Signature
            => "PING_PONG";

        private static Packet DoAction()
        {
            return new Packet(TurnBasedHandler.PlayerHash, TB.ActionPingPong);
        }

        protected override bool CheckAction(object payload)
        {
            return true;
        }

        protected override Packet DoAction(object payload)
        {
            if (!TurnBasedHandler.IsAvailable) throw new GameServiceException("GSLiveRealTime Not Available yet");
            if (!CheckAction(payload)) throw new ArgumentException();
            return DoAction();
        }
    }
}