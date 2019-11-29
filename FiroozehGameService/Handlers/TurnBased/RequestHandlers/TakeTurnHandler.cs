using FiroozehGameService.Models;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.TB;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased.RequestHandlers
{
    internal class TakeTurnHandler : BaseRequestHandler
    {
        public static string Signature =>
            "TAKE_TURN";

        public TakeTurnHandler() {}

        private static Packet DoAction(DataPayload payload)
            => new Packet(TurnBasedHandler.PlayerHash,TB.OnTakeTurn,
                JsonConvert.SerializeObject(payload));
        
        protected override Packet DoAction(object payload)
        { 
            if (!TurnBasedHandler.IsAvailable) throw new GameServiceException("GSLiveTurnBased Not Available yet");
            if (!CheckAction(payload)) throw new System.ArgumentException();
            return DoAction(payload as DataPayload);
        }

        protected override bool CheckAction(object payload)
            => payload.GetType() == typeof(DataPayload);
    }
}
