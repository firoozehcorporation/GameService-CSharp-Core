using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.RT;

namespace FiroozehGameService.Handlers.RealTime.RequestHandlers
{
    internal class LeaveRoomHandler : BaseRequestHandler
    {
        public static string Signature =>
            "LEAVE_ROOM";

        public LeaveRoomHandler() {}

        protected override Packet DoAction(object payload)
        { 
            if (!RealTimeHandler.IsAvailable) throw new GameServiceException("GSLiveRealTime Not Available yet");
            return new Packet(RealTimeHandler.PlayerHash, RT.ActionLeave);
        }

        protected override bool CheckAction(object payload)
            => true;
    }
}
