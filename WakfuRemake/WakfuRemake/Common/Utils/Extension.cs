using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Auth;
using WakfuRemake.Common.Utils;

namespace WakfuRemake.Common.Utils
{
    public static class Extension
    {
        public static AuthIdentifier GetAuthIdentifier(this MethodInfo method)
        {
            Attribute result = method.GetCustomAttribute(typeof(AuthIdentifier));
            if (result is AuthIdentifier)
                return result as AuthIdentifier;
            else
                return null;
        }
        public static MethodInfo GetMessageMethod(this MethodInfo[] methods, ushort id)
        {
            return (methods.FirstOrDefault(x => x.GetCustomAttribute<AuthIdentifier>() != null && x.GetCustomAttribute<AuthIdentifier>().getId() == id));
        }
        public static string ToHex(this byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b);
            return hex.ToString();
        }
    }
}
