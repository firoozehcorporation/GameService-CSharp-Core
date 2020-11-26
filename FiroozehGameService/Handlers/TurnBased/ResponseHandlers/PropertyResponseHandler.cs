using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Models.GSLive.TB;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal class PropertyResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => TB.OnProperty;

        protected override void HandleResponse(Packet packet)
        {
            var property = JsonConvert.DeserializeObject<PropertyPayload>(packet.Data);
            TurnBasedEventHandlers.PropertyUpdated?.Invoke(this, property);
        }
    }
}