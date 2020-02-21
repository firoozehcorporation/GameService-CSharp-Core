using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class FindUserHandler : BaseRequestHandler
    {
        public static string Signature
            => "FIND_USER";

        public FindUserHandler(){}

        private static Packet DoAction(RoomDetail options)
            => new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionFindUser,
                JsonConvert.SerializeObject(options, new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore })
                );

        protected override bool CheckAction(object payload)
            => payload.GetType() == typeof(RoomDetail);

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new System.ArgumentException();
            return DoAction(payload as RoomDetail);
        }

    }
}
