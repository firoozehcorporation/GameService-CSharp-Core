using System;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class AcceptInviteHandler : BaseRequestHandler
    {
        public static string Signature
            => "ACCEPT_INVITE";

        private static Packet DoAction(RoomDetail inviteOptions)
        {
            return new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionAcceptInvite, 
                JsonConvert.SerializeObject(inviteOptions, new JsonSerializerSettings
                {
                    NullValueHandling = NullValueHandling.Ignore,
                    DefaultValueHandling = DefaultValueHandling.Ignore
                }));
        }

        protected override bool CheckAction(object payload)
        {
            return payload.GetType() == typeof(RoomDetail);
        }

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new ArgumentException();
            return DoAction(payload as RoomDetail);
        }
    }
}