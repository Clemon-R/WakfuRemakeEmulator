using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Auth.Manager;
using WakfuRemake.Common.BigEndian;
using static WakfuRemake.Auth.Models.Server;

namespace WakfuRemake.Auth.Messages.Handler
{
    public class Server : AuthPacket
    {
        [AuthIdentifier(MessageConstant.C_LIST_SERVER)]
        public static void Decode(Message msg, AuthClient client)
        {
            Console.WriteLine("Client - Refresh all serveur");
            Serialize(client);
        }

        public static void Serialize(AuthClient client)
        {
            byte i = 0;
            BigEndianWriter packet = new BigEndianWriter();
            BigEndianWriter part2 = new BigEndianWriter();
            packet.WriteInt(World.Servers.Count);
            part2.WriteInt(World.Servers.Count);
            foreach (Models.Server server in World.Servers)
            {
                packet.WriteInt(server.ID);
                packet.WriteUTF(server.Name);
                packet.WriteInt((int)server.Community);
                packet.WriteUTF(server.Ip);
                packet.WriteInt(server.Ports.Count);
                foreach (int port in server.Ports)
                    packet.WriteInt(port);
                packet.WriteByte(i);

                part2.WriteInt(server.ID);
                BigEndianWriter version = new BigEndianWriter();
                version.WriteByte((byte)Config.VERSION[0]);
                version.WriteUShort((ushort)Config.VERSION[1]);
                version.WriteByte((byte)Config.VERSION[2]);
                version.WriteString((string)Config.VERSION[3]);
                part2.WriteInt(version.Data.Length);
                part2.WriteBytes(version.Data);
                BigEndianWriter propertys = new BigEndianWriter();
                propertys.WriteInt(server.Propertys.Count);
                foreach (KeyValuePair<PropertyID, string> prop in server.Propertys)
                {
                    propertys.WriteShort((short)prop.Key);
                    propertys.WriteUTF(prop.Value);
                }
                part2.WriteInt(propertys.Data.Length);
                part2.WriteBytes(propertys.Data);
                part2.WriteBoolean(server.Locked);
                i++;
            }
            packet.WriteBytes(part2.Data);
            client.Send(MessageConstant.S_LIST_SERVER, packet);
        }
    }
}
