using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    class User
    {
        public int ID { get; private set; }
        public String Name { get; set; }
        public int UnreadCount { get; set; }

        public User(int id)
        {
            this.ID = id;
        }

        public override bool Equals(object obj)
        {
            return obj is User && ((User)obj).ID == this.ID;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }
    }
}
