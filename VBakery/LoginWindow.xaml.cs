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

namespace VBakery
{
    /// <summary>
    /// Логика взаимодействия для LoginWindow.xaml
    /// </summary>
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
                Login.ToolTip = "";
                Password.ToolTip = "";
                Login.Background = Brushes.Transparent;
                Paymast paymaster = new();
                paymaster.Show();
                Close();
            }
            if (Login.Text == "повар" && Password.Password == "100")
            {
                Login.ToolTip = "";
                Password.ToolTip = "";
                Login.Background = Brushes.Transparent;
                Kitchener kitchener = new();
                kitchener.Show();
                Close();
            }
            if (Login.Text == "доставщик" && Password.Password == "100")
            {
                Login.ToolTip = "";
                Password.ToolTip = "";
                Login.Background = Brushes.Transparent;
                Deliveryman deliveryman = new();
                deliveryman.Show();
                Close();
            }
            if (Login.Text == "директор" && Password.Password == "100")
            {
                Login.ToolTip = "";
                Password.ToolTip = "";
                Login.Background = Brushes.Transparent;
                Supervisor supervisor = new();
                supervisor.Show();
                Close();
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
    }
}
