using FiroozehGameService.Models;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.TB;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased.RequestHandlers
{
    internal class CurrentTurnHandler : BaseRequestHandler
    {
        public static string Signature =>
            "CURRENT_TURN";

        public CurrentTurnHandler() {}

       
        protected override Packet DoAction(object payload)
        { 
            if (!TurnBasedHandler.IsAvailable) throw new GameServiceException("GSLiveTurnBased Not Available yet");
            return new Packet(TurnBasedHandler.PlayerHash,TB.OnCurrentTurnDetail);
        }

        protected override bool CheckAction(object payload)
            => true;
    }
}
