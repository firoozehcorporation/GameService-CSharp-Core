using System;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.TurnBased.RequestHandlers
{
    internal class SnapshotHandler : BaseRequestHandler
    {
        public static string Signature =>
            "SNAP_SHOT_HANDLER";

        private static Packet DoAction()
        {
            return new Packet(TurnBasedHandler.PlayerHash, TB.OnSnapshot);
        }

        protected override Packet DoAction(object payload)
        {
            if (!TurnBasedHandler.IsAvailable) throw new GameServiceException("GSLiveTurnBased Not Available yet");
            if (!CheckAction(payload)) throw new ArgumentException();
            return DoAction();
        }

        protected override bool CheckAction(object payload)
        {
            return true;
        }
    }
}