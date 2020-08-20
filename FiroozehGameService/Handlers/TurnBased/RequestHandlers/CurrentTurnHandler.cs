using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.TurnBased.RequestHandlers
{
    internal class CurrentTurnHandler : BaseRequestHandler
    {
        public static string Signature =>
            "CURRENT_TURN";


        protected override Packet DoAction(object payload)
        {
            if (!TurnBasedHandler.IsAvailable) throw new GameServiceException("GSLiveTurnBased Not Available yet");
            return new Packet(TurnBasedHandler.PlayerHash, TB.OnCurrentTurnDetail);
        }

        protected override bool CheckAction(object payload)
        {
            return true;
        }
    }
}