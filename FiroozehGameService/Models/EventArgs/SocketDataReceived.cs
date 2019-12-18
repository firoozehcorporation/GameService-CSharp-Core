using FiroozehGameService.Models.Enums;

namespace FiroozehGameService.Models.EventArgs
{
    internal class SocketDataReceived : System.EventArgs
    {
        internal string Data { set; get; }
        internal GProtocolSendType Type { set; get; }
    }
}
