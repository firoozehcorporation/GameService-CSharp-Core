using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.TurnBased.RequestHandlers
{
    internal class GetMemberHandler : BaseRequestHandler
    {
        public static string Signature =>
            "GET_MEMBERS";

        protected override Packet DoAction(object payload)
        {
            if (!TurnBasedHandler.IsAvailable) throw new GameServiceException("GSLiveRealTime Not Available yet");
            return new Packet(TurnBasedHandler.PlayerHash, TB.GetMembers);
        }

        protected override bool CheckAction(object payload)
        {
            return true;
        }
    }
}