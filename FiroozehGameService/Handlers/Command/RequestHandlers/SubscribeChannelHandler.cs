using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class SubscribeChannelHandler : BaseRequestHandler
    {
        public static new string Signature
            => "SUBSCRIBECHANNEL";

        public SubscribeChannelHandler(CommandHandler _handler)
            => this.CommandHandler = _handler;

        protected Packet DoAction(string channelName)
            => new Packet(
                CommandHandler.PlayerHash, 
                Models.Consts.Command.ActionSubscribe,
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
