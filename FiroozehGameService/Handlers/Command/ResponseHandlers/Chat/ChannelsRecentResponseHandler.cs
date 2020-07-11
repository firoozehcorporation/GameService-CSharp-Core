using System.Collections.Generic;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers.Chat
{
    internal class ChannelRecentResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => Models.Consts.Command.ActionGetLastChats;

        protected override void HandleResponse(Packet packet)
        {
            var chats = JsonConvert.DeserializeObject<List<Models.GSLive.Chat.Chat>>(GetStringFromBuffer(packet.Data));
            ChatEventHandlers.ChannelsRecentMessages?.Invoke(this, chats);
        }
    }
}