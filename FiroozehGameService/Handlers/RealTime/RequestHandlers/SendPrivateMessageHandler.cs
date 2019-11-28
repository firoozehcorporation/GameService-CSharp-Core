using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.RT;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.RealTime.RequestHandlers
{
    internal class SendPrivateMessageHandler : BaseRequestHandler
    {
        public static string Signature =>
            "SEND_PRIVATE_MESSAGE";

        public SendPrivateMessageHandler(RealTimeHandler handler) =>
            RealTimeHandler = handler;

        private Packet DoAction(DataPayload payload)
            => new Packet(RT.ActionData
                , JsonConvert.SerializeObject(
                    new DataPayload(
                        RT.SendPrivateMessage,
                        RealTimeHandler.CurrentRoom?.Id,
                        RealTimeHandler.PlayerHash,
                        payload.ReceiverId,
                        payload.Payload)
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
