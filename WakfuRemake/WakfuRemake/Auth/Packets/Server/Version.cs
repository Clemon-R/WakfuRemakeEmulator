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
        public override void Decode(BigEndianReader data, AuthClient client)
        {
            Console.Write("Client <- Version du client: ");
            Console.Write(data.ReadByte());
            Console.Write("."+data.ReadShort());
            Console.Write("."+data.ReadByte());
            Console.Write("."+data.ReadString());
            Console.Write("\n");
        }

        public override BigEndianWriter Encode()
        {
            throw new NotImplementedException();
        }
    }
}
