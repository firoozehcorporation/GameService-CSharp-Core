using System;
using System.Linq;
using System.Text;
using FiroozehGameService.Models.Enums.GSLive;
using FiroozehGameService.Models.GSLive;
using FiroozehGameService.Models.GSLive.Command;
using FiroozehGameService.Utils;
using GameServiceHelper.Utils;
using Newtonsoft.Json;

namespace FiroozehGameService.Core.Socket.PacketHelper
{
    internal class PacketSerializer : ISerializer
    {
        private readonly byte[] _ter = {0x00, 0x10};
        public byte[] Serialize(APacket packet,string pwd,GSLiveType type)
        {
            try
            {
                var json = JsonConvert.SerializeObject(packet , new JsonSerializerSettings { NullValueHandling = NullValueHandling.Ignore });
                //var s = Serializer.Serialize(json,KeyTypeUtil.GetPwd(pwd,type));
                LogUtil.Log(this,"PacketSerializer > JSON : " + json);
                return Encoding.UTF8.GetBytes(json);
            }
            catch (Exception e)
            {
                LogUtil.LogError(this,"PacketSerializer Err :" + e.Message);
                return null;
            }
          
        }
        
        private static byte[] Combine(params byte[][] arrays)
        {
            var rv = new byte[arrays.Sum(a => a.Length)];
            var offset = 0;
            foreach (var array in arrays) {
                Buffer.BlockCopy(array, 0, rv, offset, array.Length);
                offset += array.Length;
            }
            return rv;
        }
    }
}