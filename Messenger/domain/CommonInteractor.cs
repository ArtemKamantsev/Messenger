using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Messenger
{
    class CommonInteractor: UseCase
    {
        public static CommonInteractor Instance { get; private set; } = new CommonInteractor();

        private CommonInteractor()
        {
            repository.registerUserListener((arg0, arg1) => UsersChangedEvent?.Invoke(getUsersList()));
            repository.registerMessagesWatcher((arg0, arg1) => MessagesChangedEvent?.Invoke());
        }


        public User CurrentUser { get; private set; }

        public delegate void UsersChanged(List<User> newUsers);
        public delegate void MessagesChanged();
        public event UsersChanged UsersChangedEvent;
        public event MessagesChanged MessagesChangedEvent;


        private IRepository repository = DataStorage.Instance;
        private NameValidator validator = NameValidator.Instance;

        public bool registerUser(String name)
        {
            bool res = validator.isValid(name);
            if (res)
            {
                List<User> users = repository.getAllUsers();
                CurrentUser = new User(name.GetHashCode()) { Name = name };
                users.Add(CurrentUser);
                //todo replace by append
                repository.updateUsersList(users);
            }

            return res;
        }

        public void sendMessage(int receiverID, string content)
        {
            List<Message> messages = repository.getAllMessages();
            int lastID = -1;
            if (messages.Count > 0)
            {
                lastID = messages.Max(message => message.ID);
            }
               
            messages.Add(new Message(lastID + 1)
            {
                SenderID = CurrentUser.ID,
                ReceiverID = receiverID,
                Date = DateTimeOffset.UtcNow.ToUnixTimeMilliseconds(),
                isRead = false,
                Content = content
            });
            repository.updateMessagesList(messages);
        }

        public void markAsRead(List<Message> messages)
        {
            List<Message> fullMessagesList = repository.getAllMessages();
            messages.ForEach(m =>
            {
                if (fullMessagesList.Any(mes => mes.ID == m.ID))
                {
                    fullMessagesList.Single(mes => mes.ID == m.ID).isRead = true;
                }
            });
            repository.updateMessagesList(fullMessagesList);
        }

        public List<User> getUsersList()
        {
            List<User> users = repository.getAllUsers();
            if (users.Contains(CurrentUser))
            {
                users.Remove(CurrentUser);
            }
            List<Message> messages = repository.getAllMessages();
            foreach(Message m in messages)
            {
                if (!m.isRead && m.ReceiverID == CurrentUser.ID)
                {
                    if (users.Any(u => u.ID == m.SenderID))
                    {
                        users.First(u => u.ID == m.SenderID).UnreadCount++;
                    }
                }
            }

            return users;
        }

        public Chat getChatWith(int friendID)
        {
            List<Message> messages = repository.getAllMessages();
            Chat res = new Chat();
            User friend = repository.getAllUsers().First(user => user.ID == friendID);
            res.FriendName = friend.Name;

            foreach (Message m in messages)
            {
                if ((m.SenderID == friendID || m.ReceiverID == friendID)
                    && (m.SenderID == CurrentUser.ID || m.ReceiverID == CurrentUser.ID))
                {
                    if(m.SenderID == CurrentUser.ID)
                    {
                        m.Alignment = HorizontalAlignment.Right;
                    }
                    res.MessageList.Add(m);
                }
            }
            res.MessageList.Sort();

            return res;
        }


        public void ExitApp()
        {
            removeCurrentUser();
            Application.Current.Shutdown();
        }

        private void removeCurrentUser()
        {
            if(CurrentUser != null)
            {
                List<User> users = repository.getAllUsers();
                users.Remove(CurrentUser);
                repository.updateUsersList(users);
            }
        }
    }
}
