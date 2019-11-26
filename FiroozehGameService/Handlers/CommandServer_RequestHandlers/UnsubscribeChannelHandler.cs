using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;

namespace FiroozehGameService.Handlers.CommandServer_RequestHandlers
{
    internal class UnsubscribeChannelHandler : BaseHandler
    {
        public static new string Signature
            => "UNSUBSCRIBECHANNEL";

        public UnsubscribeChannelHandler(CommandHandler _handler)
            => this._commandHander = _handler;

        protected Packet DoAction(string channelName)
            => new Packet(
                _commandHander.PlayerHash,
                Command.ActionUnSubscribe,
                null,
                channelName);

        protected override bool CheckAction(object payload)
           => payload.GetType() == typeof(string);

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new System.ArgumentException();
            return DoAction(payload as string);
        }
    }
}
