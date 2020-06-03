using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.RT;

namespace FiroozehGameService.Handlers.RealTime.RequestHandlers
{
    internal class GetPingHandler : BaseRequestHandler
    {
        public static string Signature =>
            "GET_PING";

        protected override Packet DoAction(object payload)
        {
            if (!RealTimeHandler.IsAvailable) throw new GameServiceException("GSLiveRealTime Not Available yet");
            return new Packet(RealTimeHandler.PlayerHash, RT.ActionPing);
        }

        protected override bool CheckAction(object payload)
        {
            return true;
        }
    }
}