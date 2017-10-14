using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Auth.Packets;
using WakfuRemake.Common.BigEndian;
using WakfuRemake.Common.Socket;

namespace WakfuRemake.Auth
{
    public class AuthClient
    {
        private Socket socket;

        public AuthClient(Socket socket)
        {
            this.socket = socket;
            Packet packet = new Packet(8192);
            this.socket.BeginReceive(packet.Buff, 0, 8191, SocketFlags.None, new AsyncCallback(this.handlerPacket), packet);
        }

        private void handlerPacket(IAsyncResult result)
        {
            Packet packet = (Packet)result.AsyncState;
            int read = this.socket.EndReceive(result);

            try
            {
                if (!this.socket.Connected){
                    Console.WriteLine("Client <- Close connection");
                    this.socket.Close();
                }
                if (read > 0)
                {
                    Console.WriteLine("Client <- Reception packet");
                    byte[] data = new byte[packet.Bytes.Length + read];
                    Array.Copy(packet.Bytes, 0, data, 0, packet.Bytes.Length);
                    Array.Copy(packet.Buff, 0, data, packet.Bytes.Length, read);
                    if (read < 8192)
                    {
                        this.dispatchPacket(new BigEndianReader(data));
                        packet = new Packet(8192);
                    }
                    else
                        packet.Bytes = data;
                    this.socket.BeginReceive(packet.Buff, 0, packet.Len, 0, new AsyncCallback(this.handlerPacket), packet);
                }
                else if (read == 0 && packet.Bytes.Length > 0)
                {
                    Console.WriteLine("Client <- Reception final packet");
                    this.dispatchPacket(new BigEndianReader(packet.Bytes));
                    packet = new Packet(8192);
                    this.socket.BeginReceive(packet.Buff, 0, packet.Len, 0, new AsyncCallback(this.handlerPacket), packet);
                }
                else
                    this.Close();
            }catch (Exception e)
            {
                Console.WriteLine("Client -> Closing client - Erreur : "+e.Message);
                this.socket.Close();
            }
        }

        private void dispatchPacket(BigEndianReader data)
        {
            ushort len = data.ReadUShort();
            byte type = data.ReadByte();
            ushort id = data.ReadUShort();
            Console.WriteLine($"Client <- Message ID: {id} Len: {len} Type: {type}");
            PacketTemplate packet = AuthHandler.getPackets().FirstOrDefault(elem => elem.Key == id).Value;
            if (packet != null)
                packet.Decode(data, this);
            else
                Console.WriteLine("Client -> Unknow ID: " + id);
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
