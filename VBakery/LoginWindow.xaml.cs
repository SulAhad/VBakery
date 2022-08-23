using System.Linq;
using System.Windows;
using System.Windows.Input;
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
                                this.Close();
                            }
                        }
                    }
                }
            }
            else
            {
                MessageBox.Show("Не введены Лоин или пароль");
            }
            
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
                case Key.Escape:
                    Close();
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
    }
}
