using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Common.BigEndian;

namespace WakfuRemake.Auth.Packets.Server
{
    public class Version : PacketTemplate
    {
        public static Version Instance = new Version(7);
        private byte version;
        private ushort revision;
        private byte change;
        private string build;
        public Version(ushort id) : base(id)
        {
        }
        public override void Decode(BigEndianReader data, AuthClient client)
        {
            Console.Write("Client <- Version du client: ");
            version = data.ReadByte();
            revision = data.ReadUShort();
            change = data.ReadByte();
            build = data.ReadString();
            Console.WriteLine($"{version}.{revision}.{change}.{build}");
            Console.WriteLine("Client -> Send version");
            client.getSocket().Send(this.Encode().Data);
        }

        public override BigEndianWriter Encode() {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteByte(0);
            packet.WriteUShort(this.Id);
            packet.WriteByte(this.version);
            packet.WriteUShort(this.revision);
            packet.WriteByte(this.change);
            packet.WriteString(this.build);
            packet.Seek(0, System.IO.SeekOrigin.Begin);
            packet.WriteUShort((ushort)packet.Data.Length);
            return (packet);
        }
    }
}
