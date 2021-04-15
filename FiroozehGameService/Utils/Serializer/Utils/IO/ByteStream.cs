using System;
using System.IO;

namespace FiroozehGameService.Utils.Serializer.Utils.IO
{
    /// <summary>
    ///     A simple stream implementation for reading/writing from/to byte arrays which can be reused
    /// </summary>
    internal class ByteStream : Stream
    {
        private byte[] _srcByteArray;

        public override long Position { get; set; }

        public override long Length => _srcByteArray.Length;

        public override bool CanRead => true;

        public override bool CanWrite => true;

        public override bool CanSeek => true;

        /// <summary>
        ///     Set a new byte array for this stream to read from
        /// </summary>
        internal void SetStreamSource(byte[] sourceBuffer)
        {
            _srcByteArray = sourceBuffer;
            Position = 0;
        }

        internal byte[] ReadBytes(int length)
        {
            var bytes = new byte[length];
            Read(bytes, 0, length);

            return bytes;
        }

        internal char ReadChar()
        {
            var c = 0;

            for (var i = 0; i < sizeof(char); i++) c |= ReadByte() << (i << 3);

            return (char) c;
        }

        internal char[] ReadChars(int length)
        {
            var chars = new char[length];

            for (var i = 0; i < length; i++)
                chars[i] = ReadChar();

            return chars;
        }

        internal string ReadString()
        {
            var len = ReadUInt32();
            var chars = ReadChars((int) len);
            return new string(chars);
        }

        internal short ReadInt16()
        {
            var c = 0;

            for (var i = 0; i < sizeof(short); i++) c |= ReadByte() << (i << 3);

            return (short) c;
        }

        internal int ReadInt32()
        {
            var c = 0;

            for (var i = 0; i < sizeof(int); i++) c |= ReadByte() << (i << 3);

            return c;
        }

        internal long ReadInt64()
        {
            long c = 0;

            for (var i = 0; i < sizeof(long); i++) c |= (long) ReadByte() << (i << 3);

            return c;
        }

        internal ushort ReadUInt16()
        {
            ushort c = 0;

            for (var i = 0; i < sizeof(ushort); i++) c |= (ushort) (ReadByte() << (i << 3));

            return c;
        }

        internal uint ReadUInt32()
        {
            uint c = 0;

            for (var i = 0; i < sizeof(uint); i++) c |= (uint) ReadByte() << (i << 3);

            return c;
        }

        internal ulong ReadUInt64()
        {
            ulong c = 0;

            for (var i = 0; i < sizeof(ulong); i++) c |= (ulong) ReadByte() << (i << 3);

            return c;
        }

        public override int Read(byte[] buffer, int offset, int count)
        {
            var readBytes = 0;
            var pos = Position;
            var len = Length;
            for (var i = 0; i < count && pos < len; i++)
            {
                buffer[i + offset] = _srcByteArray[pos++];
                readBytes++;
            }

            Position = pos;
            return readBytes;
        }

        internal new byte ReadByte()
        {
            var pos = Position;
            var val = _srcByteArray[pos++];
            Position = pos;

            return val;
        }

        public override void Write(byte[] buffer, int offset, int count)
        {
            for (var i = 0; i < count; i++)
                WriteByte(buffer[i + offset]);
        }

        public override void WriteByte(byte value)
        {
            var pos = Position;
            _srcByteArray[pos++] = value;
            Position = pos;
        }

        internal void Write(byte val)
        {
            WriteByte(val);
        }

        internal void Write(byte[] val)
        {
            Write(val, 0, val.Length);
        }

        internal void Write(char val)
        {
            uint uval = val;
            for (var i = 0; i < sizeof(char); i++)
            {
                WriteByte((byte) (uval & 0xFF));
                uval >>= 8;
            }
        }

        internal void Write(char[] val)
        {
            for (var i = 0; i < val.Length; i++) Write(val[i]);
        }

        internal void Write(string val)
        {
            Write((uint) val.Length);
            for (var i = 0; i < val.Length; i++) Write(val[i]);
        }

        internal void Write(short val)
        {
            for (var i = 0; i < sizeof(short); i++)
            {
                WriteByte((byte) (val & 0xFF));
                val >>= 8;
            }
        }

        internal void Write(int val)
        {
            for (var i = 0; i < sizeof(int); i++)
            {
                WriteByte((byte) (val & 0xFF));
                val >>= 8;
            }
        }

        internal void Write(long val)
        {
            for (var i = 0; i < sizeof(long); i++)
            {
                WriteByte((byte) (val & 0xFF));
                val >>= 8;
            }
        }

        internal void Write(ushort val)
        {
            for (var i = 0; i < sizeof(ushort); i++)
            {
                WriteByte((byte) (val & 0xFF));
                val >>= 8;
            }
        }

        internal void Write(uint val)
        {
            for (var i = 0; i < sizeof(uint); i++)
            {
                WriteByte((byte) (val & 0xFF));
                val >>= 8;
            }
        }

        internal void Write(ulong val)
        {
            for (var i = 0; i < sizeof(ulong); i++)
            {
                WriteByte((byte) (val & 0xFF));
                val >>= 8;
            }
        }

        public override void Flush()
        {
        }

        public override long Seek(long offset, SeekOrigin origin)
        {
            if (origin == SeekOrigin.Begin)
                Position = offset;
            else if (origin == SeekOrigin.Current)
                Position += offset;
            else
                Position = Length - offset - 1;

            return Position;
        }

        public override void SetLength(long value)
        {
            throw new NotImplementedException();
        }
    }
}