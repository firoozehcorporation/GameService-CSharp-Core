using FiroozehGameService.Models.GSLive;

namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal interface IDeserializer
    {
        APacket Deserialize(byte[] buffer, int offset, int receivedBytes);
        APacket Deserialize(string buffer);
    }
}