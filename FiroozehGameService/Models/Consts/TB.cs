namespace FiroozehGameService.Models.Consts
{
    internal static class TB
    {
        // Packet Actions
        internal const int ActionAuth = 1;
        internal const int ActionPingPong = 2;
        internal const int OnTakeTurn = 4;
        internal const int OnChooseNext = 5;
        internal const int OnLeave = 6;
        internal const int OnFinish = 7;
        internal const int OnComplete = 8;
        internal const int GetMembers = 9;
        internal const int OnJoin = 11;
        internal const int OnCurrentTurnDetail = 12;
        internal const int OnProperty = 13;
        internal const int OnSnapshot = 15;



        internal const int Errors = 100;


        internal const int TurnBasedLimit = 5; // 5 Request per sec
        internal const int RestLimit = 1000; //  RestLimit per sec in long
        internal const short MaxRetryConnect = 5;
    }
}