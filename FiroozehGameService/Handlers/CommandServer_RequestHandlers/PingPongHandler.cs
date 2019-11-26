using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;

namespace FiroozehGameService.Handlers.CommandServer_RequestHandlers
{
    internal class PingPongHandler : BaseHandler
    {
        public static new string Signature
            => "PINGPONG";

        public PingPongHandler(CommandHandler _handler)
            => this._commandHander = _handler;

        protected Packet DoAction()
            => new Packet(
                _commandHander.PlayerHash,
                Command.ActionPing);

        protected override bool CheckAction(object payload)
           => true;

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new System.ArgumentException();
            return DoAction();
        }
    }

}
