using FiroozehGameService.Models.Enums.GSLive;

namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal interface IDeserializer
    {
        string Deserialize(byte[] buffer,int offset,int receivedBytes,string pwd,GSLiveType type);
        string Deserialize(byte[] buffer,string pwd,GSLiveType type);
    }
}