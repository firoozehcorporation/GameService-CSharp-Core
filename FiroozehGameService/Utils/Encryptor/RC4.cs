using System;

namespace FiroozehGameService.Utils.Encryptor
{
    internal static class Rc4
    {
        internal static byte[] Apply(byte[] data, byte[] key)
        {
            var s = new int[256];
            for (var _ = 0; _ < 256; _++) s[_] = _;

            var T = new int[256];

            if (key.Length == 256)
                Buffer.BlockCopy(key, 0, T, 0, key.Length);
            else
                for (var _ = 0; _ < 256; _++)
                    T[_] = key[_ % key.Length];

            int i;
            var j = 0;
            for (i = 0; i < 256; i++)
            {
                j = (j + s[i] + T[i]) % 256;
                var temp = s[i];
                s[i] = s[j];
                s[j] = temp;
            }

            i = j = 0;
            var result = new byte[data.Length];
            for (var iteration = 0; iteration < data.Length; iteration++)
            {
                i = (i + 1) % 256;
                j = (j + s[i]) % 256;

                var temp = s[i];
                s[i] = s[j];
                s[j] = temp;

                var k = s[(s[i] + s[j]) % 256];

                result[iteration] = Convert.ToByte(data[iteration] ^ k);
            }

            return result;
        }
    }
}