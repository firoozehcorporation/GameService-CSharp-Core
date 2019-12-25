
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.TB;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal class TakeTurnResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => TB.OnTakeTurn;

        protected override void HandleResponse(Packet packet)
        {
           TurnBasedEventHandlers.TakeTurn?.Invoke(this,JsonConvert.DeserializeObject<Turn>(packet.Data));
        }
      
    }
}
