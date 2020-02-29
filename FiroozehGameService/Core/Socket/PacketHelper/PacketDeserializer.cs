using System;
using System.Linq;
using FiroozehGameService.Utils;
using GameServiceHelper.Utils;

namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal class PacketDeserializer : IDeserializer
    {
        public string Deserialize(byte[] buffer, int offset, int receivedBytes, string pwd)
        {
            try
            {
                var seg = new ArraySegment<byte>(buffer,offset,receivedBytes);
                var deserialize = Serializer.Deserialize(seg.ToArray(), pwd);
                LogUtil.Log(this,"PacketDeserializer : " + deserialize);
                return deserialize;
            }
            catch (Exception e)
            {
                LogUtil.LogError(this,"PacketDeserializer Err : " + e.Message);
                return null;
            }
           
        }

        public string Deserialize(byte[] buffer, string pwd)
        {
            try
            {
                LogUtil.Log(this,"PacketDeserializer Rec :" + buffer.Length +" Bytes");
                return Serializer.Deserialize(buffer, pwd);
            }
            catch (Exception e)
            {
                LogUtil.LogError(this,"PacketDeserializer Err : " + e.Message);
                return null;
            }
        }
    }
}