using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using VBakery.DB;

namespace VBakery
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            AddHandler(KeyDownEvent, (KeyEventHandler)HandlerKeyDownEvent);
            UsersContext usersContext = new();
            int UserCount = usersContext.Users.Count();
            if (UserCount >= 1)
            {
                registrationLabel.Visibility = Visibility.Hidden;
                EnterButton.Visibility = Visibility.Visible;
            }
            else
            {
                registrationLabel.Visibility = Visibility.Visible;
                EnterButton.Visibility = Visibility.Hidden;
            }
        }
        private void CheckUser()
        {
            UsersContext usersContext = new();
            var login = usersContext.Users.Where(p => p.Name == Login.Text);
            var password = usersContext.Users.Where(p => p.Password == Password.Password);
            //var Data = usersContext.Users.Where(p => p.CheckPymaster == "Кассир");
            if (Login.Text.Length > 0 && Password.Password.Length > 0)
            {
                foreach (User userLogin in login)
                {
                    if (Login.Text == userLogin.Name)
                    {
                        foreach (User userPassword in password)
                        {
                            if (Password.Password == userPassword.Password)
                            {
                                MainWindow mainWindow = new();
                                mainWindow.Show();
                                mainWindow.chek_user.Content = "Приветствую" + " " + Login.Text + "!" + " " + "Давай начнем работу.";
                                this.Close();
                            }
                        }
                    }
                }
            }
            else
            {
                Login.Background = new SolidColorBrush(Colors.LightCoral);
                Password.Background = new SolidColorBrush(Colors.LightCoral);
                Login.ToolTip = "Не ввели логин";
                Password.ToolTip = "Не ввели пароль";
                DispatcherTimer dispatcherTimer = new();
                dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
                dispatcherTimer.Start();
            }
        }
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Login.Background = new SolidColorBrush(Colors.Transparent);
            Password.Background = new SolidColorBrush(Colors.Transparent);
            Login.ToolTip = null;
            Password.ToolTip = null;
        }
        private void HandlerKeyDownEvent(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    CheckUser();
                    break;
                case Key.Escape:
                    this.Close();
                    break;
                default:
                    break;
            }
        }//Клавиатура
        private void Enter(object sender, RoutedEventArgs e)
        {
            CheckUser();
        }
        private void MouseDownRegistration(object sender, MouseButtonEventArgs e)
        {
            LoginWindowRegistration loginWindowRegistration = new();
            loginWindowRegistration.ShowDialog();
            this.Close();
        }
        private void EnterButton_MouseEnter(object sender, MouseEventArgs e)
        {
            EnterButton.Background = Brushes.Gray;
        }
        private void EnterButton_MouseLeave(object sender, MouseEventArgs e)
        {
            EnterButton.Background = Brushes.Transparent;
        }
    }
}
