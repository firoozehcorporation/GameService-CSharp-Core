using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.RT;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal class LeaveRoomResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => RT.ActionLeave;

        protected override void HandleResponse(Packet packet, GProtocolSendType type)
        {
           RealTimeEventHandlers.LeftRoom?.Invoke(this,JsonConvert.DeserializeObject<Member>(packet.Payload));
        }
      
    }
}
