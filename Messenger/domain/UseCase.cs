using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static Messenger.CommonInteractor;

namespace Messenger
{
    interface UseCase
    {
        event UsersChanged UsersChangedEvent;
        event MessagesChanged MessagesChangedEvent;

        User CurrentUser { get; }
        bool registerUser(String name);
        void sendMessage(int receiverID, string content);
        void markAsRead(List<Message> messages);
        List<User> getUsersList();
        Chat getChatWith(int friendID);
        void ExitApp();
    }
}
