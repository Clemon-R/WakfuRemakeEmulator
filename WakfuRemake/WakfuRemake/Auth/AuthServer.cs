using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using WakfuRemake.Common.Cryptography;

namespace WakfuRemake.Auth
{
    public class AuthServer
    {
        private Socket serverSocket;
        private AutoResetEvent allDone = new AutoResetEvent(false);
        private bool running = true;
        private List<AuthClient> clients = new List<AuthClient>();

        public AuthServer()
        {
            Console.WriteLine("Initialisation of AuthServer...");
            IPEndPoint lep = new IPEndPoint(IPAddress.Parse("127.0.0.1"), 443);
            this.serverSocket = new Socket(lep.AddressFamily, SocketType.Stream, ProtocolType.Tcp);
            this.serverSocket.Bind(lep);
            this.serverSocket.Listen(1000);
            AuthMessage.InitMessages();
            CryptoManager.InitRSA();
        }

        public void Start()
        {
            Console.WriteLine("Waiting new connection...");
            while (this.running)
            {
                allDone.Reset();
                this.serverSocket.BeginAccept(new AsyncCallback(this.ConnectionHandler), null);
                allDone.WaitOne();
            }
            Program.AuthServer = null;
            Program.AuthServerThread = null;
            Console.WriteLine("AuthServer Stopped");
        }

        public void Stop()
        {
            this.running = false;
            this.allDone.Set();
            this.serverSocket.Shutdown(SocketShutdown.Both);
            this.serverSocket.Close();
        }

        private void ConnectionHandler(IAsyncResult result)
        {
            Console.WriteLine("New connection");
            AuthClient client = new AuthClient(this.serverSocket.EndAccept(result));
            clients.Add(client);
            this.allDone.Set();
        }
    }
}
