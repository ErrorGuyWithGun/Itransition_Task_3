using System;
using System.Security.Cryptography;
using System.Text;
using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Macs;
using Org.BouncyCastle.Crypto.Parameters;

namespace task3
{
    internal class FairRandomGenerator
    {
        private readonly RandomNumberGenerator _randomNumGen = RandomNumberGenerator.Create();

        public (byte[] Key, int Number, string Hmac) GenerateNumber(int max)
        {
            byte[] key = new byte[32];
            _randomNumGen.GetBytes(key);

            int number = GenerateUniformNumber(max);
            string hmac = ComputeHmacSha3(number.ToString(), key);

            return (key, number, hmac);
        }

        private int GenerateUniformNumber(int max)
        {
            byte[] bytes = new byte[4];
            _randomNumGen.GetBytes(bytes);
            uint value = BitConverter.ToUInt32(bytes, 0);
            return (int)(value % (uint)(max + 1));
        }

        public string ComputeHmacSha3(string message, byte[] key)
        {
            var hmac = new HMac(new Sha3Digest(256));
            hmac.Init(new KeyParameter(key));
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            hmac.BlockUpdate(messageBytes, 0, messageBytes.Length);
            byte[] result = new byte[hmac.GetMacSize()];
            hmac.DoFinal(result, 0);
            return Convert.ToHexString(result).ToUpper();
        }

        public int GetFairResult(int computerNumber, int userNumber, int max)
        {
            return (computerNumber + userNumber) % (max + 1);
        }
    }
}
