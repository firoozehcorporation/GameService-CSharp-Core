using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Models.GSLive.TB;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal class CompleteResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => TB.OnComplete;

        protected override void HandleResponse(Packet packet)
        {
            TurnBasedEventHandlers.Completed?.Invoke(this,
                JsonConvert.DeserializeObject<Complete>(GetStringFromBuffer(packet.Data)));
        }
    }
}