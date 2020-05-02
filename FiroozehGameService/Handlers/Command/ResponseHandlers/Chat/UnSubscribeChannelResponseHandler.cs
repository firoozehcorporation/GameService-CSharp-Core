using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers.Chat
{
    internal class UnSubscribeChannelResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => Models.Consts.Command.ActionUnSubscribe;

        protected override void HandleResponse(Packet packet)
        {
            ChatEventHandlers.OnUnSubscribeChannel?.Invoke(null, packet.Message);
        }
    }
}