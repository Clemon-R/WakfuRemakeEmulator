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
        public ushort Id { get; private set; }
        public PacketTemplate(ushort id)
        {
            this.Id = id;
        }
        public void CallBackDecode(object sender, EventArgs args)
        {
            object[] obj = sender as object[];
            this.Decode(obj[0] as BigEndianReader, obj[1] as AuthClient);
        }
        public abstract void Decode(BigEndianReader data, AuthClient client);
        public abstract BigEndianWriter Encode();
    }
}
