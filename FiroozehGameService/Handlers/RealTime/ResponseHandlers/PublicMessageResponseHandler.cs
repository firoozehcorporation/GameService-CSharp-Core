using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive.RT;
using FiroozehGameService.Models.GSLive.RT;
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
                MessageType = MessageType.Public,
                SendType = type,
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