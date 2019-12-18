namespace FiroozehGameService.Models.Consts
{
    internal static class RT
    {

      
        // Packet Actions
        public const int ActionAuth = 0;
        public const int ActionData = 1;
        public const int ActionPublicMessage = 2;
        public const int ActionPrivateMessage = 3;
        public const int ActionJoin = 4;
        public const int ActionMembersDetail = 5;
        public const int ActionLeave = 6;        
        public const int Error = 100;

        // Limit Checker
        public const int RealTimeLimit = 60; // 60 Request per sec
        public const int RestLimit = 1000; //  RestLimit per sec in long


    }
}