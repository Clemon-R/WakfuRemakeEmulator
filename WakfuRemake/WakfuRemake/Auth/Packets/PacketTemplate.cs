using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Common.BigEndian;

namespace WakfuRemake.Auth.Packets
{
    public abstract class PacketTemplate
    {
        public int Id { get; private set; }
        public abstract void Decode(BigEndianReader data, AuthClient client);
        public abstract BigEndianWriter Encode();
    }
}
