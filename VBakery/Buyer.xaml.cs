using System.Linq;
using System.Windows;
using System.Windows.Input;
namespace VBakery
{
    public partial class Buyer : Window
    {
        public Buyer()
        {
            InitializeComponent();
            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandlerKeyDownEvent);

            using MenuArea1Context Menudb1 = new();//Покупатель
            Menu1.ItemsSource = Menudb1.Menu1.ToList();

            using MenuArea2Context Menudb2 = new();//Покупатель
            Menu2.ItemsSource = Menudb2.Menu2.ToList();

            using MenuArea3Context Menudb3 = new();//Покупатель
            Menu3.ItemsSource = Menudb3.Menu3.ToList();

            using MenuArea4Context Menudb4 = new();//Покупатель
            Menu4.ItemsSource = Menudb4.Menu4.ToList();
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
                case Key.Tab:
                    MainWindow mainWindow = new();
                    mainWindow.Show();
                    this.Close();
                    break;
                default:
                    break;
            }
        }//Клавиатура

        private void MenuImage(object sender, MouseButtonEventArgs e)
        {
            BuyerToOrder buyerOrder = new();
            buyerOrder.Show();
            this.Close();
        }
    }
}
