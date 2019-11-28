using FiroozehGameService.Models;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;

namespace FiroozehGameService.Handlers.TurnBased.RequestHandlers
{
    internal class PingPongHandler : BaseRequestHandler
    {
        public static string Signature
            => "PING_PONG";

        public PingPongHandler(TurnBasedHandler handler)
            => TurnBasedHandler = handler;

        private static Packet DoAction()
            => new Packet(TurnBasedHandler.PlayerHash,TB.ActionPingPong);        

        protected override bool CheckAction(object payload)
           => true;

        protected override Packet DoAction(object payload)
        {
            if (!TurnBasedHandler.IsAvailable) throw new GameServiceException("GSLiveRealTime Not Available yet");
            if (!CheckAction(payload)) throw new System.ArgumentException();
            return DoAction();
        }
    }

}
