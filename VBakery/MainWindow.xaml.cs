using System.Windows;
using System.Windows.Input;
using System.Windows.Media;

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
            this.Close();
        }
        private void Kitchener_MouseDown(object sender, RoutedEventArgs e)
        {
            Kitchener kitchener = new();
            kitchener.Show();
            this.Close();
        }
        private void Deliveryman_MouseDown(object sender, RoutedEventArgs e)
        {
            Deliveryman deliveryman = new();
            deliveryman.Show();
            this.Close();
        }
        private void Buyer_MouseDown(object sender, RoutedEventArgs e)
        {
            BuyerTerminal buyerTerminal = new();
            buyerTerminal.Show();
            this.Close();
        }
        public void Supervisor_MouseDown(object sender, RoutedEventArgs e)
        {
            Supervisor supervisor = new();
            supervisor.Show();
            this.Close();
        }
        private void Supervisor_MouseEnter(object sender, MouseEventArgs e)
        {
            Supervisor.Background = Brushes.Gray;
        }
        private void Supervisor_MouseLeave(object sender, MouseEventArgs e)
        {
            Supervisor.Background = Brushes.Transparent;
        }
        private void Buyer_MouseEnter(object sender, MouseEventArgs e)
        {
            buyer.Background = Brushes.Gray;
        }
        private void Buyer_MouseLeave(object sender, MouseEventArgs e)
        {
            buyer.Background = Brushes.Transparent;
        }
        private void Deliveryman_MouseEnter(object sender, MouseEventArgs e)
        {
            deliveryman.Background = Brushes.Gray;
        }
        private void Deliveryman_MouseLeave(object sender, MouseEventArgs e)
        {
            deliveryman.Background = Brushes.Transparent;
        }
        private void Kitchener_MouseEnter(object sender, MouseEventArgs e)
        {
            kitchener.Background = Brushes.Gray;
        }
        private void Kitchener_MouseLeave(object sender, MouseEventArgs e)
        {
            kitchener.Background = Brushes.Transparent;
        }
        private void Paymaster_MouseEnter(object sender, MouseEventArgs e)
        {
            
            paymaster.Background = Brushes.Gray;
        }
        private void Paymaster_MouseLeave(object sender, MouseEventArgs e)
        {
            paymaster.Background = Brushes.Transparent;
        }
    }
}
