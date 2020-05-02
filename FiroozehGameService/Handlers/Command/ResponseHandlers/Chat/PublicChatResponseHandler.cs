using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers.Chat
{
    internal class PublicChatResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => Models.Consts.Command.ActionPublicChat;

        protected override void HandleResponse(Packet packet)
        {
            var chat = JsonConvert.DeserializeObject<Models.GSLive.Chat.Chat>(packet.Data);
            ChatEventHandlers.OnChatReceived?.Invoke(null, chat);
        }
    }
}