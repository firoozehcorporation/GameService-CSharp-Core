using System;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive.RT;

namespace FiroozehGameService.Handlers.RealTime.RequestHandlers
{
    internal class SendPrivateMessageHandler : BaseRequestHandler
    {
        public static string Signature =>
            "SEND_PRIVATE_MESSAGE";

        private static Packet DoAction(DataPayload payload)
        {
            var dataPayload = new DataPayload(receiverId: payload.ReceiverId, payload: payload.Payload);
            return new Packet(RealTimeHandler.PlayerHash, RT.ActionPrivateMessage, GProtocolSendType.Reliable,
                dataPayload.Serialize());
        }

        protected override Packet DoAction(object payload)
        {
            if (!RealTimeHandler.IsAvailable) throw new GameServiceException("GSLiveRealTime Not Available yet");
            if (!CheckAction(payload)) throw new ArgumentException();
            return DoAction(payload as DataPayload);
        }

        protected override bool CheckAction(object payload)
        {
            return payload.GetType() == typeof(DataPayload);
        }
    }
}