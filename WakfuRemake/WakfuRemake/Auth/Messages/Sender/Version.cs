using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Common.BigEndian;

namespace WakfuRemake.Auth.Messages.Sender
{
    public class Version
    {
        public static BigEndianWriter SendVersion(AuthClient client, byte version, ushort revision, byte change, string build)
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteByte(0);
            packet.WriteUShort(7);
            packet.WriteByte(version);
            packet.WriteUShort(revision);
            packet.WriteByte(change);
            packet.WriteString(build);
            packet.Seek(0, System.IO.SeekOrigin.Begin);
            packet.WriteUShort((ushort)packet.Data.Length);
            return (packet);
        }
    }
}
