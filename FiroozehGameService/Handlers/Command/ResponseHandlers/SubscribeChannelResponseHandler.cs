using FiroozehGameService.Models.Command;
using Newtonsoft.Json;
using Chat = FiroozehGameService.Models.GSLive.Chat.Chat;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class SubscribeChannelResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand 
            => Models.Consts.Command.ActionSubscribe;

        protected override void HandleResponse(Packet packet)
        {
            ChatEventHandlers.onSubscribeChannel?.Invoke(null, packet.Message);
        }
    }
}
