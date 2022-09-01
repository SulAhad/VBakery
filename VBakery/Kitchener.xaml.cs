using System;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using VBakery.DB;
using System.Linq;
using VBakery.Model;

namespace VBakery
{
    public partial class Kitchener : Window
    {
        
        public Kitchener()
        {
            InitializeComponent();

            UpdateOrderForBuyer();


            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandlerKeyDownEvent);

            imageHome.ToolTip = "Выйти на главную страницу";
            imageRecepts.ToolTip = "Открыть рецепты";
            imageChat.ToolTip = "Открыть лист претензий";

            
            
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
        public void UpdateOrderForBuyer()
        {
            using OrderForBuyersContext db = new();
            UserOrder.ItemsSource = db.OrderForBuyers.ToList();
            DownTray.Background = Brushes.LightGreen;
            DownTray.Content = "Обновлено --" + DateTime.Now.ToString();
        }
        private void MouseDownRefresh(object sender, MouseButtonEventArgs e)
        {
            UpdateOrderForBuyer();
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
                    OrderForBuyer BuyerKitchener = new ()
                    {
                        BuyerName = "\"Повар\"" + "\n" + AddName.Text,
                        BuyerMobile = AddMobile.Text,
                        WeightProduct = AddWeight.Text,
                        NameProduct = AddnameProduct.Text,
                        DeliveryDate = AddAddress.Text,
                        StaffComment = AddComm.Text,
                        OrderDateTime = DateTime.Now.ToString()
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
                    UserOrder.ItemsSource = db.OrderForBuyers.ToList();
                }
                else
                {
                    MessageBox.Show("Нет такого значения!");
                }
            }
        }
        //private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    ScrollBar scrollBar = new();
        //    scrollBar.Value = numeric.Value;
        //    DeleteAndGoToDelivery.Text = Convert.ToString(numeric.Value);
        //}

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
            LoginWindow loginWindow = new();
            loginWindow.ShowDialog();
            this.Close();
        }
    }
}

