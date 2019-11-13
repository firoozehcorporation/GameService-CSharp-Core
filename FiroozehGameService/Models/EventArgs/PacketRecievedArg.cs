
namespace FiroozehGameService.Models.EventArgs
{
    internal class SocketDataReceived : System.EventArgs
    {
        public byte[] Buffer { set; get; }

    }
}
