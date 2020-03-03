using System;
using System.Linq;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Utils;
using GameServiceHelper.Utils;

namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal class PacketDeserializer : IDeserializer
    {
        public string Deserialize(byte[] buffer, int offset, int receivedBytes,string pwd,GSLiveType type)
        {
            try
            {
                var seg = new ArraySegment<byte>(buffer,offset,receivedBytes);
                var deserialize = Serializer.Deserialize(seg.ToArray(), KeyTypeUtil.GetPwd(pwd,type));
               
                LogUtil.Log(this,"PacketDeserializer : " + deserialize);
                return deserialize;
            }
            catch (Exception e)
            {
                LogUtil.LogError(this,"PacketDeserializer Err : " + e.Message);
                return null;
            }
           
        }

        public string Deserialize(byte[] buffer,string pwd,GSLiveType type)
        {
            try
            {
                LogUtil.Log(this,"PacketDeserializer Rec :" + buffer.Length +" Bytes");
                return Serializer.Deserialize(buffer, KeyTypeUtil.GetPwd(pwd,type));
            }
            catch (Exception e)
            {
                LogUtil.LogError(this,"PacketDeserializer Err : " + e.Message);
                return null;
            }
        }
    }
}