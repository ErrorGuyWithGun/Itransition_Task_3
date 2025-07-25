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
        private readonly Random random = new Random();
        public (byte[] Key, int Number, string Hmac) GenerateNumber(int max)
        {
            byte[] key = new byte[32];
            _randomNumGen.GetBytes(key);

            int number = random.Next(max+1);
            string hmac = ComputeHmacSha3(number.ToString(), key);

            return (key, number, hmac);
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
