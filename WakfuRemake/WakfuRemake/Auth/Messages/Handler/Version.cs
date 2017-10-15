using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Common.BigEndian;

namespace WakfuRemake.Auth.Messages.Handler
{
    public class Version : AuthPacket
    {
        [AuthIdentifier(7)]
        public static void ReceptionVersion(BigEndianReader data, AuthClient client)
        {
            Console.Write("Client <- Version du client: ");
            byte version = data.ReadByte();
            ushort revision = data.ReadUShort();
            byte change = data.ReadByte();
            string build = data.ReadString();
            Console.WriteLine($"{version}.{revision}.{change}.{build}");
        }
    }
}
