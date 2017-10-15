using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Common.BigEndian;

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

        [AuthIdentifier(1)]
        public static void ReceptionCloseConnection(BigEndianReader packet, AuthClient client)
        {
            Console.WriteLine("Client <- Close connection");
            client.Close();
        }
    }
}
