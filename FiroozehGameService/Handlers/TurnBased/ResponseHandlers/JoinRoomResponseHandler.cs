
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal class JoinRoomResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => TB.OnJoin;

        protected override void HandleResponse(Packet packet)
        {
           var joinData = JsonConvert.DeserializeObject<JoinData>(packet.Data);
           TurnBasedEventHandlers.JoinedRoom?.Invoke(this, new JoinEvent
            {
                Type = GSLiveType.TurnBased,
                JoinData = joinData
            });
        }
      
    }
}
