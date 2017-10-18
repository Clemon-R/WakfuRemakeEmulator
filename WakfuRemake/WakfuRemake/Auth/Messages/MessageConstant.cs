using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakfuRemake.Auth.Messages
{
    public static class MessageConstant
    {
        //Client
        public const ushort C_VERSION           = 7;
        public const ushort C_LIST_SERVER       = 1035;
        public const ushort C_AUTH_IDENTICATION = 1026;
        public const ushort C_CLOSE_CONNECTION  = 1;
        public const ushort C_CRYPT_CONNECTION  = 1033;

        //Server
        public const ushort S_VERSION           = 8;
        public const ushort S_IP                = 110;
        public const ushort S_CRYPT             = 1034;
        public const ushort S_STATE_CONNECTION  = 1027;
        public const ushort S_LIST_SERVER       = 1036;
    }
}
