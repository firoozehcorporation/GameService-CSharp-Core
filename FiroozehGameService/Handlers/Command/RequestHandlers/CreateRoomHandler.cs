using FiroozehGameService.Core.GSLive;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.RequestHandlers
{
    internal class CreateRoomHandler : BaseRequestHandler
    {
        public static new string Signature =>
          "CREATEROOM";

        public CreateRoomHandler(CommandHandler handler) =>
            CommandHandler = handler;

        protected Packet DoAction(GSLiveOption.CreateRoomOption options)
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
                    GsLiveType = (int)options.RoomType
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
