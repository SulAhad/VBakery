using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace VBakery
{
    public partial class Deliveryman : Window
    {
        public Deliveryman()
        {
            InitializeComponent();
            UpdateDataBase();
            AddButtons();
        }
        public void TimerForEmpty()
        {
            DispatcherTimer dispatcherTimer = new();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
            dispatcherTimer.Start();
        }///Диспетчер времени для отсчета времени для очистки уведмлений
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            DownTray.Content = "";
            DownTray.Background = Brushes.AliceBlue;
        }///Исходное положение после отсчета таймера
        public void DownTrayWindow()
        {
            DownTray.Background = Brushes.LightGreen;
            DownTray.Content = "Обновлено --" + DateTime.Now.ToString();
        }
        public void UpdateDataBase()
        {
            OrderForBuyersContext orderForBuyersContext = new();
            UserList.ItemsSource = orderForBuyersContext.OrderForBuyers.Where(p => p.OrderStatus == "Выполнено" ).ToList();
            DownTrayWindow();
            TimerForEmpty();
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
            UpdateDataBase();
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
        private void ButtonDelete(object sender, RoutedEventArgs e)
        {
            if (InputIdForDelete.Text == "")
            {
                DownTray.Content = "Не ввели номер!";
                DownTray.Background = Brushes.LightCoral;
            }
            else
            {
                OrderForBuyersContext buyersContext = new();
                InputIdForDelete.Text = InputIdForDelete.Text.Trim();
                int key = Convert.ToInt32(InputIdForDelete.Text.Trim());
                var item = buyersContext.OrderForBuyers.Find(key);
                if (item != null)
                {
                    buyersContext.OrderForBuyers.Remove(item);
                    buyersContext.SaveChanges();
                    DownTray.Background = Brushes.LightGreen;
                    DownTray.Content = "Удалена запись --" + InputIdForDelete.Text;
                    InputIdForDelete.Text = "";
                    UserList.ItemsSource = buyersContext.OrderForBuyers.ToList();
                }
                else
                {
                    MessageBox.Show("Не найдено такое значение!");
                }
            }
        }
        public void ButtonClickClear(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text = "";
        }
        private void MouseDownLock(object sender, MouseButtonEventArgs e)
        {
            LoginWindow loginWindow = new();
            loginWindow.Show();
            this.Close();
        }
        public void AddButtons()
        {
            for (int i = 0; i < 10; i++)
            {
                Button button = new Button();
                button.Content = i;
                button.Click += ButtonClick;
                button.Width = 50;
                button.Height = 50;
                button.Margin = new Thickness(2, 2, 2, 2);
                wrapPanelNums.Children.Add(button);
            }
        }
        public void ButtonClick(object sender, RoutedEventArgs e)
        {
            var button = (Button)sender;
            var num = button.Content;
            InputIdForDelete.Text += num;
        }
        private void OrderDelivered(object sender, RoutedEventArgs e)
        {
            using OrderForBuyersContext db = new OrderForBuyersContext();
            if (InputIdForDelete.Text == "")
            {
                DownTray.Content = "Не ввели номер!";
                DownTray.Background = Brushes.LightCoral;
            }
            else
            {
                InputIdForDelete.Text = InputIdForDelete.Text.Trim();
                string text = "Доставлено";
                int key = Convert.ToInt32(InputIdForDelete.Text.Trim());
                var item = db.OrderForBuyers.Find(key);
                if (item != null)
                {
                    item.OrderStatus = text;
                    db.SaveChanges();
                    DownTray.Background = Brushes.LightGreen;
                    DownTray.Content = "Запись изменена --" + InputIdForDelete.Text;
                    InputIdForDelete.Text = "";
                    UpdateDataBase();
                }
                else
                {
                    MessageBox.Show("Не найдено такое значение!");
                }
            }
        }
    }
}
