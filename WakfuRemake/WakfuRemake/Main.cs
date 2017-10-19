using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WakfuRemake.Auth;
using WakfuRemake.Auth.Manager;

namespace WakfuRemake
{
    class Program
    {
        public static AuthServer AuthServer { set; get; }
        public static Thread AuthServerThread { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Initialisation of the emulator...");
            Console.WriteLine("Initialisation of the world...");
            World.InitWorld();
            Console.WriteLine("Initialisiation of authserver...");
            AuthServerThread = new Thread(new ThreadStart((AuthServer = new AuthServer()).Start));
            AuthServerThread.Start();
            Console.WriteLine("End of the initilisation of the world !");
        }
    }
}
