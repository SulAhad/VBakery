using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;
using System.Windows.Threading;
using VBakery.DB;

namespace VBakery
{
    public partial class LoginWindow : Window
    {
        readonly UsersContext usersContext = new();
        readonly MainWindow mainWindow = new();
        public LoginWindow()
        {
            InitializeComponent();
            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandlerKeyDownEvent);
            System.Int32 UserCount = usersContext.Users.Count();
            
            if (UserCount > 0)
            {
                registrationLabel.Visibility = Visibility.Hidden;
            }
            else
            {
                registrationLabel.Visibility = Visibility.Visible;
            }
        }
        private void CheckUser()
        {
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
                                //CheckPaymaster();
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
                dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
                dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
                dispatcherTimer.Start();
            }
            
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            Login.Background = new SolidColorBrush(Colors.Transparent);
            Password.Background = new SolidColorBrush(Colors.Transparent);
            Login.ToolTip = null;
            Password.ToolTip = null;
        }
        public void CheckPaymaster()
        {
            mainWindow.paymaster.Visibility = Visibility.Visible;
        }
        public void CheckKithener()
        {
            mainWindow.kitchener.Visibility = Visibility.Visible;
        }
        public void CheckDeliveryman()
        {
            mainWindow.deliveryman.Visibility = Visibility.Visible;
        }
        public void CheckBuyer()
        {
            mainWindow.buyer.Visibility = Visibility.Visible;
        }
        public void CheckSupervisor()
        {
            mainWindow.Supervisor.Visibility = Visibility.Visible;
        }
        private void HandlerKeyDownEvent(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    CheckUser();
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
            loginWindowRegistration.Show();
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
