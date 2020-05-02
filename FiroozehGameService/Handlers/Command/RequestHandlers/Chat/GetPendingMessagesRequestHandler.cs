using System;
using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.Command.RequestHandlers.Chat
{
    internal class GetPendingMessagesRequestHandler : BaseRequestHandler
    {
        public static string Signature
            => "GET_PENDING_MESSAGES";

        private static Packet DoAction()
        {
            return new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionGetPendingChats);
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