using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    class Chat
    {
        public String FriendName { get; set; }
        public List<Message> MessageList { get; private set; } = new List<Message>();
    }
}
