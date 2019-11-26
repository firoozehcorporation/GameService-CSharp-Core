using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using Newtonsoft.Json;
using System;

namespace FiroozehGameService.Handlers.CommandHandlers
{
    internal class SendChannelMessageHandler : BaseHandler<SendChannelMessageHandler>
    {
        public static new string Signature
            => "SENDMESSAGE";

        public SendChannelMessageHandler(CommandHandler _handler)
            => this._commandHander = _handler;

        protected Packet DoAction(Tuple<string, string> channelMessage)
            => new Packet(
                _commandHander.PlayerHash,
                Command.ActionChat,
                JsonConvert.SerializeObject(
                    new Message(
                        false, 
                        channelMessage.Item1,
                        null,
                        channelMessage.Item2))
                );

        protected override bool CheckAction(object payload)
           => payload.GetType() == typeof(Tuple<string,string>);

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new System.ArgumentException();
            return DoAction(payload as Tuple<string,string>);
        }
    }
}
