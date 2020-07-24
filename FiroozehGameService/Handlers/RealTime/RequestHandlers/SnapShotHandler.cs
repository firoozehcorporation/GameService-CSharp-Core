using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive.RT;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.RealTime.RequestHandlers
{
    internal class SnapShotHandler : BaseRequestHandler
    {
        public static string Signature =>
            "SNAPSHOT_HANDLER";

        public SnapShotHandler() {}

        private static Packet DoAction()
            => new Packet(RealTimeHandler.PlayerHash,RT.ActionSnapShot,GProtocolSendType.Reliable);        
        
        protected override Packet DoAction(object payload)
        { 
            if (!RealTimeHandler.IsAvailable) throw new GameServiceException("GSLiveRealTime Not Available yet");
            if (!CheckAction(payload)) throw new System.ArgumentException();
            return DoAction();
        }

        protected override bool CheckAction(object payload)
            => payload.GetType() == typeof(DataPayload);
    }
}
