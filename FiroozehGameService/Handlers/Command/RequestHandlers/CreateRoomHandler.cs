using FiroozehGameService.Core.GSLive;
using FiroozehGameService.Models;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class CreateRoomHandler : BaseRequestHandler
    {
        public static string Signature =>
          "CREATE_ROOM";

        public CreateRoomHandler() {}

        private static Packet DoAction(GSLiveOption.CreateRoomOption options)
        {
            if (!CommandHandler.IsAvailable) throw new GameServiceException("GSLiveService Not Available yet");
            return new Packet(
                CommandHandler.PlayerHash,
                Models.Consts.Command.ActionCreateRoom,
                JsonConvert.SerializeObject(new RoomDetail
                {
                    Name = options.RoomName,
                    Role = options.Role,
                    Min = options.MinPlayer,
                    Max = options.MaxPlayer,
                    IsPrivate = options.IsPrivate,
                    IsPersist = options.IsPersist,
                    Type = Models.Consts.Command.ActionCreateRoom,
                    GsLiveType = (int)options.GsLiveType
                }));
        }

        protected override bool CheckAction(object payload)
            => payload.GetType() == typeof(GSLiveOption.CreateRoomOption);

        protected override Packet DoAction(object payload)
        {
            if (!CheckAction(payload)) throw new System.ArgumentException();
            return DoAction(payload as GSLiveOption.CreateRoomOption);
        }
    }
}
