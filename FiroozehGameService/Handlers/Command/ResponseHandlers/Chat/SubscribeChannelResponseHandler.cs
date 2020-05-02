using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers.Chat
{
    internal class SubscribeChannelResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => Models.Consts.Command.ActionSubscribe;

        protected override void HandleResponse(Packet packet)
        {
            ChatEventHandlers.OnSubscribeChannel?.Invoke(null, packet.Message);
        }
    }
}