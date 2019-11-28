using FiroozehGameService.Core;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.RT;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.RealTime.RequestHandlers
{
    internal class SendPublicMessageHandler : BaseRequestHandler
    {
        public static string Signature =>
            "SEND_PUBLIC_MESSAGE";

        public SendPublicMessageHandler(RealTimeHandler handler) =>
            RealTimeHandler = handler;

        private static Packet DoAction(DataPayload payload)
            => new Packet(RT.ActionData
                , JsonConvert.SerializeObject(
                    new DataPayload(
                        RT.SendPublicMessage,
                        RealTimeHandler.CurrentRoom?.Id,
                        RealTimeHandler.PlayerHash,
                        payload:payload.Payload)
                ));        
        
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
