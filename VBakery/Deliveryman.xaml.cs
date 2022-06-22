using System.Windows;

namespace VBakery
{
    public partial class Deliveryman : Window
    {
        //public Deliveryman()
        //{
        //    InitializeComponent();
        //}
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }
    }
}
