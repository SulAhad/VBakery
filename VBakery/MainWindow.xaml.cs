using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Animation;

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
        public void Supervisor_MouseDown(object sender, RoutedEventArgs e)
        {
            Supervisor supervisor = new();
            supervisor.Show();
        }
        private void Supervisor_MouseEnter(object sender, MouseEventArgs e)
        {
            Supervisor.Background = Brushes.Gray;
        }

        private void Supervisor_MouseLeave(object sender, MouseEventArgs e)
        {
            Supervisor.Background = Brushes.Transparent;
        }

        private void buyer_MouseEnter(object sender, MouseEventArgs e)
        {
            buyer.Background = Brushes.Gray;
        }

        private void buyer_MouseLeave(object sender, MouseEventArgs e)
        {
            buyer.Background = Brushes.Transparent;
        }

        private void deliveryman_MouseEnter(object sender, MouseEventArgs e)
        {
            deliveryman.Background = Brushes.Gray;
        }

        private void deliveryman_MouseLeave(object sender, MouseEventArgs e)
        {
            deliveryman.Background = Brushes.Transparent;
        }

        private void kitchener_MouseEnter(object sender, MouseEventArgs e)
        {
            kitchener.Background = Brushes.Gray;
        }

        private void kitchener_MouseLeave(object sender, MouseEventArgs e)
        {
            kitchener.Background = Brushes.Transparent;
        }

        private void paymaster_MouseEnter(object sender, MouseEventArgs e)
        {
            
            paymaster.Background = Brushes.Gray;
        }

        private void paymaster_MouseLeave(object sender, MouseEventArgs e)
        {
            paymaster.Background = Brushes.Transparent;
        }
    }
}
