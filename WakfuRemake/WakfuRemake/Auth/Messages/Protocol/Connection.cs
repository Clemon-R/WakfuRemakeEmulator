using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Common.BigEndian;
using WakfuRemake.Common.Cryptography;
using WakfuRemake.Common.Utils;

namespace WakfuRemake.Auth.Messages.Handler
{
    public class Connection : AuthPacket
    {
        [AuthIdentifier(MessageConstant.C_CRYPT_CONNECTION)]
        public static void DecodeCrypt(Message msg, AuthClient client)
        {
            Console.WriteLine("Client - Open connection");
            client.Crypted = true;
            SerializeCrypt(client);
        }

        public static void SerializeCrypt(AuthClient client)
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteULong(0x8000000000000000);
            packet.WriteBytes(CryptoManager.RsaPublicKey.ToArray());
            client.Send(MessageConstant.S_CRYPT, packet);
        }

        [AuthIdentifier(MessageConstant.C_CLOSE_CONNECTION)]
        public static void DecodeClose(Message msg, AuthClient client)
        {
            Console.WriteLine("Client <- Close connection");
            client.Close();
        }

        public static void SerializeIp(AuthClient client)
        {
            BigEndianWriter packet = new BigEndianWriter();
            string[] args = (client.GetSocket().RemoteEndPoint as IPEndPoint).Address.ToString().Split('.');
            packet.WriteByte(byte.Parse(args[0]));
            packet.WriteByte(byte.Parse(args[1]));
            packet.WriteByte(byte.Parse(args[2]));
            packet.WriteByte(byte.Parse(args[3]));
            client.Send(MessageConstant.S_IP, packet);
        }

        public static void SerializeState(AuthClient client, byte step, bool display, int countryId, bool connectedToServer)
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteByte(step); //Step
            packet.WriteBoolean(display); //Display serveur
            if (display)
            {
                packet.WriteInt(countryId); //0 FR
                packet.WriteBoolean(connectedToServer); //Connecté au serveur
                if (connectedToServer)
                {

                    BigEndianWriter server = new BigEndianWriter();
                    server.WriteInt(0); //Id account
                    server.WriteUTF("Clemon");//Pseudo
                    server.WriteInt(1); //Nombre de serveur
                    for (int i = 0; i < 1; i++)
                    {
                        server.WriteInt(1); //ID serveur
                        server.WriteInt(0);//Droit du serveur
                    }

                    packet.WriteInt(server.Data.Length);
                    packet.WriteBytes(server.Data);

                }
            }
            client.Send(MessageConstant.S_STATE_CONNECTION, packet);
        }
    }
}
