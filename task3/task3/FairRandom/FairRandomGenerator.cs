using System.Security.Cryptography;

namespace task3.FairRandom
{
    internal class FairRandomGenerator
    {
        private readonly RandomNumberGenerator _randomNumGen = RandomNumberGenerator.Create();
        private readonly Random _random = new Random();
        private readonly Hmac hmac = new Hmac();
        public (byte[] Key, int Number, string Hmac) GenerateNumber(int max)
        {
            byte[] key = KeyInit();
            int number = _random.Next(max+1);
            string hmacFinal = hmac.ComputeHmacSha3(number.ToString(), key);
            return (key, number, hmacFinal);
        }
        private byte[] KeyInit()
        {
            byte[] key = new byte[32];
            _randomNumGen.GetBytes(key);
            return key;
        }
        
        public int GetFairResult(int computerNumber, int userNumber, int max)
        {
            return (computerNumber + userNumber) % (max + 1);
        }

    }
}
