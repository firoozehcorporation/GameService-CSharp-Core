using FiroozehGameService.Models.Command;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class SubscribeChannelHandler : BaseRequestHandler
    {
        public static string Signature
            => "SUBSCRIBE_CHANNEL";

        public SubscribeChannelHandler(){}

        private static Packet DoAction(string channelName)
            => new Packet(
                CommandHandler.PlayerHash, 
                Models.Consts.Command.ActionSubscribe,
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
