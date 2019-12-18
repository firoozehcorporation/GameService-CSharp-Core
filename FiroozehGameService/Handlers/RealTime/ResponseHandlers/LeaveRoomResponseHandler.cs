using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal class LeaveRoomResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => RT.ActionLeave;

        protected override void HandleResponse(Packet packet, GProtocolSendType type)
        {
           TurnBasedEventHandlers.OnLeaveRoom?.Invoke(this,JsonConvert.DeserializeObject<Leave>(packet.Data));
        }
      
    }
}
