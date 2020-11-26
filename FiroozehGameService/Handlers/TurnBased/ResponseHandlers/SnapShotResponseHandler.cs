using System.Collections.Generic;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Models.GSLive.TB;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.TurnBased.ResponseHandlers
{
    internal class SnapShotResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand => TB.OnSnapshot;

        protected override void HandleResponse(Packet packet)
        {
            var snapShot = JsonConvert.DeserializeObject<List<SnapShot>>(packet.Data);
            TurnBasedEventHandlers.SnapShotReceived?.Invoke(this, snapShot);
        }
    }
}