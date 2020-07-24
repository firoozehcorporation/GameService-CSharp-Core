using System;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive.RT;
using FiroozehGameService.Models.GSLive.RT;
using FiroozehGameService.Utils;
using FiroozehGameService.Utils.Serializer;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal class SnapShotResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => RT.ActionSnapShot;

        protected override void HandleResponse(Packet packet, GProtocolSendType type)
        {
            try
            {
                var dataPayload = new DataPayload(packet.Payload);
                var shotsFromBuffer = GsSerializer.Object.GetSnapShotsFromBuffer(dataPayload.Payload);
                GsSerializer.OnNewSnapShotReceived?.Invoke(this,shotsFromBuffer);
            }
            catch (Exception e)
            {
                LogUtil.LogError(this, e.Message);
            }
        }
    }
}