using System;
using FiroozehGameService.Models.GSLive.Chat;

namespace FiroozehGameService.Handlers
{
    public class ChatEventHandlers
    {
        public static EventHandler<Chat> OnChatReceived;
        public static EventHandler<string> OnSubscribeChannel;
        public static EventHandler<string> OnUnSubscribeChannel;
    }
}