using System;
using System.Text;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Utils.Encryptor
{
    internal static class GsEncryptor
    {
        internal static void EncryptPacket(this Packet packet, string key)
        {
            if (packet.Message != null) packet.Message = EncryptSrt(packet.Message, key);
            if (packet.Data != null) packet.Data = EncryptSrt(packet.Data, key);
        }

        internal static void DecryptPacket(this Packet packet, string key)
        {
            if (packet.Message != null) packet.Message = DecryptSrt(packet.Message, key);
            if (packet.Data != null) packet.Data = DecryptSrt(packet.Data, key);
        }


        private static string EncryptSrt(string srt, string key)
        {
            try
            {
                var srtBytes = Encoding.UTF8.GetBytes(srt);
                var keyBytes = Encoding.UTF8.GetBytes(key);
                var encrypted = Rc4.Apply(srtBytes, keyBytes);

                return Convert.ToBase64String(encrypted);
            }
            catch (Exception e)
            {
                e.LogException(typeof(GsEncryptor), DebugLocation.Internal, "EncryptSrt");
                return srt;
            }
        }

        private static string DecryptSrt(string srt, string key)
        {
            try
            {
                var srtBytes = Convert.FromBase64String(srt);
                var keyBytes = Encoding.UTF8.GetBytes(key);
                var decrypted = Rc4.Apply(srtBytes, keyBytes);

                return Encoding.UTF8.GetString(decrypted);
            }
            catch (Exception e)
            {
                e.LogException(typeof(GsEncryptor), DebugLocation.Internal, "DecryptSrt");
                return srt;
            }
        }
    }
}