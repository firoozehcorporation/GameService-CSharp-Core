using System;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive.RT;
using FiroozehGameService.Utils;
using FiroozehGameService.Utils.Serializer;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal class NewEventResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => RT.ActionEvent;

        protected override void HandleResponse(Packet packet, GProtocolSendType type)
        {
            try
            {
                var dataPayload = new DataPayload(packet.Payload);
                GsSerializer.OnNewEventHandler?.Invoke(this,
                    new EventData
                    {
                        Caller = dataPayload.ExtraData,
                        Data = dataPayload.Payload,
                        SenderId = dataPayload.SenderId,
                        ReceiverId = dataPayload.ReceiverId
                    });
            }
            catch (Exception e)
            {
                LogUtil.LogError(this, e.Message);
            }
        }
    }
}