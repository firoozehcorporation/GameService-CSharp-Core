// <copyright file="SerializerUtil.cs" company="Firoozeh Technology LTD">
// Copyright (C) 2020 Firoozeh Technology LTD. All Rights Reserved.
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
using System.Collections.Generic;
using System.Linq;
using System.Text;
using FiroozehGameService.Models;
using FiroozehGameService.Models.Enums;
using FiroozehGameService.Utils.Serializer.Abstracts;
using FiroozehGameService.Utils.Serializer.Helpers;
using FiroozehGameService.Utils.Serializer.Models;
using FiroozehGameService.Utils.Serializer.Utils.IO;

namespace FiroozehGameService.Utils.Serializer.Utils
{
    internal static class SerializerUtil
    {
        private static Tuple<ushort, List<ObjectInfo>> GetInfos(GsWriteStream writeStream)
        {
            var infos = new List<ObjectInfo>();
            var bufferSize = sizeof(byte); // Size of ObjectsInfo

            while (writeStream.CanRead())
            {
                bufferSize += sizeof(byte); // Add Type Size
                var obj = writeStream.GetObject();
                switch (obj)
                {
                    case null:
                        infos.Add(new ObjectInfo(null, Types.Null));
                        break;
                    case bool _:
                        infos.Add(new ObjectInfo(obj, Types.Bool));
                        bufferSize += sizeof(byte);
                        break;
                    case byte _:
                        infos.Add(new ObjectInfo(obj, Types.Byte));
                        bufferSize += sizeof(byte);
                        break;
                    case char _:
                        infos.Add(new ObjectInfo(obj, Types.Char));
                        bufferSize += sizeof(char);
                        break;
                    case double _:
                        infos.Add(new ObjectInfo(obj, Types.Double));
                        bufferSize += sizeof(double);
                        break;
                    case float _:
                        infos.Add(new ObjectInfo(obj, Types.Float));
                        bufferSize += sizeof(float);
                        break;
                    case int _:
                        infos.Add(new ObjectInfo(obj, Types.Int));
                        bufferSize += sizeof(int);
                        break;
                    case long _:
                        infos.Add(new ObjectInfo(obj, Types.Long));
                        bufferSize += sizeof(long);
                        break;
                    case short _:
                        infos.Add(new ObjectInfo(obj, Types.Short));
                        bufferSize += sizeof(short);
                        break;
                    case uint _:
                        infos.Add(new ObjectInfo(obj, Types.Uint));
                        bufferSize += sizeof(uint);
                        break;
                    case ulong _:
                        infos.Add(new ObjectInfo(obj, Types.Ulong));
                        bufferSize += sizeof(ulong);
                        break;
                    case ushort _:
                        infos.Add(new ObjectInfo(obj, Types.Ushort));
                        bufferSize += sizeof(ushort);
                        break;
                    case string s:
                        var sBuffer = GetBuffer(s, true);
                        infos.Add(new ObjectInfo(sBuffer, Types.String));
                        bufferSize += sizeof(ushort) + sBuffer.Length;
                        break;
                    case byte[] ba:
                        infos.Add(new ObjectInfo(ba, Types.ByteArray));
                        bufferSize += sizeof(ushort) + ba.Length;
                        break;
                    case char[] ca:
                        var cBuffer = GetBuffer(ca, true);
                        infos.Add(new ObjectInfo(cBuffer, Types.CharArray));
                        bufferSize += sizeof(ushort) + cBuffer.Length;
                        break;
                    case double[] da:
                        var dBuffer = GetBuffer(da);
                        infos.Add(new ObjectInfo(dBuffer, Types.DoubleArray));
                        bufferSize += sizeof(ushort) + dBuffer.Length;
                        break;
                    case float[] fa:
                        var fBuffer = GetBuffer(fa);
                        infos.Add(new ObjectInfo(fBuffer, Types.FloatArray));
                        bufferSize += sizeof(ushort) + fBuffer.Length;
                        break;
                    case int[] ia:
                        var iBuffer = GetBuffer(ia);
                        infos.Add(new ObjectInfo(iBuffer, Types.IntArray));
                        bufferSize += sizeof(ushort) + iBuffer.Length;
                        break;
                    case long[] la:
                        var lBuffer = GetBuffer(la);
                        infos.Add(new ObjectInfo(lBuffer, Types.LongArray));
                        bufferSize += sizeof(ushort) + lBuffer.Length;
                        break;
                    case string[] sa:
                        var saBuffer = GetBuffer(sa);
                        infos.Add(new ObjectInfo(saBuffer, Types.StringArray));
                        bufferSize += sizeof(ushort) + saBuffer.Length;
                        break;
                    case BaseSerializer _:
                        infos.Add(new ObjectInfo(obj, Types.CustomObject));
                        break;
                    default:
                        if (TypeUtil.HaveType(obj))
                        {
                            var (hash, stream) = TypeUtil.GetWriteStream(obj);
                            var bufferObj = Serialize(stream);
                            infos.Add(new ObjectInfo(bufferObj, Types.CustomObject, hash));
                            bufferSize += sizeof(int) + sizeof(ushort) + bufferObj.Length;
                        }
                        else
                        {
                            throw new GameServiceException("SerializerUtil -> The Type " + obj.GetType() +
                                                           " is Not Supported")
                                .LogException(typeof(SerializerUtil), DebugLocation.RealTime, "GetInfos");
                        }

                        break;
                }
            }


            if (bufferSize >= ushort.MaxValue)
                throw new GameServiceException("SerializerUtil -> The Buffer is Too Large!")
                    .LogException(typeof(SerializerUtil), DebugLocation.RealTime, "GetInfos");

            return Tuple.Create((ushort) bufferSize, infos);
        }

