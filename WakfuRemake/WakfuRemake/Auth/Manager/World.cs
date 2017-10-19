using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Auth.Models;

namespace WakfuRemake.Auth.Manager
{
    public static class World
    {
        public static List<Server> Servers = new List<Server>();

        public static void InitWorld()
        {
            //Temporaire
            Server server = new Server(1, "Owny", Server.CommunityID.FRENCH, "127.0.0.1", 444);
            Servers.Add(server);
        }
    }
}
