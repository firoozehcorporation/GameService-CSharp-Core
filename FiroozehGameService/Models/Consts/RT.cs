namespace FiroozehGameService.Models.Consts
{
    internal static class RT
    {

        internal const string Pwd = "B8D9272D97AC3E0536068F9BADE14603";

        // Packet Actions
        public const int ActionAuth = 1;
        public const int ActionData = 2;
        public const int ActionPublicMessage = 3;
        public const int ActionPrivateMessage = 4;
        public const int ActionJoin = 5;
        public const int ActionMembersDetail = 6;
        public const int ActionLeave = 7;        
        public const int Error = 100;

        // Limit Checker
        public const int RealTimeLimit = 60; // 60 Request per sec
        public const int RestLimit = 1000; //  RestLimit per sec in long


    }
}