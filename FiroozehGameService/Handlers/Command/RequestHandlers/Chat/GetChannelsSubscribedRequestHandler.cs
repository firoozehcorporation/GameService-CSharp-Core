using System;
using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.Command.RequestHandlers.Chat
{
    internal class GetChannelsSubscribedRequestHandler : BaseRequestHandler
    {
        public static string Signature
            => "GET_CHANNELS_SUBSCRIBED";

        private static Packet DoAction()
        {
            return new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionGetChannelsSubscribed);
        }

        protected override bool CheckAction(object payload)
        {
            return true;
        }

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new ArgumentException();
            return DoAction();
        }
    }
}