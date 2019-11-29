namespace FiroozehGameService.Models.Consts
{
    internal static class RT
    {

        // Packet Actions
        public const int ActionData = 0;
        public const int ActionAuth = 1;
        public const int ActionStatus = 2;
        public const int ActionPingPong = 3;


        // DataPayload Actions
        public const int OnJoin = 1;
        public const int SendPublicMessage = 2;
        public const int SendPrivateMessage = 3;
        public const int OnMembersDetail = 4;
        public const int OnLeave = 5;
        public const int OnDestroy = 6;

        public const int PingPongDelay = 7000; // Delay In Millisecond


        // Limit Checker
        public const int RealTimeLimit = 60; // 60 Request per sec
        public const int RestLimit = 1000; //  RestLimit per sec in long


    }
}