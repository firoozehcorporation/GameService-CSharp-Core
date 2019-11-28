using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class PingPongHandler : BaseRequestHandler
    {
        public static string Signature
            => "PING_PONG";

        public PingPongHandler(CommandHandler handler)
            => CommandHandler = handler;

        private static Packet DoAction()
            => new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionPing);

        protected override bool CheckAction(object payload)
           => true;

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new System.ArgumentException();
            return DoAction();
        }
    }

}
