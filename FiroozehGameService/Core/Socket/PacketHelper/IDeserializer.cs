using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive;

namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal interface IDeserializer
    {
        APacket Deserialize(byte[] buffer, int offset, int receivedBytes, GSLiveType type);
    }
}