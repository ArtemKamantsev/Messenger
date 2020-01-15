using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace Messenger
{
    public partial class ChatWindow : Window
    {
        public ChatWindow(int friendID)
        {
            InitializeComponent();

            this.friendID = friendID;
            listener = () =>
            {
                Chat chat = useCase.getChatWith(friendID);
                updateChat(chat);
            };

            listener();
            useCase.MessagesChangedEvent += listener;
        }

        private int friendID;
        CommonInteractor.MessagesChanged listener;
        private UseCase useCase = CommonInteractor.Instance;

        private void updateChat(Chat chat)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                tbFriendName.Text = "Chat with user: " + chat.FriendName;
                messageList.ItemsSource = chat.MessageList;
                List<Message> unread = new List<Message>();
                foreach(Message m in chat.MessageList)
                {
                    if (!m.isRead && m.SenderID != useCase.CurrentUser.ID)
                    {
                        unread.Add(m);
                    }
                }
                useCase.markAsRead(unread);
            });
        }

        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {
            useCase.sendMessage(friendID, tbMessage.Text);
            tbMessage.Text = "";
        }

        private void Window_KeyDown(object sender, KeyEventArgs e)
        {
            if(e.Key == Key.Enter)
            {
                Image_MouseDown(sender, null);
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            useCase.MessagesChangedEvent -= listener;
            UserListWindow.Instance.Show();
        }

        private void TbClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
