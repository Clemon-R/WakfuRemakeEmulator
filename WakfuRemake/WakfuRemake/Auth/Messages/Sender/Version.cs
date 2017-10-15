using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Common.BigEndian;
using WakfuRemake.Common.Cyptography;

namespace WakfuRemake.Auth.Messages.Sender
{
    public class Version
    {
        public static void SendVersion(AuthClient client)
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteByte((byte)Config.VERSION[0]);
            packet.WriteUShort((ushort)Config.VERSION[1]);
            packet.WriteByte((byte)Config.VERSION[2]);
            packet.WriteString((string)Config.VERSION[3]);
            SendPacket(client, packet, 8);
        }

        public static void SendIpClient(AuthClient client)
        {
            BigEndianWriter packet = new BigEndianWriter();
            string[] args = (client.GetSocket().RemoteEndPoint as IPEndPoint).Address.ToString().Split('.');
            packet.WriteByte(byte.Parse(args[0]));
            packet.WriteByte(byte.Parse(args[1]));
            packet.WriteByte(byte.Parse(args[2]));
            packet.WriteByte(byte.Parse(args[3]));
            SendPacket(client, packet, 110);
        }

        public static void SendRSAKey(AuthClient client)
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteULong(0x8000000000000000);
            packet.WriteBytes(Cryptography.GetPublicKey());
            SendPacket(client, packet, 1034);
        }

        public static void SendPacket(AuthClient client, BigEndianWriter arg, ushort id)
        {
            BigEndianWriter packet = new BigEndianWriter();
            ushort len;

            //packet.WriteByte(0);
            packet.WriteUShort(id);

            packet.WriteBytes(arg.Data);

            packet.Seek(0, System.IO.SeekOrigin.Begin);
            packet.WriteUShort(len = (ushort)packet.Data.Length);
            Console.WriteLine($"Client -> Send ID: {id} Len: {len}");
            client.GetSocket().Send(packet.Data);
        }
    }
}
