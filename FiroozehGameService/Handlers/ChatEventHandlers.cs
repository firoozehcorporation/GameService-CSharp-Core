using System;
using FiroozehGameService.Models.GSLive.Chat;

namespace FiroozehGameService.Handlers
{
    public class ChatEventHandlers
    {
        public static EventHandler<Chat> onChatReceived;
        public static EventHandler<string> onSubscribeChannel;
        public static EventHandler<string> onUnSubscribeChannel;
    }
}