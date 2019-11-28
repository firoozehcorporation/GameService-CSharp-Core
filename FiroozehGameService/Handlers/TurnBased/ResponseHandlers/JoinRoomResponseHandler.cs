
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
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
           TurnBasedEventHandlers.onJoinRoom?.Invoke(this,joinData);
        }
      
    }
}
