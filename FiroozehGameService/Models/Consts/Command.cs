using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Models.Consts
{
    internal static class Command
    {
        internal const string Pwd = "C4B7416A4C427A278537C158CAA75A44";

        // For Send
        public const int ActionAuth = 0;
        public const int ActionAutoMatch = 1;
        public const int ActionCreateRoom = 2;
        public const int ActionGetRooms = 3;
        public const int ActionJoinRoom = 4;
        public const int ActionPing = 5;
        public const int ActionInviteUser = 6;
        public const int ActionKickUser = 7;
        public const int ActionGetInviteList = 8;
        public const int ActionAcceptInvite = 9;
        public const int ActionFindUser = 10;
        public const int ActionNotification = 11;
        public const int ActionSubscribe = 12;
        public const int ActionChat = 13;
        public const int ActionUnSubscribe = 14;
        public const int ActionOnInvite = 15;
        public const int ActionCancelAutoMatch = 16;

        public const int Error = 100;

        public static readonly Area CommandArea
            = new Area {Ip = "command.gamesservice.ir", Port = 3003, Protocol = "tcp"};
    }
}