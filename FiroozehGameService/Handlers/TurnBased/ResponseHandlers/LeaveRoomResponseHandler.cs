using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal class LeaveRoomResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => TB.OnLeave;

        protected override void HandleResponse(Packet packet)
        {
            TurnBasedEventHandlers.LeftRoom?.Invoke(this,
                JsonConvert.DeserializeObject<Member>(GetStringFromBuffer(packet.Data)));
        }
    }
}