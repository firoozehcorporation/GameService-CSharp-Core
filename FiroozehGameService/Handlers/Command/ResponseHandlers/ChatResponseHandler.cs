using FiroozehGameService.Models.Command;
using Newtonsoft.Json;
using Chat = FiroozehGameService.Models.GSLive.Chat.Chat;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class ChatResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand 
            => Models.Consts.Command.ActionChat;

        protected override void HandleResponse(Packet packet)
        {
            var chat = JsonConvert.DeserializeObject<Chat>(packet.Data);
            ChatEventHandlers.onChatReceived?.Invoke(null, chat);
        }
    }
}
