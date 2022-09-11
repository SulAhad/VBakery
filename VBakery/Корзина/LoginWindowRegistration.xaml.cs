using System.Windows;
using System.Windows.Media;
using System.Linq;
using VBakery.DB;
using System;
using System.Windows.Threading;

namespace VBakery
{
    public partial class LoginWindowRegistration : Window
    {
        public LoginWindowRegistration()
        {
            InitializeComponent();

            Name.Text = null;
            LastName.Text = null;
            Password.Password = null;
            SecondPassword.Password = null;
            Name.Text = Name.Text.Trim(' ');
            LastName.Text = LastName.Text.Trim(' ');
            Password.Password = Password.Password.Trim(' ');
            SecondPassword.Password = SecondPassword.Password.Trim(' ');

        }
        private void Button_ClickRegistration(object sender, RoutedEventArgs e)
        {
            bool flag = true;
            Name.Text = Name.Text.Trim(' ');
            LastName.Text = LastName.Text.Trim(' ');
            Password.Password = Password.Password.Trim(' ');
            SecondPassword.Password = SecondPassword.Password.Trim(' ');

            if (Name.Text.Length <= 2)
            {
                Name.ToolTip = "Не ввели имя";
                Name.Background = Brushes.LightCoral;
                TimerForEmty();
                flag = false;
            }
            if (LastName.Text.Length <= 2)
            {
                LastName.ToolTip = "Не ввели фамилию";
                LastName.Background = Brushes.LightCoral;
                TimerForEmty();
                flag = false;
            }
            if (Password.Password != SecondPassword.Password && Password.Password != "" && SecondPassword.Password != "")
            {
                Password.ToolTip = "Пароли не совпадают";
                Password.Background = Brushes.LightCoral;
                SecondPassword.ToolTip = "Пароли не совпадают";
                SecondPassword.Background = Brushes.LightCoral;
                TimerForEmty();
                flag = false;
            }
            if (flag)
            {
                UsersContext usersContext = new();
                User usersContext1 = new User
                {
                    Name = Name.Text,
                    LastName = LastName.Text,
                    Password = Password.Password,
                    DateOfRegistration = DateTime.Now.ToString()
                };
                usersContext.Users.Add(usersContext1);
                usersContext.SaveChanges();
                LoginWindow loginWindow = new();
                loginWindow.Show();
                this.Close();

                
            }
        }
        public void TimerForEmty()
        {
            DispatcherTimer dispatcherTimer = new();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
            dispatcherTimer.Start();
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Name.Background = new SolidColorBrush(Colors.Transparent);
            LastName.Background = new SolidColorBrush(Colors.Transparent);
            Password.Background = new SolidColorBrush(Colors.Transparent);
            Name.ToolTip = null;
            LastName.ToolTip = null;
            Password.ToolTip = null;
        }
    }
}
