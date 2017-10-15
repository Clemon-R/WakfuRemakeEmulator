using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Auth.Packets;
using WakfuRemake.Common.BigEndian;
using WakfuRemake.Common.Socket;
using WakfuRemake.Common.Utils;

namespace WakfuRemake.Auth
{
    public class AuthClient
    {
        private Socket socket;

        public AuthClient(Socket socket)
        {
            this.socket = socket;
            Packet packet = new Packet(8192);
            this.socket.BeginReceive(packet.Buff, 0, 8191, SocketFlags.None, new AsyncCallback(this.HandlerPacket), packet);
        }

        private void HandlerPacket(IAsyncResult result)
        {
            Packet packet = (Packet)result.AsyncState;
            int read = this.socket.EndReceive(result);

            try
            {
                if (!this.socket.Connected){
                    Console.WriteLine("Client <- Close connection");
                    this.socket.Close();
                }
                Console.WriteLine("Client <- Reception packet");
                byte[] data = packet.Bytes;
                if (read > 0)
                {
                    data = new byte[packet.Bytes.Length + read];
                    Array.Copy(packet.Bytes, 0, data, 0, packet.Bytes.Length);
                    Array.Copy(packet.Buff, 0, data, packet.Bytes.Length, read);
                }
                if (read < 8192 && data.Length > 0)
                {
                    this.DispatchPacket(new BigEndianReader(data));
                    packet = new Packet(8192);
                }
                else
                    packet.Bytes = data;
                this.socket.BeginReceive(packet.Buff, 0, packet.Len, 0, new AsyncCallback(this.HandlerPacket), packet);
            }
            catch (Exception e)
            {
                Console.WriteLine("Client -> Closing client - Erreur : "+e.Message);
                this.socket.Close();
            }
        }

        private void DispatchPacket(BigEndianReader data)
        {
            ushort len = data.ReadUShort();
            byte type = data.ReadByte();
            ushort id = data.ReadUShort();
            Console.WriteLine($"Client <- Message ID: {id} Len: {len} Type: {type}");
            MethodInfo function = null;
            AuthHandler.GetMessages().FirstOrDefault(x => (function = x.GetMethods().GetMessageMethod(id)) != null);
            if (function != null)
            {
                function.Invoke(null, new object[] {data, this });
            }
            else
                Console.WriteLine("Unkknow message Id: "+id);
        }

        public void Close()
        {
            Console.WriteLine("Client -> Closing client");
            this.socket.Close();
        }

        public Socket getSocket() {
            return (this.socket);
        }
    }
}
