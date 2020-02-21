﻿using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;
using Chat = FiroozehGameService.Models.GSLive.Chat.Chat;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
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
