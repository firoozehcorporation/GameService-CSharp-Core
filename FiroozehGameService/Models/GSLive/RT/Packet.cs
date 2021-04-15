// <copyright file="Packet.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2019 Firoozeh Technology LTD. All Rights Reserved.
//
//  Licensed under the Apache License, Version 2.0 (the "License");
//  you may not use this file except in compliance with the License.
//  You may obtain a copy of the License at
//
//  http://www.apache.org/licenses/LICENSE-2.0
//
//  Unless required by applicable law or agreed to in writing, software
//  distributed under the License is distributed on an "AS IS" BASIS,
//  WITHOUT WARRANTIES OR CONDITIONS OF ANY KIND, either express or implied.
//  See the License for the specific language governing permissions and
//    limitations under the License.
// </copyright>


/**
* @author Alireza Ghodrati
*/


using System;
using System.Text;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Utils.Serializer.Utils.IO;

namespace FiroozehGameService.Models.GSLive.RT
{
    [Serializable]
    internal class Packet : APacket
    {
        private int _payloadLen;
        internal int Action;
        internal long ClientReceiveTime;
        internal long ClientSendTime;
        internal ulong Hash;
        internal byte[] Payload;
        internal GProtocolSendType SendType;

        public Packet(byte[] buffer)
        {
            Deserialize(buffer);
        }

        public Packet(ulong hash, int action, GProtocolSendType sendType = GProtocolSendType.UnReliable,
            byte[] payload = null)
        {
            Hash = hash;
            Action = action;
            Payload = payload;
            SendType = sendType;
        }

        internal override byte[] Serialize()
        {
            byte havePayload = 0x0, haveSendTime = 0x0;
            short prefixLen = 4 * sizeof(byte) + sizeof(ulong);


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
                packetWriter.Write((byte) Action);
                packetWriter.Write(haveSendTime);
                packetWriter.Write(havePayload);

                if (havePayload == 0x1) packetWriter.Write((ushort) _payloadLen);

                // data Segment
                packetWriter.Write((byte) SendType);
                packetWriter.Write(Hash);

                if (havePayload == 0x1) packetWriter.Write(Payload);
                if (haveSendTime == 0x1) packetWriter.Write(ClientSendTime);
            }

            return packetBuffer;
        }

        internal sealed override void Deserialize(byte[] buffer)
        {
            using (var packetWriter = ByteArrayReaderWriter.Get(buffer))
            {
                Action = packetWriter.ReadByte();

                var haveSendTime = packetWriter.ReadByte();
                var havePayload = packetWriter.ReadByte();

                if (havePayload == 0x1) _payloadLen = packetWriter.ReadUInt16();

                SendType = (GProtocolSendType) packetWriter.ReadByte();
                Hash = packetWriter.ReadUInt64();

                if (havePayload == 0x1) Payload = packetWriter.ReadBytes(_payloadLen);
                if (haveSendTime == 0x1) ClientSendTime = packetWriter.ReadInt64();
            }
        }

        internal override int BufferSize(short prefixLen)
        {
            return prefixLen + _payloadLen;
        }

        public override string ToString()
        {
            return "Packet{" +
                   "Hash='" + Hash + '\'' +
                   ", Action=" + Action + '\'' +
                   ", type = " + SendType + '\'' +
                   ", payload = " + Encoding.UTF8.GetString(Payload ?? new byte[0]) + '\'' +
                   ", SendTime = " + ClientSendTime + '\'' +
                   '}';
        }
    }
}