using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Auth.Messages;
using WakfuRemake.Auth.Messages.Handler;
using WakfuRemake.Common.BigEndian;
using WakfuRemake.Common.Cryptography;
using WakfuRemake.Common.Socket;
using WakfuRemake.Common.Utils;

namespace WakfuRemake.Auth
{
    public class AuthClient
    {
        private Socket socket;
        public bool Crypted { get; set; }

        public AuthClient(Socket socket)
        {
            this.socket = socket;
            this.Crypted = false;
            Packet packet = new Packet(8192);
            Connection.SerializeIp(this);
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
                    this.Close();
                    return;
                }
                Console.WriteLine("Client <- Reception packet");
                byte[] data = packet.Bytes;
                if (read > 0)
                {
                    data = new byte[packet.Bytes.Length + read];
                    Array.Copy(packet.Bytes, 0, data, 0, packet.Bytes.Length);
                    Array.Copy(packet.Buff, 0, data, packet.Bytes.Length, read);
                }
                if (read <= 8192 && data.Length > 0)
                {
                    this.DispatchPacket(data);
                    packet = new Packet(8192);
                }
                else
                    packet.Bytes = data;
                this.socket?.BeginReceive(packet.Buff, 0, packet.Len, 0, new AsyncCallback(this.HandlerPacket), packet);
            }
            catch (Exception e)
            {
                Console.WriteLine("Client -> Closing client - Erreur : "+e.Message);
                this.Close();
            }
        }

        private void DispatchPacket(byte[] data)
        {
            BigEndianReader packet = new BigEndianReader(data);
            ushort len = packet.ReadUShort();
            byte type = packet.ReadByte();
            ushort id = packet.ReadUShort();
            MessageHeader header = new MessageHeader(id, type, len);
            if (this.Crypted)
            {
                uint size = packet.ReadUInt();//Useless size without crypted byte
                packet = CryptoManager.RSA.Decrypt(packet.ReadBytes((int)packet.BytesAvailable), false);
            }
            Message msg = new Message(header, new MessageContent(data, packet));
            Console.WriteLine($"Client <- Message ID: {id} Len: {len} Type: {type} = {data.ToHex()}");
            MethodInfo function = AuthMessage.GetMethod(id);
            if (function != null)
                function.Invoke(null, new object[] {msg, this });
            else
                Console.WriteLine("Unkknow message Id: "+id);
        }

        public void Send(ushort id, BigEndianWriter arg)
        {
            BigEndianWriter packet = new BigEndianWriter();
            ushort len;

            packet.WriteUShort((len = (ushort)(arg.Data.Length + 4)));
            packet.WriteUShort(id);

            packet.WriteBytes(arg.Data);
            Console.WriteLine($"Client -> Send ID: {id} Len: {len} = {packet.Data.ToHex()}");
            socket.Send(packet.Data);
        }

        public void Close()
        {
            Console.WriteLine("Client -> Closing client");
            this.socket?.Close();
            this.socket = null;
            Program.AuthServer.removeClient(this);

        }

        public Socket GetSocket() {
            return (this.socket);
        }
    }
}
