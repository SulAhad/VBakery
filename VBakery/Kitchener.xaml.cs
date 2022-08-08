using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using VBakery.DB;
namespace VBakery
{
    public partial class Kitchener : Window
    {
        public Kitchener()
        {
            InitializeComponent();
            using OrderForBuyersContext db = new();
            UserList.ItemsSource = db.OrderForBuyers.ToArray();
            DownTrayKitchenerOrders.Content = "Обновлено --" + DateTime.Now.ToString();
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
        private void MouseDownRefresh(object sender, System.Windows.Input.MouseButtonEventArgs e)
        {
            using OrderForBuyersContext db = new();
            UserList.ItemsSource = db.OrderForBuyers.ToArray();
            DownTrayKitchenerOrders.Background = Brushes.LightGreen;
            DownTrayKitchenerOrders.Content = "Обновлено --" + DateTime.Now.ToString();
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
                    OrderForBuyer BuyerKitchener = new OrderForBuyer
                    {
                        name = "Повар" + "\n" + AddName.Text,
                        mobile = AddMobile.Text,
                        weight = AddWeight.Text,
                        nameProduct = AddnameProduct.Text,
                        address = AddAddress.Text,
                        comm = AddComm.Text,
                        dateTime = DateTime.Now.ToString()
                    };
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

            if (DeleteAndGoToDelivery.Text == "")
            {
                DownTrayKitchenerOrders.Content = "Не ввели номер!";
                DownTrayKitchenerOrders.Background = Brushes.LightCoral;
            }
            else
            {
                DeleteAndGoToDelivery.Text = DeleteAndGoToDelivery.Text.Trim();
                int key = Convert.ToInt32(DeleteAndGoToDelivery.Text.Trim());
                var item = db.OrderForBuyers.Find(key);
                if (item != null)
                {
                    db.OrderForBuyers.Remove(item);
                    db.SaveChanges();
                    DownTrayKitchenerOrders.Background = Brushes.LightGreen;
                    DownTrayKitchenerOrders.Content = "Удалена запись --" + DeleteAndGoToDelivery.Text;
                    DeleteAndGoToDelivery.Text = "";
                    UserList.ItemsSource = db.OrderForBuyers.ToList();
                }
                else
                {
                    MessageBox.Show("Нет такого значения!");
                }
            }
        }
        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ScrollBar scrollBar = new();
            scrollBar.Value = numeric.Value;
            DeleteAndGoToDelivery.Text = Convert.ToString(numeric.Value);
        }
    }
}