        internal static byte[] Serialize(GsWriteStream writeStream)
        {
            var (bufferSize, objectInfos) = GetInfos(writeStream);

            var packetBuffer = BufferPool.GetBuffer(bufferSize);
            using (var packetWriter = ByteArrayReaderWriter.Get(packetBuffer))
            {
                packetWriter.Write((byte) objectInfos.Count);

                foreach (var objectInfo in objectInfos)
                {
                    packetWriter.Write((byte) objectInfo.Type);
                    switch (objectInfo.Type)
                    {
                        case Types.Byte:
                            packetWriter.Write((byte) objectInfo.Src);
                            break;
                        case Types.Char:
                            packetWriter.Write((char) objectInfo.Src);
                            break;
                        case Types.Double:
                            packetWriter.Write(BitConverter.GetBytes((double) objectInfo.Src));
                            break;
                        case Types.Float:
                            packetWriter.Write(BitConverter.GetBytes((float) objectInfo.Src));
                            break;
                        case Types.Int:
                            packetWriter.Write((int) objectInfo.Src);
                            break;
                        case Types.Long:
                            packetWriter.Write((long) objectInfo.Src);
                            break;
                        case Types.Short:
                            packetWriter.Write((short) objectInfo.Src);
                            break;
                        case Types.Uint:
                            packetWriter.Write((uint) objectInfo.Src);
                            break;
                        case Types.Ushort:
                            packetWriter.Write((ushort) objectInfo.Src);
                            break;
                        case Types.Ulong:
                            packetWriter.Write((ulong) objectInfo.Src);
                            break;
                        case Types.Bool:
                            byte data = 0x0;
                            if ((bool) objectInfo.Src) data = 0x1;
                            packetWriter.Write(data);
                            break;
                        case Types.CustomObject:
                            var bufferDataObject = (byte[]) objectInfo.Src;
                            packetWriter.Write((int) objectInfo.Extra);
                            packetWriter.Write((ushort) bufferDataObject.Length);
                            packetWriter.Write(bufferDataObject);
                            break;
                        case Types.String:
                        case Types.ByteArray:
                        case Types.CharArray:
                        case Types.DoubleArray:
                        case Types.FloatArray:
                        case Types.IntArray:
                        case Types.LongArray:
                        case Types.StringArray:
                            var bArray = (byte[]) objectInfo.Src;
                            packetWriter.Write((ushort) bArray.Length);
                            packetWriter.Write(bArray);
                            break;
                        case Types.Null: break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            return packetBuffer;
        }

        internal static GsReadStream Deserialize(byte[] buffer)
        {
            var readStream = new GsReadStream();

            using (var packetReader = ByteArrayReaderWriter.Get(buffer))
            {
                var count = packetReader.ReadByte();
                for (var i = 0; i < count; i++)
                {
                    var type = (Types) packetReader.ReadByte();
                    switch (type)
                    {
                        case Types.Bool:
                            readStream.Add(packetReader.ReadByte() != 0x0);
                            break;
                        case Types.Byte:
                            readStream.Add(packetReader.ReadByte());
                            break;
                        case Types.Char:
                            readStream.Add(packetReader.ReadChar());
                            break;
                        case Types.Double:
                            readStream.Add(BitConverter.ToDouble(packetReader.ReadBytes(sizeof(double)), 0));
                            break;
                        case Types.Float:
                            readStream.Add(BitConverter.ToSingle(packetReader.ReadBytes(sizeof(float)), 0));
                            break;
                        case Types.Short:
                            readStream.Add(packetReader.ReadInt16());
                            break;
                        case Types.Int:
                            readStream.Add(packetReader.ReadInt32());
                            break;
                        case Types.Long:
                            readStream.Add(packetReader.ReadInt64());
                            break;
                        case Types.Ushort:
                            readStream.Add(packetReader.ReadUInt16());
                            break;
                        case Types.Uint:
                            readStream.Add(packetReader.ReadUInt32());
                            break;
                        case Types.Ulong:
                            readStream.Add(packetReader.ReadUInt64());
                            break;
                        case Types.String:
                            readStream.Add(GetStringFromBuffer(packetReader.ReadBytes(packetReader.ReadUInt16()),
                                true));
                            break;
                        case Types.ByteArray:
                            readStream.Add(packetReader.ReadBytes(packetReader.ReadUInt16()));
                            break;
                        case Types.CharArray:
                            readStream.Add(GetCharsFromBuffers(packetReader.ReadBytes(packetReader.ReadUInt16())));
                            break;
                        case Types.DoubleArray:
                            readStream.Add(GetDoublesFromBuffers(packetReader.ReadBytes(packetReader.ReadUInt16())));
                            break;
                        case Types.FloatArray:
                            readStream.Add(GetFloatsFromBuffers(packetReader.ReadBytes(packetReader.ReadUInt16())));
                            break;
                        case Types.IntArray:
                            readStream.Add(GetIntsFromBuffers(packetReader.ReadBytes(packetReader.ReadUInt16())));
                            break;
                        case Types.LongArray:
                            readStream.Add(GetLongsFromBuffers(packetReader.ReadBytes(packetReader.ReadUInt16())));
                            break;
                        case Types.StringArray:
                            readStream.Add(GetStringsFromBuffer(packetReader.ReadBytes(packetReader.ReadUInt16())));
                            break;
                        case Types.Null:
                            readStream.Add(null);
                            break;
                        case Types.CustomObject:
                            var id = packetReader.ReadInt32();
                            var bufferData = packetReader.ReadBytes(packetReader.ReadUInt16());
                            readStream.Add(TypeUtil.GetFinalObject(id, Deserialize(bufferData)));
                            break;
                        default:
                            throw new ArgumentOutOfRangeException();
                    }
                }
            }

            return readStream;
        }


        internal static List<SnapShotData> GetSnapShotsFromBuffer(byte[] buffer)
        {
            var data = new List<SnapShotData>();
            using (var packetReader = ByteArrayReaderWriter.Get(buffer))
            {
                var count = packetReader.ReadUInt16();
                for (var i = 0; i < count; i++)
                {
                    var type = (SnapShotType) packetReader.ReadByte();
                    var ownerId = packetReader.ReadBytes(packetReader.ReadByte());
                    var payload = packetReader.ReadBytes(packetReader.ReadUInt16());
                    data.Add(new SnapShotData(type, GetStringFromBuffer(ownerId, true), payload));
                }
            }

            return data;
        }


        internal static byte[] GetSendQueueBuffer(Queue<byte[]> queue)
        {
            var bufferSize = GetSendQueueBufferSize(queue);
            var packetBuffer = BufferPool.GetBuffer(bufferSize);
            using (var packetWriter = ByteArrayReaderWriter.Get(packetBuffer))
            {
                packetWriter.Write((byte) queue.Count);
                while (queue.Count > 0)
                {
                    var item = queue.Dequeue();
                    packetWriter.Write((ushort) item.Length);
                    packetWriter.Write(item);
                }
            }

            return packetBuffer;
        }


        internal static Queue<byte[]> GetQueueData(byte[] buffer)
        {
            var data = new Queue<byte[]>();
            using (var packetReader = ByteArrayReaderWriter.Get(buffer))
            {
                var count = packetReader.ReadByte();
                for (var i = 0; i < count; i++)
                    data.Enqueue(packetReader.ReadBytes(packetReader.ReadUInt16()));
            }

            return data;
        }


        internal static Tuple<string, byte[]> GetObserver(byte[] buffer)
        {
            using (var packetReader = ByteArrayReaderWriter.Get(buffer))
            {
                var len = packetReader.ReadByte();
                var ownerId = GetStringFromBuffer(packetReader.ReadBytes(len), true);
                var payload = packetReader.ReadBytes(buffer.Length - len);
                return Tuple.Create(ownerId, payload);
            }
        }


        internal static int GetSendQueueBufferSize(IEnumerable<byte[]> data)
        {
            return data.Sum(i => i.Length + sizeof(ushort)) + sizeof(byte);
        }

        private static byte[] GetBuffer(string data, bool isUtf)
        {
            return isUtf ? Encoding.UTF8.GetBytes(data) : Encoding.ASCII.GetBytes(data);
        }

        private static byte[] GetBuffer(char[] data, bool isUtf)
        {
            return isUtf ? Encoding.UTF8.GetBytes(data) : Encoding.ASCII.GetBytes(data);
        }

        private static byte[] GetBuffer(IEnumerable<double> values)
        {
            return values.SelectMany(BitConverter.GetBytes).ToArray();
        }

        private static byte[] GetBuffer(IEnumerable<float> values)
        {
            return values.SelectMany(BitConverter.GetBytes).ToArray();
        }

        private static byte[] GetBuffer(IEnumerable<int> values)
        {
            return values.SelectMany(BitConverter.GetBytes).ToArray();
        }


        private static byte[] GetBuffer(IEnumerable<long> values)
        {
            return values.SelectMany(BitConverter.GetBytes).ToArray();
        }

        private static byte[] GetBuffer(IReadOnlyCollection<string> values)
        {
            var buffer = new List<byte>();

            if (values.Count > ushort.MaxValue)
                throw new GameServiceException("GetBuffer Err -> String Array is Too Large!")
                    .LogException(typeof(SerializerUtil), DebugLocation.RealTime, "GetBuffer");

            var count = BitConverter.GetBytes((ushort) values.Count);
            buffer.AddRange(count);

            foreach (var value in values)
            {
                if (value.Length > ushort.MaxValue)
                    throw new GameServiceException("GetBuffer Err -> String Array Element is Too Large!")
                        .LogException(typeof(SerializerUtil), DebugLocation.RealTime, "GetBuffer");

                var len = BitConverter.GetBytes((ushort) value.Length);
                var data = GetBuffer(value, true);

                buffer.AddRange(len);
                buffer.AddRange(data);
            }

            return buffer.ToArray();
        }


        private static string[] GetStringsFromBuffer(byte[] bytes)
        {
            var data = new List<string>();
            using (var packetReader = ByteArrayReaderWriter.Get(bytes))
            {
                var count = packetReader.ReadUInt16();
                for (var i = 0; i < count; i++)
                    data.Add(GetStringFromBuffer(packetReader.ReadBytes(packetReader.ReadUInt16()), true));
            }

            return data.ToArray();
        }

        private static double[] GetDoublesFromBuffers(byte[] bytes)
        {
            var result = new double[bytes.Length / sizeof(double)];
            Buffer.BlockCopy(bytes, 0, result, 0, bytes.Length);
            return result;
        }

        private static float[] GetFloatsFromBuffers(byte[] bytes)
        {
            var result = new float[bytes.Length / sizeof(float)];
            Buffer.BlockCopy(bytes, 0, result, 0, bytes.Length);
            return result;
        }

        private static long[] GetLongsFromBuffers(byte[] bytes)
        {
            var result = new long[bytes.Length / sizeof(long)];
            Buffer.BlockCopy(bytes, 0, result, 0, bytes.Length);
            return result;
        }

        private static int[] GetIntsFromBuffers(byte[] bytes)
        {
            var result = new int[bytes.Length / sizeof(int)];
            Buffer.BlockCopy(bytes, 0, result, 0, bytes.Length);
            return result;
        }

        private static char[] GetCharsFromBuffers(byte[] bytes)
        {
            var result = new char[bytes.Length / sizeof(char)];
            Buffer.BlockCopy(bytes, 0, result, 0, bytes.Length);
            return result;
        }


        private static string GetStringFromBuffer(byte[] buffer, bool isUtf)
        {
            return isUtf ? Encoding.UTF8.GetString(buffer) : Encoding.ASCII.GetString(buffer);
        }
    }
}