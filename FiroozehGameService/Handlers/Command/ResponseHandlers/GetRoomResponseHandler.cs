using System.Collections.Generic;
using FiroozehGameService.Models.Command;
using FiroozehGameService.Models.GSLive;
using Newtonsoft.Json;

namespace FiroozehGameService.Handlers.Command.ResponseHandlers
{
    internal class GetRoomResponseHandler : BaseResponseHandler
    {
        public static int ActionCommand 
            => Models.Consts.Command.ActionGetRooms;

        protected override void HandleResponse(Packet packet)
        {
            var rooms = JsonConvert.DeserializeObject<List<Room>>(packet.Data);
            //some operations on rooms variable
            RoomHandler.GetRooms?.Invoke(null, rooms);
        }
    }
}
