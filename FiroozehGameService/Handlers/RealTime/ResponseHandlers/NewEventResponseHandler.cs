using System;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive.RT;
using FiroozehGameService.Models.GSLive.RT;
using FiroozehGameService.Utils;

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
                RealTimeEventHandlers.NewEventReceived?.Invoke(this,
                    new EventData
                    {
                        Caller = dataPayload.ExtraData,
                        Data = dataPayload.Payload
                    });
            }
            catch (Exception e)
            {
                LogUtil.LogError(this, e.Message);
            }
        }
    }
}