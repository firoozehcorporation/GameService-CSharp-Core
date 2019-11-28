using FiroozehGameService.Models.GSLive;
using System;
using System.Collections.Generic;

namespace FiroozehGameService.Handlers
{
    //example demostration ONLY
    internal static class RoomHandler
    {
        public static EventHandler RoomUpdate; //or EventHandler<SOMEARG>
        public static EventHandler UserJoin;
        public static EventHandler<List<Room>> GetRooms;

        //more EventHandler goes here
    }
}
