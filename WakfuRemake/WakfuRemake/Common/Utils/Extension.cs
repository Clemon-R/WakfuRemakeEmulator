using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using WakfuRemake.Auth;
using WakfuRemake.Auth.Messages;
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
        public static Dictionary<ushort, MethodInfo> GetMessages(this Type[] list)
        {
            Dictionary<ushort, MethodInfo> result = new Dictionary<ushort, MethodInfo>();
            foreach (Type value in list)
            {
                MethodInfo[] args = value.GetMethods();
                if (args == null)
                    continue;
                foreach (MethodInfo function in args)
                {
                    AuthIdentifier id = function.GetAuthIdentifier();
                    if (id == null 
                        || function.GetParameters().Length != 2 
                        || function.GetParameters()[0].ParameterType != typeof(Message) 
                        || function.GetParameters()[1].ParameterType != typeof(AuthClient))
                        continue;
                    result.Add(id.getId(), function);
                }
            }
            return (result);
        }
        public static string ToHex(this byte[] ba)
        {
            StringBuilder hex = new StringBuilder(ba.Length * 2);
            foreach (byte b in ba)
                hex.AppendFormat("{0:x2}", b).Append(".");
            return hex.ToString();
        }
    }
}
