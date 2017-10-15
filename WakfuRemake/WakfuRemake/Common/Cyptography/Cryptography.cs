using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Auth;

namespace WakfuRemake.Common.Cyptography
{
    public static class Cryptography
    {
        private static RSACryptoServiceProvider crypt = null;
        private static RSAParameters privateKey;
        private static byte[] publicKey;

        public static void InitKey()
        {
            if (crypt != null)
                return;
            crypt = new RSACryptoServiceProvider(1024);

            //how to get the private key
            privateKey = crypt.ExportParameters(true);

            //and the public key ...
            var pubKey = crypt.ExportParameters(false);

            var sw = new System.IO.StringWriter();
            //we need a serializer
            var xs = new System.Xml.Serialization.XmlSerializer(typeof(RSAParameters));
            //serialize the key into the stream
            xs.Serialize(sw, pubKey);
            //get the string from the stream
            publicKey = Encoding.UTF8.GetBytes(sw.ToString());
            crypt.ImportParameters(privateKey);
        }

        public static BigEndian.BigEndianReader Decode(AuthClient client, byte[] data)
        {
            if (client.Crypted == true)
                data = crypt.Decrypt(data, false);
            return (new BigEndian.BigEndianReader(data));
        }

        public static byte[] GetPublicKey()
        {
            return (publicKey);
        }
    }
}
