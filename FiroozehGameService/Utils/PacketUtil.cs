using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.GSLive.RT;

namespace FiroozehGameService.Utils
{
    internal static class PacketUtil
    {
        internal static bool CheckPacketSize(byte[] buffer)
        {
            return buffer.Length <= RT.MaxPacketSize;
        }
    }
}