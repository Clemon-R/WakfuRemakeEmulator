using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Common.BigEndian;
using WakfuRemake.Common.Cyptography;

namespace WakfuRemake.Auth.Messages.Handler
{
    public class Connection : AuthPacket
    {
        [AuthIdentifier(1033)]
        public static void RecpetionNewConnection(BigEndianReader packet, AuthClient client)
        {
            Console.WriteLine("Client <- Open connection");
            Sender.Version.SendVersion(client);
            client.Crypted = true;
            Cryptography.InitKey();
            Sender.Version.SendRSAKey(client);
        }

        [AuthIdentifier(1)]
        public static void ReceptionCloseConnection(BigEndianReader packet, AuthClient client)
        {
            Console.WriteLine("Client <- Close connection");
            client.Close();
        }
    }
}
