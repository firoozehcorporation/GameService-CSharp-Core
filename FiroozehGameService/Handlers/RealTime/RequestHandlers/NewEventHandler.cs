using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive.RT;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.RealTime.RequestHandlers
{
    internal class NewEventHandler : BaseRequestHandler
    {
        public static string Signature =>
            "EVENT_HANDLER";

        internal NewEventHandler() {}

        private static Packet DoAction(DataPayload payload)
            => new Packet(RealTimeHandler.PlayerHash,RT.ActionEvent,GProtocolSendType.Reliable,payload.Serialize());        
        
        protected override Packet DoAction(object payload)
        { 
            if (!RealTimeHandler.IsAvailable) throw new GameServiceException("GSLiveRealTime Not Available yet");
            if (!CheckAction(payload)) throw new System.ArgumentException();
            return DoAction(payload as DataPayload);
        }

        protected override bool CheckAction(object payload)
            => payload.GetType() == typeof(DataPayload);
    }
}
