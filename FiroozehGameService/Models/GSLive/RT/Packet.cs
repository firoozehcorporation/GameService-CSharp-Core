using System;
using System.Text;
using FiroozehGameService.Models.Enums;
using GProtocol.Utils;
using GProtocol.Utils.IO;

namespace FiroozehGameService.Models.GSLive.RT
{
    [Serializable]
    internal class Packet : APacket
    {
        internal int Action;
        internal long ClientReceiveTime;
        internal long ClientSendTime;
        internal string Hash;
        internal byte[] Payload;
        internal GProtocolSendType SendType;

        
        private int _hashLen;
        private int _payloadLen;

        public Packet(byte[] buffer)
        {
            Deserialize(buffer);
        }
        public Packet(string hash, int action, GProtocolSendType sendType = GProtocolSendType.UnReliable,
            byte[] payload = null)
        {
            Hash = hash;
            Action = action;
            Payload = payload;
            SendType = sendType;
        }
        
        internal override byte[] Serialize()
        {
            byte haveToken = 0x0,havePayload = 0x0,haveSendTime = 0x0;
            short prefixLen = 5 * sizeof(byte);

            if (Hash != null)
            {
                haveToken = 0x1;
                _hashLen = Hash.Length;
                prefixLen += sizeof(byte);
            }
            
            if (Payload != null)
            {
                havePayload = 0x1;
                _payloadLen = Payload.Length;
                prefixLen += sizeof(ushort);
            }
            
            if (ClientSendTime != 0L)
            {
                haveSendTime = 0x1;
                prefixLen += sizeof(long);
            }
            
            var packetBuffer = BufferPool.GetBuffer(BufferSize(prefixLen));
            using (var packetWriter = ByteArrayReaderWriter.Get(packetBuffer))
            {
                // header Segment
                packetWriter.Write((byte)Action);
                packetWriter.Write(haveToken);
                packetWriter.Write(haveSendTime);
                packetWriter.Write(havePayload);
                
                if(havePayload == 0x1)  packetWriter.Write((ushort)_payloadLen);
                if(haveToken == 0x1)    packetWriter.Write((byte)_hashLen);

                // data Segment
                packetWriter.Write((byte)SendType);
                if(havePayload == 0x1)  packetWriter.Write(Payload);
                if(haveToken == 0x1)    packetWriter.Write(ConvertToBytes(Hash));  
                if(haveSendTime == 0x1) packetWriter.Write(ClientSendTime);
            }
            
            return packetBuffer;
        }

        internal sealed override void Deserialize(byte[] buffer)
        {
            using (var packetWriter = ByteArrayReaderWriter.Get(buffer))
            {
                Action = packetWriter.ReadByte();

                var haveToken = packetWriter.ReadByte();
                var haveSendTime = packetWriter.ReadByte();
                var havePayload = packetWriter.ReadByte();

                if(havePayload == 0x1) _payloadLen = packetWriter.ReadUInt16();
                if(haveToken == 0x1)   _hashLen = packetWriter.ReadByte();
                 
                SendType = (GProtocolSendType) packetWriter.ReadByte();
                
                if(havePayload == 0x1)    Payload = packetWriter.ReadBytes(_payloadLen);
                if(haveToken == 0x1)      Hash = ConvertToString(packetWriter.ReadBytes(_hashLen));
                if(haveSendTime == 0x1)   ClientSendTime = packetWriter.ReadInt64();
            }
        }

        internal override int BufferSize(short prefixLen)
        {
            return prefixLen + _hashLen + _payloadLen;
        }
        
        public override string ToString()
        {
            return "Packet{" +
                   "Hash='" + Hash + '\'' +
                   ", Action=" + Action +  '\'' +
                   ", type = " + SendType + '\'' +
                   ", payload = " + Encoding.UTF8.GetString(Payload ?? new byte[0]) + '\'' +
                   ", SendTime = " + ClientSendTime + '\'' +
                   '}';
        }

    }
}