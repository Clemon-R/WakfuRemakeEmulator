using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace WakfuRemake.Auth
{
    public class AuthServer
    {
        private Socket serverSocket;
        private AutoResetEvent allDone = new AutoResetEvent(false);

        public AuthServer()
        {
            Console.WriteLine("Initialisation of AuthServer...");
            IPEndPoint lep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 443);
            this.serverSocket = new Socket(lep.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.serverSocket.Bind(lep);
            this.serverSocket.Listen(1000);
            AuthHandler.initPackets();
        }

        public void Start()
        {
            Console.WriteLine("Waiting new connection...");
            while (true)
            {
                allDone.Reset();
                this.serverSocket.BeginAccept(new AsyncCallback(this.ConnectionHandler), null);
                allDone.WaitOne();
            }
        }

        public void Stop()
        {
            this.serverSocket.Shutdown(SocketShutdown.Both);
            this.serverSocket.Close();
        }

        private void ConnectionHandler(IAsyncResult result)
        {
            Console.WriteLine("New connection");
            AuthClient client = new AuthClient(this.serverSocket.EndAccept(result));
        }
    }
}
