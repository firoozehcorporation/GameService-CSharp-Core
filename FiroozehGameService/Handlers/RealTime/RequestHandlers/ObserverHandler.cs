using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive.RT;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.RealTime.RequestHandlers
{
    internal class ObserverHandler : BaseRequestHandler
    {
        public static string Signature =>
            "OBSERVER_HANDLER";

        public ObserverHandler() {}

        private static Packet DoAction(byte[] buffer)
            => new Packet(RealTimeHandler.PlayerHash,RT.ActionObserver,GProtocolSendType.UnReliable,buffer);        
        
        protected override Packet DoAction(object payload)
        { 
            if (!RealTimeHandler.IsAvailable) throw new GameServiceException("GSLiveRealTime Not Available yet");
            if (!CheckAction(payload)) throw new System.ArgumentException();
            return DoAction(payload as byte[]);
        }

        protected override bool CheckAction(object payload)
            => payload.GetType() == typeof(byte[]);
    }
}
