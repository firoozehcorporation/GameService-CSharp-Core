namespace FiroozehGameService.Models.EventArgs
{
    internal class SocketDataReceived : System.EventArgs
    {
        internal string Data { set; get; }
        internal long Time { set; get; }
    }
}