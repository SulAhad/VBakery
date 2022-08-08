using Microsoft.EntityFrameworkCore;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
namespace VBakery
{
    public partial class LoginWindow : Window
    {
        public LoginWindow()
        {
            InitializeComponent();
            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandlerKeyDownEvent);
        }
        private void CheckUser()
        {
            if (Login.Text == "кассир" && Password.Password == "100")
            {
                PaymasterSales paymasterSales = new();
                Login.ToolTip = "";
                Password.ToolTip = "";
                Login.Background = Brushes.Transparent;
                paymasterSales.Show();
                this.Close();
            }
            if (Login.Text == "повар" && Password.Password == "100")
            {
                Login.ToolTip = "";
                Password.ToolTip = "";
                Login.Background = Brushes.Transparent;
                Kitchener kitchener = new();
                kitchener.Show();
                this.Close();
            }
            if (Login.Text == "доставщик" && Password.Password == "100")
            {
                Login.ToolTip = "";
                Password.ToolTip = "";
                Login.Background = Brushes.Transparent;
                Deliveryman deliveryman = new();
                deliveryman.Show();
                this.Close();
            }
            if (Login.Text == "директор" && Password.Password == "100")
            {
                Login.ToolTip = "";
                Password.ToolTip = "";
                Login.Background = Brushes.Transparent;
                Supervisor supervisor = new();
                supervisor.Show();
                this.Close();
            }
            else
            {
                Login.ToolTip = "Введите логин";
                Password.ToolTip = "Введите пароль";
                Login.Background = Brushes.LightCoral;
                Password.Background = Brushes.LightCoral;
            }
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
