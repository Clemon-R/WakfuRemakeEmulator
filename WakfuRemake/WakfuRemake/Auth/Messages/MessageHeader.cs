using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakfuRemake.Auth.Messages
{
    public class MessageHeader
    {
        public ushort ID { get; private set; }
        public byte Type { get; private set; }
        public ushort Len { get; private set; }

        public MessageHeader(ushort id, byte type, ushort len)
        {
            this.ID = id;
            this.Type = type;
            this.Len = len;
        }
    }
}
