using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Common.Utils;

namespace WakfuRemake.Auth
{
    public static class AuthMessage
    {
        private static Dictionary<ushort, MethodInfo> listMessages;
        public static void InitMessages()
        {
            listMessages = Assembly.GetAssembly(typeof(AuthPacket)).GetTypes().Where(x => x.BaseType != null && x.BaseType.Name == "AuthPacket").ToArray().GetMessages();
        }
        public static MethodInfo GetMethod(ushort id)
        {
            return (listMessages.ContainsKey(id) ? listMessages[id] : null);
        }
    }
    public class AuthIdentifier : Attribute
    {
        private ushort id;

        public AuthIdentifier(ushort id)
        {
            this.id = id;
        }

        public ushort getId()
        {
            return (this.id);
        }
    }
    public class AuthPacket
    {
    }
}
