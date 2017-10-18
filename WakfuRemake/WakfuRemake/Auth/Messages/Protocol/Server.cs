using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Common.BigEndian;

namespace WakfuRemake.Auth.Messages.Handler
{
    public class Server : AuthPacket
    {
        [AuthIdentifier(MessageConstant.C_LIST_SERVER)]
        public static void Decode(Message msg, AuthClient client)
        {
            Console.WriteLine("Client - Refresh all serveur");
            Serialize(client);
        }

        public static void Serialize(AuthClient client)
        {
            BigEndianWriter packet = new BigEndianWriter();
            packet.WriteInt(1);
            packet.WriteInt(1);
            packet.WriteUTF("Dathura");
            packet.WriteInt(0);
            packet.WriteUTF("127.0.0.1");
            packet.WriteInt(1);
            packet.WriteInt(444);
            packet.WriteByte(0);

            packet.WriteInt(1);
            packet.WriteInt(1);
            BigEndianWriter vers = new BigEndianWriter();
            vers.WriteByte((byte)Config.VERSION[0]);
            vers.WriteUShort((ushort)Config.VERSION[1]);
            vers.WriteByte((byte)Config.VERSION[2]);
            vers.WriteString((string)Config.VERSION[3]);
            //Concat prop with packet
            packet.WriteInt(vers.Data.Length);
            packet.WriteBytes(vers.Data);
            packet.WriteInt(0);
            //packet.WriteShort(420);
            //packet.WriteString("1");
            packet.WriteBoolean(false);
            /*packet.WriteInt(1);//Nombre de proxy
            for (int i = 0;i < 1; i++)
            {
                packet.WriteInt(2);//ID proxy
                packet.WriteString("Dathura", 4);//Nom proxy en generale serveur
                packet.WriteInt(0); //Communauté
                packet.WriteString("127.0.0.1", 4);
                packet.WriteInt(1);//Nombre de port
                for (int p = 0;p < 1; p++)
                {
                    packet.WriteInt(444);
                }
                packet.WriteByte(0);//Position dans la liste
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
                prop.WriteInt(0); //Nombre de propriété
                //prop.WriteShort(20);//Id Propriété 
                //prop.WriteString("false");//Valeur
                //prop.WriteShort(208);//Id Propriété 
                //prop.WriteString("true");//Valeur
                //prop.WriteShort(209);//Id Propriété 
                //prop.WriteString("0");//Valeur
                //prop.WriteShort(210);//Id Propriété 
                //prop.WriteString("");//Valeur
                //prop.WriteShort(420);//Serveur id
                //prop.WriteString("1", 4);//Valeur
                //Concat prop with packet
                packet.WriteInt(prop.Data.Length);
                packet.WriteBytes(prop.Data);

                packet.WriteBoolean(false);//Vérrouiller
            }*/
            client.Send(MessageConstant.S_LIST_SERVER, packet);
        }
    }
}
