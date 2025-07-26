using Org.BouncyCastle.Crypto.Digests;
using Org.BouncyCastle.Crypto.Parameters;
using Org.BouncyCastle.Crypto.Macs;
using System.Text;

namespace task3.FairRandom
{
    internal class Hmac
    {
        public string ComputeHmacSha3(string message, byte[] key)
        {
            HMac hmac = HmacInit(key);
            HmacUpdate(message, hmac);
            return Convert.ToHexString(HmacFinal(hmac)).ToUpper();
        }
        private HMac HmacInit(byte[] key)
        {
            HMac hmac = new HMac(new Sha3Digest(256));
            hmac.Init(new KeyParameter(key));
            return hmac;
        }

        private void HmacUpdate(string message, HMac hmac)
        {
            byte[] messageBytes = Encoding.UTF8.GetBytes(message);
            hmac.BlockUpdate(messageBytes, 0, messageBytes.Length);
        }

        private byte[] HmacFinal(HMac hmac)
        {
            byte[] result = new byte[hmac.GetMacSize()];
            hmac.DoFinal(result, 0);
            return result;
        }
    }
}
