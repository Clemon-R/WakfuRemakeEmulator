using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Common.BigEndian;
using WakfuRemake.Common.Cryptography;
using WakfuRemake.Common.Utils;

namespace WakfuRemake.Auth.Messages.Handler
{
    public class Connection : AuthPacket
    {
        [AuthIdentifier(7)]
        public static void ReceptionVersion(BigEndianReader data, AuthClient client)
        {
            Console.Write("Client - Version du client: ");
            byte version = data.ReadByte();
            ushort revision = data.ReadUShort();
            byte change = data.ReadByte();
            string build = data.ReadString();
            Config.VERSION = new object[] { version, revision, change, build };
            Console.WriteLine($"{version}.{revision}.{change}.{build}");
        }

        [AuthIdentifier(1033)]
        public static void RecpetionNewConnection(BigEndianReader packet, AuthClient client)
        {
            Console.WriteLine("Client - Open connection");
            Sender.Connection.SendVersion(client);
            client.Crypted = true;
            Sender.Connection.SendRSAKey(client);
        }

        [AuthIdentifier(1035)]
        public static void RecpetionRefreshServeur(BigEndianReader packet, AuthClient client)
        {
            Console.WriteLine("Client - Refresh all serveur");
            Sender.Connection.SendAllServeur(client);
        }

        [AuthIdentifier(1)]
        public static void ReceptionCloseConnection(BigEndianReader packet, AuthClient client)
        {
            Console.WriteLine("Client <- Close connection");
            client.Close();
        }

        [AuthIdentifier(1026)]
        public static void ReceptionLogin(BigEndianReader packet, AuthClient client)
        {
            ulong salt = packet.ReadULong();
            string user = packet.ReadString();
            string pass = packet.ReadString();
            Console.WriteLine($"Client - Authentication User: {user} Pass: {pass} Salt: {salt}");
            if (user == "test" && pass == "test")
                Sender.Connection.SendStateConnection(client, 0, true, 0, false);
            else
                Sender.Connection.SendStateConnection(client, 2, false, 0, false);//0 OK //2 Error log 5//Ban //8Compte sous protection
        }
    }
}
