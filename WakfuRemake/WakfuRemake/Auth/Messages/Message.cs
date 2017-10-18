using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WakfuRemake.Auth.Messages
{
    public class Message
    {
        public MessageHeader Header { get; private set; }
        public MessageContent Content { get; private set; }

        public Message(MessageHeader header, MessageContent content)
        {
            this.Header = header;
            this.Content = content;
        }
    }
}
