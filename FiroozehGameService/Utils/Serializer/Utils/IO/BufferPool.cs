using System;
using System.Collections.Generic;

namespace FiroozehGameService.Utils.Serializer.Utils.IO
{
    /// <summary>
    ///     Helper methods for allocating temporary buffers
    /// </summary>
    internal static class BufferPool
    {
        private static readonly Dictionary<int, Queue<byte[]>> bufferPool = new Dictionary<int, Queue<byte[]>>();

        /// <summary>
        ///     Retrieve a buffer of the given size
        /// </summary>
        internal static byte[] GetBuffer(int size)
        {
            lock (bufferPool)
            {
                if (!bufferPool.ContainsKey(size)) return new byte[size];
                if (bufferPool[size].Count > 0)
                    return bufferPool[size].Dequeue();
            }

            return new byte[size];
        }

        /// <summary>
        ///     Return a buffer to the pool
        /// </summary>
        internal static void ReturnBuffer(byte[] buffer)
        {
            lock (bufferPool)
            {
                if (!bufferPool.ContainsKey(buffer.Length))
                    bufferPool.Add(buffer.Length, new Queue<byte[]>());

                Array.Clear(buffer, 0, buffer.Length);
                bufferPool[buffer.Length].Enqueue(buffer);
            }
        }
    }
}