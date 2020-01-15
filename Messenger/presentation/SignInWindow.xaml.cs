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
    public partial class SignInWindow : Window
    {
        public SignInWindow()
        {
            InitializeComponent();
        }

        private UseCase useCase = CommonInteractor.Instance;

        private void BtnNext_Click(object sender, RoutedEventArgs e)
        {
            if (useCase.registerUser(tbUserName.Text))
            {
                UserListWindow.Instance.Show();
                this.Close();
            }
        }

        private void TbClose_MouseDown(object sender, MouseButtonEventArgs e)
        {
            CommonInteractor.Instance.ExitApp();
        }
    }
}
