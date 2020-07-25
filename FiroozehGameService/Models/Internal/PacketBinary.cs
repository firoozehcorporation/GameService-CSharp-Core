using System;
using System.Text;

namespace FiroozehGameService.Models.Internal
{
    [Serializable]
    internal abstract class PacketBinary
    {
        internal abstract byte[] Serialize();
        internal abstract void Deserialize(byte[] buffer);
        internal abstract int BufferSize(short prefixLen);
<<<<<<< HEAD
=======

        internal string ConvertToString(byte[] data)
        {
            return Encoding.UTF8.GetString(data);
        }

        internal byte[] ConvertToBytes(string data)
        {
            return Encoding.UTF8.GetBytes(data);
        }
>>>>>>> Development
    }
}