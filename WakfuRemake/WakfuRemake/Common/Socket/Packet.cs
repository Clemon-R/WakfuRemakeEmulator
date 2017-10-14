using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakfuRemake.Common.Socket
{
    public class Packet
    {
        public Packet(int len)
        {
            this.Len = len;
            this.Buff = new byte[this.Len];
            this.Bytes = new byte[0];
        }

        public int Len { get; set; }
        public byte[] Buff { get; set; }
        public byte[] Bytes { get; set; }
    }
}
