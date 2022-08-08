using System.Windows;
using System.Windows.Input;

namespace VBakery
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandlerKeyDownEvent);
        }
        private void HandlerKeyDownEvent(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    break;
                case Key.Escape:
                    Close();
                    break;
                default:
                    break;
            }
        }//Клавиатура
        private void Paymaster_MouseDown(object sender, RoutedEventArgs e)
        {
            PaymasterSales paymasterSales = new();
            paymasterSales.Show();
            Close();
        }
        private void Kitchener_MouseDown(object sender, RoutedEventArgs e)
        {
            Kitchener kitchener = new();
            kitchener.Show();
            Close();
        }
        private void Deliveryman_MouseDown(object sender, RoutedEventArgs e)
        {
            Deliveryman deliveryman = new();
            deliveryman.Show();
            this.Close();
        }
        private void Buyer_MouseDown(object sender, RoutedEventArgs e)
        {
            Buyer buyer = new();
            buyer.Show();
            Close();
        }
        private void Supervisor_MouseDown(object sender, RoutedEventArgs e)
        {
            Supervisor supervisor = new();
            supervisor.Show();
        }
    }
}
