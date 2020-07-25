using FiroozehGameService.Models.GSLive;

namespace FiroozehGameService.Models.EventArgs
{
    internal class SocketDataReceived : System.EventArgs
    {
        internal APacket Packet { set; get; }
        internal long Time { set; get; }
    }
}