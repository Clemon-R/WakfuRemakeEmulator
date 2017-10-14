using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Auth.Packets;

namespace WakfuRemake.Auth
{
    public static class AuthHandler
    {
        private static Dictionary<int, PacketTemplate> packets = new Dictionary<int, PacketTemplate>();

        public static void initPackets()
        {
            if (packets.Count > 0)
                return;
            packets.Add(7, new Packets.Server.Version());
        }

        public static Dictionary<int, PacketTemplate> getPackets()
        {
            return (packets);
        }
    }
}
