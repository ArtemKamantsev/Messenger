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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Messenger
{
    public partial class UserListWindow : Window
    {

        public static UserListWindow Instance { get; private set; } = new UserListWindow();

        private UserListWindow()
        {
            InitializeComponent();

            listener = (newUsers) => onUserListChanged(newUsers);
            listenerMessage = () => onUserListChanged(useCase.getUsersList());

            listener(useCase.getUsersList());
            useCase.UsersChangedEvent += listener;
            useCase.MessagesChangedEvent += listenerMessage;
        }

        private CommonInteractor.UsersChanged listener;
        private CommonInteractor.MessagesChanged listenerMessage;
        private UseCase useCase = CommonInteractor.Instance;

        private void onUserListChanged(List<User> users)
        {
            Application.Current.Dispatcher.Invoke(() =>
            {
                placeholder.Visibility = (users.Count > 0) ? Visibility.Hidden : Visibility.Visible;
                userList.ItemsSource = users;
            });
        }

        private void UserList_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
            if(userList.SelectedItem != null)
            {
                new ChatWindow((userList.SelectedItem as User).ID).Show();
                this.Hide();
            }
        }

        private void Window_Closed(object sender, EventArgs e)
        {
            useCase.UsersChangedEvent -= listener;
            useCase.MessagesChangedEvent -= listenerMessage;
            useCase.ExitApp();
        }

        private void TbClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            useCase.ExitApp();
        }
    }
}
