using System.Collections.Generic;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers.Chat
{
    internal class PendingMessagesResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => Models.Consts.Command.ActionGetPendingChats;

        protected override void HandleResponse(Packet packet)
        {
            var chats = JsonConvert.DeserializeObject<List<Models.GSLive.Chat.Chat>>(GetStringFromBuffer(packet.Data));
            ChatEventHandlers.PendingMessages?.Invoke(this, chats);
        }
    }
}