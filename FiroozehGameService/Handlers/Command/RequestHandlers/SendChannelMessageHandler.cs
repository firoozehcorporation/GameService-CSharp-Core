using System;
using FiroozehGameService.Models.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class SendChannelMessageHandler : BaseRequestHandler
    {
        public static string Signature
            => "SEND_MESSAGE";

        public SendChannelMessageHandler(){}

        private static Packet DoAction(Tuple<string, string> channelMessage)
            => new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionChat,
                JsonConvert.SerializeObject(
                    new Message(
                        false, 
                        channelMessage.Item1,
                        null,
                        channelMessage.Item2), new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
                );

        protected override bool CheckAction(object payload)
           => payload.GetType() == typeof(Tuple<string,string>);

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new ArgumentException();
            return DoAction(payload as Tuple<string,string>);
        }
    }
}
