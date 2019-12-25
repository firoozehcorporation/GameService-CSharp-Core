
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.TB;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal class CurrentTurnResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => TB.OnCurrentTurnDetail;

        protected override void HandleResponse(Packet packet)
        {
           TurnBasedEventHandlers.CurrentTurnMember?.Invoke(this,JsonConvert.DeserializeObject<Member>(packet.Data));
        }
      
    }
}
