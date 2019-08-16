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


namespace Chat.PresentationLayer.Pages
{
    public partial class SignUp : Page
    {
        private readonly UserManager _userManager = new UserManager();
        public UserSignUpModel UserSignUp { get; set; } = new UserSignUpModel();

        public SignUp()
        {
            InitializeComponent();
        }

        private void Login(object sender, RoutedEventArgs e)
        {
            NavigationService?.Navigate(new SignIn());
        }

        private void SignUpClick(object sender, RoutedEventArgs e)
        {
            var response =_userManager.Registrate(new UserRegistrationModel()
            {
                UserName=UserSignUp.UserName,
                Password = UserSignUp.Password,
                ConfirmPassword=UserSignUp.ConfirmPassword
            });

            if (response.Type==ResponseType.Success)
            {
                MessageBox.Show(response.Message);
                NavigationService?.Navigate(new SignIn());
            }
             MessageBox.Show($"Type:{response.Type}| Message: {response.Message}");

        }

        private void PasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UserSignUp.Password =HashingUtilities.GetHash512(PasswordBox.Password);
        }

        private void ConfPasswordBox_LostFocus(object sender, RoutedEventArgs e)
        {
            UserSignUp.ConfirmPassword = HashingUtilities.GetHash512(ConfPasswordBox.Password);

        }
    }
}
