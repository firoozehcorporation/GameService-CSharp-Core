using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive.RT;
using FiroozehGameService.Models.GSLive.RT;
using FiroozehGameService.Utils;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal class PublicMessageResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => RT.ActionPublicMessage;

        protected override void HandleResponse(Packet packet, GProtocolSendType type)
        {
            var dataPayload = JsonConvert.DeserializeObject<DataPayload>(packet.Payload);
            RealTimeEventHandlers.NewMessageReceived?.Invoke(this, new MessageReceiveEvent
            {
                MessageInfo = new MessageInfo
                {
                    MessageType = MessageType.Public,
                    SendType = type,
                    ClientReceiveTime = packet.ClientReceiveTime,
                    RoundTripTime = PingUtil.GetLastPing()
                },
                Message = new Message
                {
                    Data = dataPayload.Payload,
                    ReceiverId = dataPayload.ReceiverId,
                    SenderId = dataPayload.SenderId
                }
            });
        }
    }
}