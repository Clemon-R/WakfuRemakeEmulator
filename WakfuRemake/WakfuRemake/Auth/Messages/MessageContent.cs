using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Common.BigEndian;

namespace WakfuRemake.Auth.Messages
{
    public class MessageContent
    {
        public byte[] BaseStream { get; private set; }
        public BigEndianReader Data { get; private set; }

        public MessageContent(byte[] basestream, BigEndianReader data)
        {
            this.BaseStream = basestream;
            this.Data = data;
        }
    }
}
