using System.Collections.Generic;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers.Chat
{
    internal class ChannelSubscribedResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => Models.Consts.Command.ActionGetChannelsSubscribed;

        protected override void HandleResponse(Packet packet)
        {
            var channels = JsonConvert.DeserializeObject<List<string>>(GetStringFromBuffer(packet.Data));
            ChatEventHandlers.ChannelsSubscribed?.Invoke(this, channels);
        }
    }
}