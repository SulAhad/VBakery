using System;
using System.Data;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using VBakery.DB;
using VBakery.Model;

namespace VBakery
{
    public partial class PaymasterSales : Window
    {
        LoginWindow loginWindow = new();
        TimerEndsContext timerEndsContext = new();
        TimerStartsContext timerStartsContext = new();
        OrderForBuyersContext orderForBuyersContext = new();
        public PaymasterSales()
        {

            InitializeComponent();

            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandlerKeyDownEvent);

            subtotal.Text = "0";
            total.Text = "0";

            string time = DateTime.Now.ToString();
            OpenTime.Text = time;

            DispatcherTimer timer = new()
            {
                Interval = new TimeSpan(0, 0, 1),
                IsEnabled = true
            };

            timer.Tick += (o, t) => { Times.Text = DateTime.Now.ToString(); };
            timer.Start();

            TimeKitchener();
        }
        private void HandlerKeyDownEvent(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    break;
                case Key.Escape:
                    MessageBoxResult result = MessageBox.Show(
                    "Вы точно хотите выйти?",
                    "Сообщение",
                    MessageBoxButton.YesNo);
                    if (result == MessageBoxResult.Yes)
                    {
                        MainWindow mainWindow = new();
                        this.Close();
                        mainWindow.Close();
                    }
                    break;
                default:
                    break;
            }
        }//Клавиатура
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            int price = 12;
            string name = "свекла(нарезка)";
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            int price = 15;
            string name = "макароны отварные";
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            int price = 23;
            string name = "Рассольник";
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            int price = 16;
            string name = "Салат из моркови с майонезом";
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            int price = 15;
            string name = "Рис";
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            int price = 27;
            string name = "Суп вермишелевый";
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            int price = 13;
            string name = "Салат из капусты";
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            int price = 22;
            string name = "Нарезка: огурцы, помидоры";
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
        }
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            int price = 4;
            string name = "масло";
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
        }
        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            int price = 3;
            string name = "Хлеб ржаной";
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
        }
        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            int price = 32;
            string name = "Салат \"Лобио\"";
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
        }
        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            int price = 8;
            string name = "Зелень нарезка";
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
        }
        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            int price = 15;
            string name = "Компот из сухофруктов";
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
        }
        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            int price = 5;
            string name = "Соус красный";
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
        }
        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            int price = 15;
            string name = "Напиток ягодный";
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
        }
        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            int price = 5;
            string name = "Майонез";
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
        }
        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            int price = 10;
            string name = "Чай с лимоном";
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
        }
        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            int price = 5;
            string name = "Сметана";
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
        }
        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            int price = 8;
            string name = "Чай 0.2";
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += price + "\r\n";
                TextArea.Text += name + "\r\n";
                subtotal.Text += price;
            }
        }
        private void Button_Click_Total(object sender, RoutedEventArgs e)
        {
            total.Text = new DataTable().Compute(subtotal.Text, null).ToString().Replace(',', '.');
        }
        private void Image_MouseDown_Clear(object sender, MouseButtonEventArgs e)
        {
            NumArea.Text = "";
            TextArea.Text = "";
            subtotal.Text = Convert.ToString(0);
            total.Text = Convert.ToString(0);
            Errors.Text = "";
            ErrorsColor.Color = Color.FromRgb(135, 206, 250);
        }
        private void Image_MouseDown_Lock(object sender, MouseButtonEventArgs e)
        {
            PaymasterSales paymaster = new();
            loginWindow.ShowDialog();
            paymaster.Close();
            TimerEnd endTime = new()
            {
                end = DateTime.Now.ToString()
            };
            timerEndsContext.TimerEnds.Add(endTime);
            timerEndsContext.SaveChanges();
            
        }
        private void TimeKitchener()
        {
            TimerStart time = new TimerStart
            {
                begin = OpenTime.Text,
            };
            timerStartsContext.TimerStarts.Add(time);
            timerStartsContext.SaveChanges();
        }
        private void Button_Click_GotoKitchen(object sender, RoutedEventArgs e)
        {
            if (total.Text == "0" || TextArea.Text == "")
            {
                Errors.Text = "Не ввели значения!";
                ErrorsColor.Color = Color.FromRgb(240, 128, 128);
            }
            else
            {
                Errors.Text = "Заказ отправлен повару!";
                ErrorsColor.Color = Color.FromRgb(0, 100, 0);
                
                OrderForBuyer tim = new()
                {
                    BuyerName = "Кассир",
                    NameProduct = TextArea.Text,
                    StaffComment = commForKitchen.Text,
                    OrderDateTime = DateTime.Now.ToString(),
                    OrderPrice = (int)Convert.ToSingle(total.Text)
                };
                LogOrdersContext dbLogOrder = new();
                LogOrder logOrder = new LogOrder
                {
                    BuyerName = "Кассир",
                    NameProduct = TextArea.Text,
                    StaffComment = commForKitchen.Text,
                    OrderDateTime = DateTime.Now.ToString(),
                    OrderPrice = (int)Convert.ToSingle(total.Text)
                };
                dbLogOrder.LogOrders.Add(logOrder);
                dbLogOrder.SaveChanges();
                orderForBuyersContext.OrderForBuyers.Add(tim);
                orderForBuyersContext.SaveChanges();
                NumArea.Text = "";
                TextArea.Text = "";
                subtotal.Text = "0";
                total.Text = "0"; 
                commForKitchen.Text = "";
            }
        }
        private void Button_Click_Discount(object sender, RoutedEventArgs e)
        {
            float Total = Convert.ToSingle(total.Text);
            float Discount = Total / 100 * 5;
            float Total_Discount = Total - Discount;
            total.Text = Convert.ToString(Total_Discount);
        }
        private void Button_Click_Allowance(object sender, RoutedEventArgs e)
        {
            float Total = Convert.ToSingle(total.Text);
            float Allowance = Total / 100 * 5;
            float Total_Discount = Total + Allowance;
            total.Text = Convert.ToString(Total_Discount);
        }
        private void OpenCalculator(object sender, MouseButtonEventArgs e)
        {
            PaymasterCalculator paymasterCalculator = new();
            paymasterCalculator.ShowDialog();
        }
        private void Image_MouseDown_GetBackHome(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
            "Вы точно хотите выйти?",
            "Сообщение",
            MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                TimerEnd endTime = new()
                {
                    end = DateTime.Now.ToString()
                };
                timerEndsContext.TimerEnds.Add(endTime);
                timerEndsContext.SaveChanges();
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
        private void openChat(object sender, MouseButtonEventArgs e)
        {
            ChatRoom chatRoom = new();
            chatRoom.ShowDialog();
            chatRoom.chatUser.Content = "Кассир";
        }
        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
        private void Image_MouseDown_FindOrder(object sender, MouseButtonEventArgs e)
        {
            Orders orders = new();
            orders.ShowDialog();
        }
    }
}
