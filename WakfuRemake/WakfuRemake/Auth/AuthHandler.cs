using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace WakfuRemake.Auth
{
    public static class AuthHandler
    {
        private static Type[] list;
        public static void InitMessages()
        {
            list = Assembly.GetAssembly(typeof(AuthPacket)).GetTypes().Where(x => x.BaseType != null && x.BaseType.Name == "AuthPacket").ToArray();
        }
        public static Type[] GetMessages()
        {
            return (list);
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
