namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal interface IDeserializer
    {
        string Deserialize(byte[] buffer,int offset,int receivedBytes,string k);
    }
}