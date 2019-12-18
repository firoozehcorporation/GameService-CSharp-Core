using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.RealTime.ResponseHandlers
{
    internal class JoinRoomResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => RT.ActionJoin;

        protected override void HandleResponse(Packet packet,GProtocolSendType type)
        {
           var joinData = JsonConvert.DeserializeObject<JoinData>(packet.Data);
           TurnBasedEventHandlers.JoinRoom?.Invoke(this, new JoinEvent
            {
                Type = GSLiveType.TurnBased,
                JoinData = joinData
            });
        }
      
    }
}
