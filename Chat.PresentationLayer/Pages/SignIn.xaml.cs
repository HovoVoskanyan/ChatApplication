using Chat.BusinessLogicLayer.Enums;
using Chat.BusinessLogicLayer.Managers;
using Chat.BusinessLogicLayer.Models;
using Chat.PresentationLayer.Models;
using Chat.PresentationLayer.Utilities;
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

namespace Chat.PresentationLayer.Pages
{
    /// <summary>
    /// Interaction logic for Login.xaml
    /// </summary>
    public partial class SignIn : Page
    {
        public UserSignInModel UserSignIn { get; set; } = new UserSignInModel();
        private readonly UserManager _usermanager = new UserManager();
        public SignIn()
        {
            DataContext = this;
            InitializeComponent();
        }

        private void LogInbutton(object sender, RoutedEventArgs e)
        {
          var response=  _usermanager.Login(new UserModel()
            {
                UserName = UserSignIn.UserName,
                Password = UserSignIn.Password
             });
            if (response.Type==ResponseType.Success)
            {
                NavigationService?.Navigate(new ChatArea());

            }
             MessageBox.Show($"Type:{response.Type}| Message: {response.Message}");
        }

        private void RegButton(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new SignUp());
        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UserSignIn.Password = HashingUtilities.GetHash512(PasswordBox.Password); 
        }
    }
}
