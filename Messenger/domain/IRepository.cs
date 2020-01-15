using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Messenger
{
    interface IRepository
    {
        void updateMessagesList(List<Message> messages);
        void updateUsersList(List<User> users);
        List<User> getAllUsers();
        List<Message> getAllMessages();
        void registerUserListener(FileSystemEventHandler handler);
        void registerMessagesWatcher(FileSystemEventHandler handler);
    }
}
