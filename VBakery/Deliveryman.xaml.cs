using System;
using System.Linq;
using System.Threading;
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
            UpdateOrderForDelivery();
        }
        public void UpdateOrderForDelivery()
        {
            using OrderForDeliverysContext db = new();
            UserList.ItemsSource = db.OrderForDeliverys.ToList();
            DownTray.Content = "Обновлено --" + DateTime.Now.ToString();
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
        private void MouseDownRefresh(object sender, MouseButtonEventArgs e)
        {
            UpdateOrderForDelivery();
        }
        private void MouseDownGotoHome(object sender, MouseButtonEventArgs e)
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
        private void OpenRecepts(object sender, MouseButtonEventArgs e)
        {
            Recepts recepts = new();
            recepts.ShowDialog();
        }
        private void OpenChat(object sender, MouseButtonEventArgs e)
        {
            ChatRoom chatRoom = new();
            chatRoom.ShowDialog();
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
        //private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    ScrollBar scrollBar = new();
        //    scrollBar.Value = numeric.Value;
        //    findId.Text = Convert.ToString(numeric.Value);
        //}
        private void ButtonDelete(object sender, RoutedEventArgs e)
        {
            using OrderForDeliverysContext db = new();

            if (InputIdForDelete.Text == "")
            {
                DownTray.Content = "Не ввели номер!";
                DownTray.Background = Brushes.LightCoral;
            }
            else
            {
                InputIdForDelete.Text = InputIdForDelete.Text.Trim();

                int key = Convert.ToInt32(InputIdForDelete.Text.Trim());

                var item = db.OrderForDeliverys.Find(key);

                if (item != null)
                {
                    db.OrderForDeliverys.Remove(item);
                    db.SaveChanges();
                    DownTray.Background = Brushes.LightGreen;
                    DownTray.Content = "Удалена запись --" + InputIdForDelete.Text;
                    InputIdForDelete.Text = "";
                    UserList.ItemsSource = db.OrderForDeliverys.ToList();
                }
                else
                {
                    MessageBox.Show("Не найдено такое значение!");
                }
            }
        }
        public void ButtonClick_1(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 1;
        }
        public void ButtonClick_2(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 2;
        }
        public void ButtonClick_3(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 3;
        }
        public void ButtonClick_4(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 4;
        }
        public void ButtonClick_5(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 5;
        }
        public void ButtonClick_6(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 6;
        }
        public void ButtonClick_7(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 7;
        }
        public void ButtonClick_8(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 8;
        }
        public void ButtonClick_9(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 9;
        }
        public void ButtonClick_0(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 0;
        }
        public void ButtonClickClear(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text = "";
        }
        private void MouseDownLock(object sender, MouseButtonEventArgs e)
        {
            Deliveryman deliveryman = new();
            deliveryman.Close();
            LoginWindow loginWindow = new();
            loginWindow.ShowDialog();
        }
    }
}
