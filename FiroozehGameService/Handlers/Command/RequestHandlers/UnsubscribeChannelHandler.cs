using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class UnsubscribeChannelHandler : BaseRequestHandler
    {
        public static string Signature
            => "UNSUBSCRIBE_CHANNEL";

        public UnsubscribeChannelHandler(){}

        private static Packet DoAction(string channelName)
            => new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionUnSubscribe,
                null,
                channelName);

        protected override bool CheckAction(object payload)
           => payload is string;

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new System.ArgumentException();
            return DoAction(payload as string);
        }
    }
}
