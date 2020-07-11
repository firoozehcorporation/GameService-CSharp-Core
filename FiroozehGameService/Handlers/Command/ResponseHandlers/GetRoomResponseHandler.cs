using System.Collections.Generic;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class GetRoomResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand
            => Models.Consts.Command.ActionGetRooms;

        protected override void HandleResponse(Packet packet)
        {
            var rooms = JsonConvert.DeserializeObject<List<Room>>(GetStringFromBuffer(packet.Data));
            CommandEventHandler.AvailableRoomsReceived?.Invoke(null, rooms);
        }
    }
}