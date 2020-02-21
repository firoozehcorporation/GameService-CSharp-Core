namespace FiroozehGameService.Models.Consts
{
    internal static class TB
    {
        
        internal const string Pwd = "F318EE7D2E7F8AFB595631CB1E52F7DE";

        // Packet Actions
        public const int ActionAuth = 1;
        public const int ActionPingPong = 2;
        public const int OnTakeTurn = 4;
        public const int OnChooseNext = 5;
        public const int OnLeave = 6;
        public const int OnFinish = 7;
        public const int OnComplete = 8;
        public const int GetUsers = 9;
        public const int OnJoin = 11;
        public const int OnCurrentTurnDetail = 12;




        public const int Errors = 100;


        public const int TurnBasedLimit = 10; // 10 Request per sec
        public const int RestLimit = 1000; //  RestLimit per sec in long
    }
}