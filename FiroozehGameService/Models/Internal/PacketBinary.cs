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
    }
}