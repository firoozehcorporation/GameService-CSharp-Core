using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.RT;
using Newtonsoft.Json;
using Packet = FiroozehGameService.Models.GSLive.RT.Packet;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal class StatusResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => RT.ActionStatus;

        protected override void HandleResponse(Packet packet)
        {
            var statusPayload = JsonConvert.DeserializeObject<StatusPayload>(packet.Payload);
            if (statusPayload.Status)
                RealTimeHandler.PlayerHash = statusPayload.Message;
            CoreEventHandlers.OnAuth?.Invoke(this,statusPayload.Message);
        }
      
    }
}
