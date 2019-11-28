using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class PingPongHandler : BaseRequestHandler
    {
        public static new string Signature
            => "PINGPONG";

        public PingPongHandler(CommandHandler _handler)
            => this.CommandHandler = _handler;

        protected Packet DoAction()
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
