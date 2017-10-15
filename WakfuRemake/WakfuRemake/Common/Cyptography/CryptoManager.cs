using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace WakfuRemake.Common.Cryptography
{ 
    public static class CryptoManager
    {
        public static RSA.RSAProvider RSA { get; set; }
        public static List<byte> RsaPublicKey { get; set; }

        public static void InitRSA()
        {
            RSA = new RSA.RSAProvider(1024);
            RsaPublicKey = RSA.ExportToX509().ToList();
        }
    }
}
