using System;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.Consts;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.CommandServer_RequestHandlers
{
    internal class SendChannelMessageHandler : BaseHandler
    {
        public static new string Signature
            => "SENDMESSAGE";

        public SendChannelMessageHandler(CommandHandler _handler)
            => this.CommandHandler = _handler;

        protected Packet DoAction(Tuple<string, string> channelMessage)
            => new Packet(
                CommandHandler.PlayerHash,
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
