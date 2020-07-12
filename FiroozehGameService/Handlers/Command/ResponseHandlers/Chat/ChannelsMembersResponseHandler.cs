using System.Collections.Generic;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers.Chat
{
    internal class ChannelsMembersResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => Models.Consts.Command.ActionChatRoomDetails;

        protected override void HandleResponse(Packet packet)
        {
            var members = JsonConvert.DeserializeObject<List<Member>>(packet.Data);
            ChatEventHandlers.ChannelsMembers?.Invoke(this, members);
        }
    }
}