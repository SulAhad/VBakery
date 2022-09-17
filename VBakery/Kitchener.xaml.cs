using System;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using VBakery.DB;
using VBakery.Model;
using System.Windows.Controls;
namespace VBakery
{
    public partial class Kitchener: Window
    {

        public Kitchener()
        {
            InitializeComponent();
            AddButton();
            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandlerKeyDownEvent);
            imageHome.ToolTip = "Выйти на главную страницу";
            imageRecepts.ToolTip = "Открыть рецепты";
            imageChat.ToolTip = "Открыть лист претензий";
        }
        public void ButtonClickPlus(object sender, RoutedEventArgs e)
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
        public void ButtonClickMinus(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        public void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        public void AddButton()
        {
            OrderForBuyersContext orderForBuyersContext = new();
            foreach (OrderForBuyer order in orderForBuyersContext.OrderForBuyers)
            {
                StackPanel stackPanel = new();
                stackPanel.DataContext = order.Id;
                stackPanel.MouseDown += ButtonOnClick;

                Label label = new();
                label.Content = order.Id;
                label.Margin = new Thickness(1, 1, 1, 0);
                label.Background = Brushes.LightBlue;

                Label label1 = new();
                label1.Content = order.NameProduct;
                label1.Margin = new Thickness(1, 0, 1, 0);
                label1.Background = Brushes.LightBlue;

                Label label2 = new();
                label2.Content = order.OrderStatus;
                label2.Margin = new Thickness(1, 0, 1, 1);
                if(order.OrderStatus == "Выполнено")
                {
                    label2.Background = Brushes.Red;
                }
                else
                {
                    label2.Background = Brushes.LightBlue;
                }

                
                if (order.OrderStatus == "В процессе")
                {
                    stackPanel.Children.Add(label);
                    stackPanel.Children.Add(label1);
                    stackPanel.Children.Add(label2);
                }

                this.ScrollData.Children.Add(stackPanel);
            }
        }
        public void ButtonOnClick(object sender, EventArgs eventArgs)
        {
            var stack = (StackPanel)sender;
            int text = (int)stack.DataContext;
            InputIdForDelete.Text = Convert.ToString(text);
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
            this.ScrollData.Children.Clear();
            AddButton();
            DownTray.Background = Brushes.LightGreen;
            DownTray.Content = "Обновлено --" + DateTime.Now.ToString();

        }
        public void MouseDownGotoHome(object sender, MouseButtonEventArgs e)
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
            chatRoom.chatUser.Content = "Повар";
        }
        private void ButtonClickAdd(object sender, RoutedEventArgs e)
        {
            if (AddName.Text == "" || AddMobile.Text == "" || AddWeight.Text == "" || AddnameProduct.Text == "" || AddAddress.Text == "" || AddComm.Text == "")
            {
                DownTrayAddRecipe.Content = "Не ввели данные";
                ErrorsColor.Color = Color.FromRgb(240, 128, 128);
            }
            else
            {
                using OrderForBuyersContext db = new();
                {
                    OrderForBuyer BuyerKitchener = new()
                    {
                        BuyerName = "\"Повар\"" + "\n" + AddName.Text,
                        BuyerMobile = AddMobile.Text,
                        WeightProduct = AddWeight.Text,
                        NameProduct = AddnameProduct.Text,
                        DeliveryDate = AddAddress.Text,
                        StaffComment = AddComm.Text,
                        OrderDateTime = DateTime.Now.ToString(),
                        OrderStatus = "В процессе"
                    };
                    LogOrdersContext dbLogOrder = new();
                    LogOrder logOrder = new LogOrder
                    {
                        BuyerName = "\"Повар\"" + "\n" + AddName.Text,
                        BuyerMobile = AddMobile.Text,
                        WeightProduct = AddWeight.Text,
                        NameProduct = AddnameProduct.Text,
                        DeliveryDate = AddAddress.Text,
                        StaffComment = AddComm.Text,
                        OrderDateTime = DateTime.Now.ToString()
                    };
                    dbLogOrder.LogOrders.Add(logOrder);
                    dbLogOrder.SaveChanges();
                    db.OrderForBuyers.Add(BuyerKitchener);
                    db.SaveChanges();
                    DownTrayAddRecipe.Content = "Добавлено!";
                    ErrorsColor.Color = Color.FromRgb(0, 100, 0);
                    AddName.Text = "";
                    AddMobile.Text = "";
                    AddWeight.Text = "";
                    AddnameProduct.Text = "";
                    AddAddress.Text = "";
                    AddComm.Text = "";
                }
            }
        }
        private void ButtonDeleteAndGoToDelivery(object sender, RoutedEventArgs e)
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
                int key = Convert.ToInt32(InputIdForDelete.Text.Trim());
                var item = db.OrderForBuyers.Find(key);
                if (item != null)
                {
                    db.OrderForBuyers.Remove(item);
                    db.SaveChanges();
                    DownTray.Background = Brushes.LightGreen;
                    DownTray.Content = "Удалена запись --" + InputIdForDelete.Text;
                    InputIdForDelete.Text = "";
                    this.ScrollData.Children.Clear();
                    AddButton();
                }
                else
                {
                    MessageBox.Show("Нет такого значения!");
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
        private void ButtonClickDone(object sender, RoutedEventArgs e)
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
                InputIdForDelete.Text = InputIdForDelete.Text.Trim();
                string text = "Выполнено";
                int key = Convert.ToInt32(InputIdForDelete.Text.Trim());
                var item = db.OrderForBuyers.Find(key);

                if (item != null)
                {
                    item.OrderStatus = text;
                    db.SaveChanges();
                    DownTray.Background = Brushes.LightGreen;
                    DownTray.Content = "Запись обновлена --" + InputIdForDelete.Text;
                    InputIdForDelete.Text = "";
                    this.ScrollData.Children.Clear();
                    AddButton();
                }
                else
                {
                    MessageBox.Show("Нет такого значения!");
                }
            }
        }
    }
}

