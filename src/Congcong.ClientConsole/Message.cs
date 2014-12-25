using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Congcong.ClientConsole
{
    public class Message
    {
        public string id;

        public int senderId;

        public int receiverId;

        public MessageType type;

        public string content;

        public DateTime sendTime;
    }
}
