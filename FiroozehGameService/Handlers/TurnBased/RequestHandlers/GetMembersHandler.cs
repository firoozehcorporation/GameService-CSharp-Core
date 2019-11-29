using FiroozehGameService.Models;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;

namespace FiroozehGameService.Handlers.TurnBased.RequestHandlers
{
    internal class GetMemberHandler : BaseRequestHandler
    {
        public static string Signature =>
            "GET_MEMBERS";

        public GetMemberHandler() {}

        protected override Packet DoAction(object payload)
        { 
            if (!TurnBasedHandler.IsAvailable) throw new GameServiceException("GSLiveRealTime Not Available yet");
            return new Packet(TurnBasedHandler.PlayerHash, TB.GetUsers);
        }

        protected override bool CheckAction(object payload)
            => true;
    }
}
