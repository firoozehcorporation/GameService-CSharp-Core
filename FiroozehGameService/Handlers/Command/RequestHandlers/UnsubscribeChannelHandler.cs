using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class UnsubscribeChannelHandler : BaseRequestHandler
    {
        public static new string Signature
            => "UNSUBSCRIBECHANNEL";

        public UnsubscribeChannelHandler(CommandHandler _handler)
            => this.CommandHandler = _handler;

        protected Packet DoAction(string channelName)
            => new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionUnSubscribe,
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
