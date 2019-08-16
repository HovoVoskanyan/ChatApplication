using Chat.BusinessLogicLayer.Managers;
using Chat.BusinessLogicLayer.Models;
using Chat.PresentationLayer.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
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
using Chat.PresentationLayer.Templates;
using Chat.PresentationLayer.Utilities;
using System.Threading;

namespace Chat.PresentationLayer.Pages
{
    /// <summary>
    /// Interaction logic for Page1.xaml
    /// </summary>
    public partial class ChatArea : Page
    {
        public ObservableCollection<UserViewModel> UserList { get; set; }
        public UserViewModel SelectedUser { get; set; }
        public ChatViewModel ChatModel { get; set; }
        private readonly UserManager _userManager = new UserManager();
        private readonly ChatManager _chatManager = new ChatManager();

        public ChatArea()
        {
            UserList = new ObservableCollection<UserViewModel>();
            SelectedUser = new UserViewModel();
            ChatModel = new ChatViewModel();
            InitializeComponent();
        }

        private void ListOfUsers_OnLoaded(object sender, RoutedEventArgs e)
        {
            var users = _userManager.GetAllUsers();
            foreach (var user in users)
            {
                UserList.Add(new UserViewModel()
                {
                    Id = user.Id,
                    UserName = user.UserName
                });
            }
        }


        private void Send_OnClick(object sender, RoutedEventArgs e)
        {
            var response = _chatManager.SendMessage(new ChatModel()
            {
                Text = ChatModel.Text,
                UserIdTo = SelectedUser.Id
            });

            //bool currentUser = false;

            //for(int i = 0; i < 10; i++)
            //{
            //    currentUser = !currentUser;
            //    AddMessage($"User{i}", "Hi", !currentUser);
            //}
            MessageBox.Show($"Type:{response.Type}| Message: {response.Message}");

        }

        private void AddMessage(string userName,string message,bool temp)
        {
            StackPanel newStackPanel = new StackPanel();
            newStackPanel.Width = 400;

            TextBlock newUserNameTextBlock = new TextBlock();
            newUserNameTextBlock.Text = userName;
            newUserNameTextBlock.FontSize = 15;
            newUserNameTextBlock.Foreground = Brushes.Aqua;

            TextBlock newMessageTextBlock = new TextBlock();
            newMessageTextBlock.Text = message;
            newMessageTextBlock.TextWrapping = TextWrapping.Wrap;

            if (!temp)
            {
                newStackPanel.Margin = new Thickness(10,10,0,0);
                newStackPanel.HorizontalAlignment = HorizontalAlignment.Left;
            }
            else
            {
                newStackPanel.Margin = new Thickness(0, 10, 10, 0);
                newStackPanel.HorizontalAlignment = HorizontalAlignment.Right;
                newUserNameTextBlock.TextAlignment = TextAlignment.Right;
                newMessageTextBlock.TextAlignment = TextAlignment.Right;
            }

            newStackPanel.Children.Add(newUserNameTextBlock);
            newStackPanel.Children.Add(newMessageTextBlock);
             
            messageMainStackPanel.Children.Add(newStackPanel);
        }

        private async void ListBox_SelectionChanged(object sender, SelectionChangedEventArgs e)
        {
           
            await PeriodicTask.Run(GetMessages, TimeSpan.FromSeconds(1), TimeSpan.FromSeconds(1), CancellationToken.None);

        }
        private void GetMessages()
        {
            var messages = _chatManager.GetMessages(SelectedUser.Id);
            messageMainStackPanel.Children.Clear();
            foreach (var message in messages)
            {
                var IsCurrent = message.UserIdTo == SelectedUser.Id;
                messageMainStackPanel.Children.Add(PageTemplates.AddMessageTemplate(IsCurrent ? SessionInfo.CurrentUserInfo.UserName : SelectedUser.UserName, message.Text, IsCurrent));
            }
        }
    }
}































