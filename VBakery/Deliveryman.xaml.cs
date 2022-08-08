using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
namespace VBakery
{
    public partial class Deliveryman : Window
    {
        public Deliveryman()
        {
            InitializeComponent();
            Update();
        }
        public void Update()
        {
            using OrderForBuyersContext db = new();
            UserList.ItemsSource = db.OrderForBuyers.ToList();
            db.SaveChanges();
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
        private void MouseDownRefresh(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            using OrderForBuyersContext db = new();
            UserList.ItemsSource = db.OrderForBuyers.ToArray();
            DownTrayKitchenerOrders.Content = "Обновлено --" + System.DateTime.Now.ToString();
        }
        private void MouseDownGotoHome(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
            "Вы точно хотите выйти?",
            "Сообщение",
            MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new();
                mainWindow.Show();
                this.Close();
            }
        }
        private void OpenRecepts(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            Recepts recepts = new();
            recepts.Show();
        }
        private void OpenChat(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            ChatRoom chatRoom = new();
            chatRoom.Show();
            chatRoom.chatUser.Content = "Доставщик";
        }
        private void ButtonClickPlus(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }
        private void ButtonClickMinus(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ScrollBar scrollBar = new();
            scrollBar.Value = numeric.Value;
            findId.Text = Convert.ToString(numeric.Value);
        }
        private void ButtonDelete(object sender, RoutedEventArgs e)
        {
            using OrderForBuyersContext db = new OrderForBuyersContext();

            if (findId.Text == "")
            {
                DownTrayKitchenerOrders.Content = "Не ввели номер!";
                DownTrayKitchenerOrders.Background = Brushes.LightCoral;
            }
            else
            {
                findId.Text = findId.Text.Trim();

                int key = Convert.ToInt32(findId.Text.Trim());

                var item = db.OrderForBuyers.Find(key);

                if (item != null)
                {
                    db.OrderForBuyers.Remove(item);
                    db.SaveChanges();
                    DownTrayKitchenerOrders.Background = Brushes.LightGreen;
                    DownTrayKitchenerOrders.Content = "Удалена запись --" + findId.Text;
                    findId.Text = "";
                    UserList.ItemsSource = db.OrderForBuyers.ToList();
                }
                else
                {
                    MessageBox.Show("Не найдено такое значение!");
                }
            }
        }
    }
}
