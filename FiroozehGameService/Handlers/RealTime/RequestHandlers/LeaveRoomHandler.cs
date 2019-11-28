using FiroozehGameService.Core;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.RT;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.RealTime.RequestHandlers
{
    internal class LeaveRoomHandler : BaseRequestHandler
    {
        public static string Signature =>
            "LEAVEROOM";

        public LeaveRoomHandler(RealTimeHandler handler) =>
            RealTimeHandler = handler;

        protected override Packet DoAction(object payload)
        { 
            if (!RealTimeHandler.IsAvailable) throw new GameServiceException("GSLiveRealTime Not Available yet");
            return new Packet(RT.ActionData
                , JsonConvert.SerializeObject(
                    new DataPayload(RT.OnLeave, GameService.CurrentGame?._Id, RealTimeHandler.PlayerHash)));
        }

        protected override bool CheckAction(object payload)
            => true;
    }
}
