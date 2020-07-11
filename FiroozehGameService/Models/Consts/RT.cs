namespace FiroozehGameService.Models.Consts
{
    internal static class RT
    {
        // Packet Actions
        internal const int ActionAuth = 1;
        internal const int ActionData = 2;
        internal const int ActionPublicMessage = 3;
        internal const int ActionPrivateMessage = 4;
        internal const int ActionJoin = 5;
        internal const int ActionMembersDetail = 6;
        internal const int ActionLeave = 7;
        internal const int ActionPing = 10;
        internal const int Error = 100;


        internal const int MaxPacketSize = 1 * 1024;

        // Limit Checker
        internal const int RealTimeLimit = 30; // 60 Request per sec
        internal const int RestLimit = 1000; //  RestLimit per sec in long
    }
}