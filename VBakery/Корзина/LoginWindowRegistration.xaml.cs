using System.Windows;
using System.Windows.Media;
using System.Linq;
using VBakery.DB;
using System;
using System.Windows.Threading;
using System.Windows.Input;

namespace VBakery
{
    public partial class LoginWindowRegistration : Window
    {
        public LoginWindowRegistration()
        {
            InitializeComponent();
            AddHandler(KeyDownEvent, (KeyEventHandler)HandlerKeyDownEvent);
            NameUser.Text = null;
            LastName.Text = null;
            Password.Password = null;
            SecondPassword.Password = null;
            NameUser.Text = NameUser.Text.Trim(' ');
            LastName.Text = LastName.Text.Trim(' ');
            Password.Password = Password.Password.Trim(' ');
            SecondPassword.Password = SecondPassword.Password.Trim(' ');

        }
        private void HandlerKeyDownEvent(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    RegistrationUser();
                    break;
                case Key.Escape:
                    Escape();
                    break;
                default:
                    break;
            }
        }//Клавиатура
        private void RegistrationUser()
        {
            bool flag = true;
            NameUser.Text = NameUser.Text.Trim(' ');
            LastName.Text = LastName.Text.Trim(' ');
            Password.Password = Password.Password.Trim(' ');
            SecondPassword.Password = SecondPassword.Password.Trim(' ');

            if (NameUser.Text.Length <= 2)
            {
                NameUser.ToolTip = "Не ввели имя";
                NameUser.Background = Brushes.LightCoral;
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
            if (Password.Password.Length <= 5)
            {
                Password.ToolTip = "Пароль не может быть меньше 5 символов";
                Password.Background = Brushes.LightCoral;
                TimerForEmty();
                flag = false;
            }
            if (SecondPassword.Password.Length <= 5)
            {
                SecondPassword.ToolTip = "Пароль не может быть меньше 5 символов";
                SecondPassword.Background = Brushes.LightCoral;
                TimerForEmty();
                flag = false;
            }
            if (Password.Password != SecondPassword.Password)
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
                    Name = NameUser.Text,
                    LastName = LastName.Text,
                    Password = Password.Password,
                    DateOfRegistration = DateTime.Now.ToString()
                };
                usersContext.Users.Add(usersContext1);
                usersContext.SaveChanges();
                MainWindow mainWindow = new();
                mainWindow.Show();
                mainWindow.chek_user.Content = "Приветствую" + " " + NameUser.Text + "!" + " " + "Давай начнем работу.";
                this.Close();
            }
        }
        private void Button_ClickRegistration(object sender, RoutedEventArgs e)
        {
            RegistrationUser();
        }
        public void TimerForEmty()
        {
            DispatcherTimer dispatcherTimer = new();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
            dispatcherTimer.Start();
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            NameUser.Background = new SolidColorBrush(Colors.Transparent);
            LastName.Background = new SolidColorBrush(Colors.Transparent);
            Password.Background = new SolidColorBrush(Colors.Transparent);
            SecondPassword.Background = new SolidColorBrush(Colors.Transparent);
            NameUser.ToolTip = null;
            LastName.ToolTip = null;
            Password.ToolTip = null;
            SecondPassword.ToolTip = null;
        }
        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            LabelGetBackLoginWindow.Background = Brushes.Gray;
        }
        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            LabelGetBackLoginWindow.Background = Brushes.Transparent;
        }
        public void Escape()
        {
            LoginWindow loginWindow = new();
            loginWindow.Show();
            this.Close();
        }
        private void ButtonClickBackToLoginWindow(object sender, MouseButtonEventArgs e)
        {
            Escape();
        }
    }
}
