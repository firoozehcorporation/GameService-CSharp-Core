using FiroozehGameService.Models.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class AcceptInviteHandler : BaseRequestHandler
    {
        public static string Signature
            => "ACCEPT_INVITE";

        public AcceptInviteHandler(CommandHandler handler)
            => CommandHandler = handler;

        private static Packet DoAction(RoomDetail inviteOptions)
            => new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionAcceptInvite,
                JsonConvert.SerializeObject(inviteOptions));

        protected override bool CheckAction(object payload)
            => payload.GetType() == typeof(RoomDetail);

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new System.ArgumentException();
            return DoAction(payload as RoomDetail);
        }

    }
}
