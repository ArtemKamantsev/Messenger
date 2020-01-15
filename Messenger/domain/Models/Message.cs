using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Messenger
{
    class Message: IComparable<Message>
    {
        public int ID { get; private set; }
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public long Date { get; set; }
        public bool isRead { get; set; } = false;
        public String Content { get; set; }
        public HorizontalAlignment Alignment { get; set; } = HorizontalAlignment.Left;

        public Message(int id)
        {
            this.ID = id;
        }

        public override bool Equals(object obj)
        {
            return obj is Message && ((Message)obj).ID == this.ID;
        }

        public override int GetHashCode()
        {
            return ID.GetHashCode();
        }

        public int CompareTo(Message message)
        {
            return (int)(Date - message.Date);
        }
    }
}
