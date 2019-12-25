
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal class ChooseNextResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => TB.OnChooseNext;

        protected override void HandleResponse(Packet packet)
        {
           TurnBasedEventHandlers.ChoosedNext?.Invoke(this,JsonConvert.DeserializeObject<Member>(packet.Data));
        }
      
    }
}
