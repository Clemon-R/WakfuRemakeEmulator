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
        private static Dictionary<int, EventHandler> packets = new Dictionary<int, EventHandler>();

        public static void initPackets()
        {
            if (packets.Count > 0)
                return;
            packets.Add(7, new EventHandler(Packets.Server.Version.Instance.CallBackDecode));
        }

        public static Dictionary<int, EventHandler> getPackets()
        {
            return (packets);
        }
    }
}
