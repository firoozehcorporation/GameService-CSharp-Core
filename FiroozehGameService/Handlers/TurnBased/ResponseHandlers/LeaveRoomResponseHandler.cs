
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal class LeaveRoomResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => TB.OnLeave;

        protected override void HandleResponse(Packet packet)
        {
           TurnBasedEventHandlers.OnLeaveRoom?.Invoke(this,JsonConvert.DeserializeObject<Member>(packet.Data));
        }
      
    }
}
