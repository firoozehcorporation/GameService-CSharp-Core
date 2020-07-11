using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class InviteReceivedInboxResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => Models.Consts.Command.ActionOnInvite;

        protected override void HandleResponse(Packet packet)
        {
            var invite = JsonConvert.DeserializeObject<Invite>(GetStringFromBuffer(packet.Data));
            CommandEventHandler.NewInviteReceived?.Invoke(null, invite);
        }
    }
}