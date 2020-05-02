using System;
using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.Command.RequestHandlers.Chat
{
    internal class SubscribeChannelHandler : BaseRequestHandler
    {
        public static string Signature
            => "SUBSCRIBE_CHANNEL";

        private static Packet DoAction(string channelName)
        {
            return new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionSubscribe,
                null,
                channelName);
        }

        protected override bool CheckAction(object payload)
        {
            return payload is string;
        }

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new ArgumentException();
            return DoAction(payload as string);
        }
    }
}