using System;
using System.Text;
using FiroozehGameService.Models.Consts;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Models.GSLive.Command;

namespace FiroozehGameService.Utils.Encryptor
{
    internal static class GsEncryptor
    {
        internal const string Tcp = "tcp";
        internal const string TcpSec = "tcp-sec";
        internal const string WebSocketSec = "wss";

        internal static void EncryptPacket(this Packet packet, string key)
        {
            if (!string.IsNullOrEmpty(packet.Message)) packet.Message = EncryptSrt(packet.Message, key);
            if (!string.IsNullOrEmpty(packet.Data)) packet.Data = EncryptSrt(packet.Data, key);
        }

        internal static void DecryptPacket(this Packet packet, string key)
        {
            if (!string.IsNullOrEmpty(packet.Message)) packet.Message = DecryptSrt(packet.Message, key);
            if (!string.IsNullOrEmpty(packet.Data)) packet.Data = DecryptSrt(packet.Data, key);
        }

        internal static bool IsEncryptionEnabled(this Packet packet, bool isCommand, bool isSerializer)
        {
            if (isSerializer)
            {
                if (isCommand) return packet.Action != CommandConst.ActionAuth;
                return packet.Action != TurnBasedConst.ActionAuth;
            }

            if (isCommand) return packet.Action != CommandConst.Error && packet.Action != CommandConst.ActionVoiceError;
            return packet.Action != TurnBasedConst.Errors;
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