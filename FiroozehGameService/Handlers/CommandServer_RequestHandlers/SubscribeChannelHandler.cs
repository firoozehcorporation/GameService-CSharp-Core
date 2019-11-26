using FiroozehGameService.Core.GSLive;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.CommandHandlers
{
    internal class SubscribeChannelHandler : BaseHandler<SubscribeChannelHandler>
    {
        public static new string Signature
            => "SUBSCRIBECHANNEL";

        public SubscribeChannelHandler(CommandHandler _handler)
            => this._commandHander = _handler;

        protected Packet DoAction(string channelName)
            => new Packet(
                _commandHander.PlayerHash, 
                Command.ActionSubscribe,
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
