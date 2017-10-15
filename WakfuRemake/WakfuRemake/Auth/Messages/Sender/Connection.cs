using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Common.BigEndian;
using WakfuRemake.Common.Cryptography;
using WakfuRemake.Common.Utils;

namespace WakfuRemake.Auth.Messages.Sender
{
    public class Connection
    {
        public static void SendVersion(AuthClient client)
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteByte((byte)Config.VERSION[0]);
            packet.WriteUShort((ushort)Config.VERSION[1]);
            packet.WriteByte((byte)Config.VERSION[2]);
            packet.WriteString((string)Config.VERSION[3]);
            SendPacket(client, packet, 8);
        }

        public static void SendIpClient(AuthClient client)
        {
            BigEndianWriter packet = new BigEndianWriter();
            string[] args = (client.GetSocket().RemoteEndPoint as IPEndPoint).Address.ToString().Split('.');
            packet.WriteByte(byte.Parse(args[0]));
            packet.WriteByte(byte.Parse(args[1]));
            packet.WriteByte(byte.Parse(args[2]));
            packet.WriteByte(byte.Parse(args[3]));
            SendPacket(client, packet, 110);
        }

        public static void SendRSAKey(AuthClient client)
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteULong(0x8000000000000000);
            packet.WriteBytes(CryptoManager.RsaPublicKey.ToArray());
            SendPacket(client, packet, 1034);
        }

        public static void SendPacket(AuthClient client, BigEndianWriter arg, ushort id)
        {
            BigEndianWriter packet = new BigEndianWriter();
            ushort len;
            
            packet.WriteUShort(len = (ushort)(arg.Data.Length + 4));
            packet.WriteUShort(id);

            packet.WriteBytes(arg.Data);
            Console.WriteLine($"Client -> Send ID: {id} Len: {len} = {packet.Data.ToHex()}");
            client.GetSocket().Send(packet.Data);
        }

        public static void SendAllServeur(AuthClient client)
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteInt(1);//Nombre de proxy
            for (int i = 0;i < 1; i++)
            {
                packet.WriteInt(1);//ID proxy
                packet.WriteString("Dathura");//Nom proxy en generale serveur
                packet.WriteInt(0); //Communauté
                packet.WriteString("127.0.0.1");
                packet.WriteInt(1);//Nombre de port
                for (int p = 0;p < 1; p++)
                {
                    packet.WriteInt(444);
                }
                packet.WriteByte(1);//Position dans la liste
            }
            packet.WriteInt(1);//Nombre de serveur
            for (int i = 0;i < 1; i++)
            {
                packet.WriteInt(1);//Id Serveur

                BigEndianWriter vers = new BigEndianWriter();
                vers.WriteByte((byte)Config.VERSION[0]);
                vers.WriteUShort((ushort)Config.VERSION[1]);
                vers.WriteByte((byte)Config.VERSION[2]);
                vers.WriteString((string)Config.VERSION[3]);
                //Concat prop with packet
                packet.WriteInt(vers.Data.Length);
                packet.WriteBytes(vers.Data);

                BigEndianWriter prop = new BigEndianWriter();
                prop.WriteInt(1); //Nombre de propriété
                //prop.WriteShort(20);//Id Propriété 
                //prop.WriteString("false");//Valeur
                //prop.WriteShort(208);//Id Propriété 
                //prop.WriteString("true");//Valeur
                //prop.WriteShort(209);//Id Propriété 
                //prop.WriteString("0");//Valeur
                //prop.WriteShort(210);//Id Propriété 
                //prop.WriteString("");//Valeur
                prop.WriteShort(420);//Serveur id
                prop.WriteString("1");//Valeur
                //Concat prop with packet
                packet.WriteInt(prop.Data.Length);
                packet.WriteBytes(prop.Data);

                packet.WriteBoolean(false);//Vérrouiller
            }
            SendPacket(client, packet, 1036);
        }

        public static void SendStateConnection(AuthClient client, byte step, bool display, int countryId, bool connectedToServer)
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
                    packet.WriteInt(0); //Id account
                    packet.WriteString("Clemon");//Pseudo
                    packet.WriteInt(1); //Nombre de serveur
                    for (int i = 0; i < 1; i++)
                    {
                        packet.WriteInt(1); //ID serveur
                        packet.WriteInt(0);//Droit du serveur
                    }
                }
                packet.WriteBytes(new byte[] { });//Inconnu
            }
            SendPacket(client, packet, 1027);
        }
    }
}
