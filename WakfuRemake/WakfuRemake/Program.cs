using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WakfuRemake
{
    class Program
    {
        public static Thread AuthThread { get; set; }

        static void Main(string[] args)
        {
            Console.WriteLine("Main: Initialisation...");
            Console.Read();
        }
    }
}
