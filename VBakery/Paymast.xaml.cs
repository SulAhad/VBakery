using System.Windows;

namespace VBakery
{
    public partial class Paymast : Window
    {
        public Paymast()
        {
            InitializeComponent();
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            PaymasterCalculator paymaster_Calculator = new();
            paymaster_Calculator.Show();
            Close();
        }

        private void Button_Click_Products(object sender, RoutedEventArgs e)
        {
            PaymasterProducts paymasterProducts = new();
            paymasterProducts.Show();
            Close();
        }
    }
}
