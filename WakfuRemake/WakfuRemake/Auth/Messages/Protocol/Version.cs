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
        [AuthIdentifier(MessageConstant.C_VERSION)]
        public static void Decode(Message msg, AuthClient client)
        {
            BigEndianReader data = msg.Content.Data;
            Console.Write("Client - Version du client: ");
            byte version = data.ReadByte();
            ushort revision = data.ReadUShort();
            byte change = data.ReadByte();
            string build = data.ReadString();
            Config.VERSION = new object[] { version, revision, change, build };
            Console.WriteLine($"{version}.{revision}.{change}.{build}");
            Serialize(client);
        }

        public static void Serialize(AuthClient client)
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteByte(1);
            packet.WriteByte((byte)Config.VERSION[0]);
            packet.WriteUShort((ushort)Config.VERSION[1]);
            packet.WriteByte((byte)Config.VERSION[2]);
            packet.WriteString((string)Config.VERSION[3]);
            client.Send(MessageConstant.S_VERSION, packet);
        }
    }
}
