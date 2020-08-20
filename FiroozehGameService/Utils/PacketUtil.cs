using FiroozehGameService.Models.Consts;

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