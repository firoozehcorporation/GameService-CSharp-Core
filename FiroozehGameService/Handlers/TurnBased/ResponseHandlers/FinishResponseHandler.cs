
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.TB;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal class FinishResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => TB.OnFinish;

        protected override void HandleResponse(Packet packet)
        {
           TurnBasedEventHandlers.onFinish?.Invoke(this,JsonConvert.DeserializeObject<Finish>(packet.Data));
        }
      
    }
}
