namespace FiroozehGameService.Models.Consts
{
    public static class Command
    {
        // For Receive
        public const int Notification = 0;
        public const int AvailableRooms = 2;
        public const int AvailableMembersForAutoMatch = 3;
        public const int GoToGame = 4;
        public const int Fail = 100;

        // For Send
        public const int AutoMatch = 1;
        public const int CreateRoom = 2;
        public const int GetRooms = 3;
        public const int JoinRoom = 4;
        public const int PingPong = 5;
    }
}