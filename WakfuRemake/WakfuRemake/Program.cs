using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WakfuRemake.Auth;

namespace WakfuRemake
{
    class Program
    {
        public static AuthServer AuthServer { set; get; }
        public static Thread AuthServerThread { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Initialisation of the emulator...");
            AuthServerThread = new Thread(new ThreadStart((AuthServer = new AuthServer()).Start));
            AuthServer.Start();
        }
    }
}
