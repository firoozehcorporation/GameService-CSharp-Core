namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal interface IDeserializer
    {
        string Deserialize(byte[] buffer,int offset,int receivedBytes,string pwd);
        string Deserialize(byte[] buffer, string pwd);
    }
}