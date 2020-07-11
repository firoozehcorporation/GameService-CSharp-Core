using System;
using GProtocol.Utils;
using GProtocol.Utils.IO;

namespace FiroozehGameService.Models.GSLive.Command
{
    [Serializable]
    internal class Packet : APacket
    {
        internal int Action;
        internal string Token;
        internal byte[] Data;
        internal string Message;
        
        private int _tokenLen;
        private int _dataLen;
        private int _messageLen;

        public Packet(byte[] buffer)
        {
            Deserialize(buffer);
        }
        
        public Packet(string token, int action, byte[] data = null, string message = null)
        {
            Token = token;
            Action = action;
            Data = data;
            Message = message;
        }
        
        internal override byte[] Serialize()
        {
            byte haveToken = 0x0,haveData = 0x0,haveMessage = 0x0;
            short prefixLen = 4 * sizeof(byte);

            if (Token != null)
            {
                haveToken = 0x1;
                _tokenLen = Token.Length;
                prefixLen += sizeof(byte);
            }
            
            if (Data != null)
            {
                haveData = 0x1;
                _dataLen = Data.Length;
                prefixLen += sizeof(ushort);
            }

            if (Message != null)
            {
                haveMessage = 0x1;
                _messageLen = Message.Length;
                prefixLen += sizeof(byte);
            }
            
            
            var packetBuffer = BufferPool.GetBuffer(BufferSize(prefixLen));
            using (var packetWriter = ByteArrayReaderWriter.Get(packetBuffer))
            {
                // header Segment
                packetWriter.Write(haveToken);
                packetWriter.Write(haveData);
                packetWriter.Write(haveMessage);
                
                if(haveToken == 0x1)   packetWriter.Write((byte)_tokenLen);
                if(haveData == 0x1)    packetWriter.Write((ushort)_dataLen);
                if(haveMessage == 0x1) packetWriter.Write((byte)_messageLen);
                
                // Data Segment
                packetWriter.Write((byte)Action);
                if(haveToken == 0x1)    packetWriter.Write(ConvertToBytes(Token));
                if(haveData == 0x1)     packetWriter.Write(Data);
                if(haveMessage == 0x1)  packetWriter.Write(ConvertToBytes(Message));
            }
            
            return packetBuffer;
        }

        internal sealed override void Deserialize(byte[] buffer)
        {
            using (var packetWriter = ByteArrayReaderWriter.Get(buffer))
            {
                
                var haveToken = packetWriter.ReadByte();
                var haveData  = packetWriter.ReadByte();
                var haveMessage = packetWriter.ReadByte();
                
                 
                if(haveToken == 0x1)   _tokenLen = packetWriter.ReadByte();
                if(haveData == 0x1)    _dataLen = packetWriter.ReadUInt16();
                if(haveMessage == 0x1) _messageLen = packetWriter.ReadByte();
                
                Action = packetWriter.ReadByte();
                if(haveToken == 0x1)   Token = ConvertToString(packetWriter.ReadBytes(_tokenLen));
                if(haveData == 0x1)    Data = packetWriter.ReadBytes(_dataLen);
                if(haveMessage == 0x1) Message = ConvertToString(packetWriter.ReadBytes(_messageLen));
            }
        }

        internal override int BufferSize(short prefixLen)
        {
            return  prefixLen + _tokenLen + _dataLen + _messageLen;
        }
        
        public override string ToString()
        {
            return "Packet{" +
                   "Hash='" + Token + '\'' +
                   ", Action=" + Action +
                   ", Data='" + Data + '\'' +
                   ", Message='" + Message + '\'' +
                   '}';
        }
    }
}