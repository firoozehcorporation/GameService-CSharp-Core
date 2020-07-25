using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class JoinRoomResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => Models.Consts.Command.ActionJoinRoom;

        protected override void HandleResponse(Packet packet)
        {
            var payload = JsonConvert.DeserializeObject<StartPayload>(packet.Data);
            CoreEventHandlers.GsLiveSystemStarted?.Invoke(this, payload);
        }
    }
}