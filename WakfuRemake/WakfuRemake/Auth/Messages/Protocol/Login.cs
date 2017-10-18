using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Common.BigEndian;

namespace WakfuRemake.Auth.Messages.Handler
{
    public class Login : AuthPacket
    {
        [AuthIdentifier(MessageConstant.C_AUTH_IDENTICATION)]
        public static void Decode(Message msg, AuthClient client)
        {
            BigEndianReader packet = msg.Content.Data;
            ulong salt = packet.ReadULong();
            string user = packet.ReadString();
            string pass = packet.ReadString();
            Console.WriteLine($"Client - Authentication User: {user} Pass: {pass} Salt: ");
            if (user == "test" && pass == "test")
                Connection.SerializeState(client, 0, true, 0, false);
            else
                Connection.SerializeState(client, 2, false, 0, false);//0 OK //2 Error log 5//Ban //8Compte sous protection
        }
    }
}
