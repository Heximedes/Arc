using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;

namespace Arc.Network.Crypto
{
    public class Cryptograph
    {

        private readonly SymmetricAlgorithm _cipher;

        public Cryptograph(string key)
        {
            _cipher = new AesManaged
            {
                KeySize = 256,
                Key = BuildEncryptionKey(key),
                Mode = CipherMode.ECB
            };
        }
        #region AES
        public byte[] Transform(IEnumerable<byte> input, uint pSrc)
        {
            var buffer = input.ToArray();
            var remaining = buffer.Length;
            var length = 0x5B0;
            var start = 0;

            var srcExp = new byte[sizeof(int) * 4];
            var srcBytes = BitConverter.GetBytes(pSrc);

            while (remaining > 0)
            {
                for (var i = 0; i < srcExp.Length; ++i)
                    srcExp[i] = srcBytes[i % 4];

                if (remaining < length)
                    length = remaining;

                for (var i = start; i < start + length; ++i)
                {
                    var sub = i - start;

                    if (sub % srcExp.Length == 0)
                    {
                        using (var encryptor = _cipher.CreateEncryptor())
                        {
                            var result = encryptor.TransformFinalBlock(srcExp, 0, srcExp.Length);
                            Array.Copy(result, srcExp, srcExp.Length);
                        }
                    }

                    buffer[i] ^= srcExp[sub % srcExp.Length];
                }

                start += length;
                remaining -= length;
                length = 0x5B4;
            }

            return buffer;
        }

        public static byte[] BuildEncryptionKey(string encryptionKey)
            => AdjustKeyToSize(Convert.FromBase64String(encryptionKey));

        public static byte[] AdjustKeyToSize(byte[] trimmedKey)
        {
            var key = new byte[trimmedKey.Length * 4];

            for (int i = 0; i < trimmedKey.Length; i++)
            {
                key[i * 4] = trimmedKey[i];
            }
            return key;
        }
        #endregion


    }
}
