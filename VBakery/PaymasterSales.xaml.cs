using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.ObjectModel;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using VBakery.DB;
using VBakery.MenuDB;

namespace VBakery
{
    public partial class PaymasterSales : Window
    {
        private DispatcherTimer timer;
        public class ArrLst
        {
            public int[] array = new int[100];
        }
        public PaymasterSales()
        {
            InitializeComponent();

            using FirstCourseContext db = new();
            Button button = new();

            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandlerKeyDownEvent);

            subtotal.Text = "0";
            total.Text = "0";
            string time = DateTime.Now.ToString();
            OpenTime.Text = time;
            var timer = new System.Windows.Threading.DispatcherTimer();
            timer.Interval = new TimeSpan(0, 0, 1);
            timer.IsEnabled = true;
            timer.Tick += (o, t) => { Times.Text = DateTime.Now.ToString(); };
            timer.Start();
            LoginWindow loginWindow = new();
            TimeKitchener();
        }
        void PrintText(object sender, SelectionChangedEventArgs args)
        {
            ListBoxItem lbi = ((sender as ListBox).SelectedItem as ListBoxItem);
            TextArea.Text = "   You selected " + lbi.Content.ToString() + ".";
        }
        //public void TimerScreen()
        //{
        //    timer = new DispatcherTimer();
        //    timer.Interval = new TimeSpan(0, 0, 2);
        //    timer.Tick += new EventHandler(timer_Tick);
        //    timer.Start();

        //}
        //private void timer_Tick(object sender, EventArgs e)
        //{
        //    LoginWindow loginWindow = new LoginWindow();
        //    loginWindow.Show();
        //    this.Close();
        //}
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
        private void Image_MouseDown(object sender, MouseButtonEventArgs e)
        {

        }
        public void Button_Click(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                int A = 12;
                NumArea.Text += A + "\r\n";
                TextArea.Text += "свекла(нарезка)" + "\r\n";
                subtotal.Text += A;
            }
            else
            {
                subtotal.Text += "+";
                int A = 12;
                NumArea.Text += A + "\r\n";
                TextArea.Text += "свекла(нарезка)" + "\r\n";
                subtotal.Text += A;
            }
        }
        private void Button_Click_1(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                int B = 15;
                NumArea.Text += B + "\r\n";
                TextArea.Text += "макароны отварные" + "\r\n";
                subtotal.Text += B;
            }
            else
            {
                subtotal.Text += "+";
                int B = 15;
                NumArea.Text += B + "\r\n";
                TextArea.Text += "макароны отварные" + "\r\n";
                subtotal.Text += B;
            }
        }
        private void Button_Click_3(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += 23 + "\r\n";
                TextArea.Text += "Рассольник" + "\r\n";
                int C = 23;
                ArrLst arrLst = new ArrLst();
                arrLst.array[2] = C;
                subtotal.Text += arrLst.array[2];
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += 23 + "\r\n";
                TextArea.Text += "Рассольник" + "\r\n";
                int C = 23;
                ArrLst arrLst = new ArrLst();
                arrLst.array[2] = C;
                subtotal.Text += arrLst.array[2];
            }
        }
        private void Button_Click_4(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += 16 + "\r\n";
                TextArea.Text += "Салат из моркови с майонезом" + "\r\n";
                int D = 16;
                ArrLst arrLst = new ArrLst();
                arrLst.array[3] = D;
                subtotal.Text += arrLst.array[3];
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += 16 + "\r\n";
                TextArea.Text += "Салат из моркови с майонезом" + "\r\n";
                int D = 16;
                ArrLst arrLst = new ArrLst();
                arrLst.array[3] = D;
                subtotal.Text += arrLst.array[3];
            }
        }
        private void Button_Click_5(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += 15 + "\r\n";
                TextArea.Text += "Рис" + "\r\n";
                int E = 15;
                ArrLst arrLst = new ArrLst();
                arrLst.array[4] = E;
                subtotal.Text += arrLst.array[4];
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += 15 + "\r\n";
                TextArea.Text += "Рис" + "\r\n";
                int E = 15;
                ArrLst arrLst = new ArrLst();
                arrLst.array[4] = E;
                subtotal.Text += arrLst.array[4];
            }
        }
        private void Button_Click_6(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += 27 + "\r\n";
                TextArea.Text += "Суп вермишелевый" + "\r\n";
                int F = 27;
                ArrLst arrLst = new ArrLst();
                arrLst.array[5] = F;
                subtotal.Text += arrLst.array[5];
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += 27 + "\r\n";
                TextArea.Text += "Суп вермишелевый" + "\r\n";
                int F = 27;
                ArrLst arrLst = new ArrLst();
                arrLst.array[5] = F;
                subtotal.Text += arrLst.array[5];
            }
        }
        private void Button_Click_7(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += 13 + "\r\n";
                TextArea.Text += "Салат из капусты" + "\r\n";
                int G = 13;
                ArrLst arrLst = new ArrLst();
                arrLst.array[6] = G;
                subtotal.Text += arrLst.array[6];
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += 13 + "\r\n";
                TextArea.Text += "Салат из капусты" + "\r\n";
                int G = 13;
                ArrLst arrLst = new ArrLst();
                arrLst.array[6] = G;
                subtotal.Text += arrLst.array[6];
            }
        }
        private void Button_Click_8(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += 22 + "\r\n";
                TextArea.Text += "Нарезка: огурцы, помидоры" + "\r\n";
                int H = 22;
                ArrLst arrLst = new ArrLst();
                arrLst.array[7] = H;
                subtotal.Text += arrLst.array[7];
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += 22 + "\r\n";
                TextArea.Text += "Нарезка: огурцы, помидоры" + "\r\n";
                int H = 22;
                ArrLst arrLst = new ArrLst();
                arrLst.array[7] = H;
                subtotal.Text += arrLst.array[7];
            }

        }
        private void Button_Click_9(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += 4 + "\r\n";
                TextArea.Text += "масло" + "\r\n";
                int I = 4;
                ArrLst arrLst = new ArrLst();
                arrLst.array[8] = I;
                subtotal.Text += arrLst.array[8];
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += 4 + "\r\n";
                TextArea.Text += "масло" + "\r\n";
                int I = 4;
                ArrLst arrLst = new ArrLst();
                arrLst.array[8] = I;
                subtotal.Text += arrLst.array[8];
            }
        }
        private void Button_Click_10(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += 3 + "\r\n";
                TextArea.Text += "Хлеб ржаной" + "\r\n";
                int J = 3;
                ArrLst arrLst = new ArrLst();
                arrLst.array[9] = J;
                subtotal.Text += arrLst.array[9];
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += 3 + "\r\n";
                TextArea.Text += "Хлеб ржаной" + "\r\n";
                int J = 3;
                ArrLst arrLst = new ArrLst();
                arrLst.array[9] = J;
                subtotal.Text += arrLst.array[9];
            }
        }
        private void Button_Click_11(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += 32 + "\r\n";
                TextArea.Text += "Салат \"Лобио\"" + "\r\n";
                int K = 32;
                ArrLst arrLst = new ArrLst();
                arrLst.array[10] = K;
                subtotal.Text += arrLst.array[10];
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += 32 + "\r\n";
                TextArea.Text += "Салат \"Лобио\"" + "\r\n";
                int K = 32;
                ArrLst arrLst = new ArrLst();
                arrLst.array[10] = K;
                subtotal.Text += arrLst.array[10];
            }
        }
        private void Button_Click_12(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += 8 + "\r\n";
                TextArea.Text += "Зелень нарезка" + "\r\n";
                int L = 8;
                ArrLst arrLst = new ArrLst();
                arrLst.array[11] = L;
                subtotal.Text += arrLst.array[11];
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += 8 + "\r\n";
                TextArea.Text += "Зелень нарезка" + "\r\n";
                int L = 8;
                ArrLst arrLst = new ArrLst();
                arrLst.array[11] = L;
                subtotal.Text += arrLst.array[11];
            }
        }
        private void Button_Click_13(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += 15 + "\r\n";
                TextArea.Text += "Компот из сухофруктов" + "\r\n";
                int M = 15;
                ArrLst arrLst = new ArrLst();
                arrLst.array[12] = M;
                subtotal.Text += arrLst.array[12];
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += 15 + "\r\n";
                TextArea.Text += "Компот из сухофруктов" + "\r\n";
                int M = 15;
                ArrLst arrLst = new ArrLst();
                arrLst.array[12] = M;
                subtotal.Text += arrLst.array[12];
            }
        }
        private void Button_Click_14(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += 5 + "\r\n";
                TextArea.Text += "Соус красный" + "\r\n";
                int N = 5;
                ArrLst arrLst = new ArrLst();
                arrLst.array[13] = N;
                subtotal.Text += arrLst.array[13];
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += 5 + "\r\n";
                TextArea.Text += "Соус красный" + "\r\n";
                int N = 5;
                ArrLst arrLst = new ArrLst();
                arrLst.array[13] = N;
                subtotal.Text += arrLst.array[13];
            }
        }
        private void Button_Click_15(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += 15 + "\r\n";
                TextArea.Text += "Напиток ягодный" + "\r\n";
                int O = 15;
                ArrLst arrLst = new ArrLst();
                arrLst.array[14] = O;
                subtotal.Text += arrLst.array[14];
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += 15 + "\r\n";
                TextArea.Text += "Напиток ягодный" + "\r\n";
                int O = 15;
                ArrLst arrLst = new ArrLst();
                arrLst.array[14] = O;
                subtotal.Text += arrLst.array[14];
            }
        }
        private void Button_Click_16(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += 5 + "\r\n";
                TextArea.Text += "Майонез" + "\r\n";
                int P = 5;
                ArrLst arrLst = new ArrLst();
                arrLst.array[15] = P;
                subtotal.Text += arrLst.array[15];
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += 5 + "\r\n";
                TextArea.Text += "Майонез" + "\r\n";
                int P = 5;
                ArrLst arrLst = new ArrLst();
                arrLst.array[15] = P;
                subtotal.Text += arrLst.array[15];
            }
        }
        private void Button_Click_17(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += 10 + "\r\n";
                TextArea.Text += "Чай с лимоном" + "\r\n";
                int Q = 10;
                ArrLst arrLst = new ArrLst();
                arrLst.array[16] = Q;
                subtotal.Text += arrLst.array[16];
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += 10 + "\r\n";
                TextArea.Text += "Чай с лимоном" + "\r\n";
                int Q = 10;
                ArrLst arrLst = new ArrLst();
                arrLst.array[16] = Q;
                subtotal.Text += arrLst.array[16];
            }
        }
        private void Button_Click_18(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += 5 + "\r\n";
                TextArea.Text += "Сметана" + "\r\n";
                int R = 5;
                ArrLst arrLst = new ArrLst();
                arrLst.array[17] = R;
                subtotal.Text += arrLst.array[17];
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += 5 + "\r\n";
                TextArea.Text += "Сметана" + "\r\n";
                int R = 5;
                ArrLst arrLst = new ArrLst();
                arrLst.array[17] = R;
                subtotal.Text += arrLst.array[17];
            }
        }
        private void Button_Click_19(object sender, RoutedEventArgs e)
        {
            if (subtotal.Text == "0")
            {
                subtotal.Text = "";
                NumArea.Text += 8 + "\r\n";
                TextArea.Text += "Чай 0.2" + "\r\n";
                int S = 8;
                ArrLst arrLst = new ArrLst();
                arrLst.array[18] = S;
                subtotal.Text += arrLst.array[18];
            }
            else
            {
                subtotal.Text += "+";
                NumArea.Text += 8 + "\r\n";
                TextArea.Text += "Чай 0.2" + "\r\n";
                int S = 8;
                ArrLst arrLst = new ArrLst();
                arrLst.array[18] = S;
                subtotal.Text += arrLst.array[18];
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
            LoginWindow loginWindow = new();
            loginWindow.Show();
            this.Close();
            using TimerEndsContext db = new();
            TimerEnd endTime = new TimerEnd
            {
                end = DateTime.Now.ToString()
            };
            db.TimerEnds.Add(endTime);
            db.SaveChanges();
            
        }
        private void TimeKitchener()
        {

            using TimerStartsContext db = new();
            TimerStart time = new TimerStart
            {
                begin = OpenTime.Text,
            };
            db.TimerStarts.Add(time);
            db.SaveChanges();
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
                using OrderForBuyersContext db = new();
                OrderForBuyer tim = new OrderForBuyer
                {
                    name = "Кассир",
                    nameProduct = TextArea.Text,
                    address = "",
                    comm = commForKitchen.Text,
                    dateTime = DateTime.Now.ToString(),
                    total = total.Text

            };
                db.OrderForBuyers.Add(tim);
                db.SaveChanges();
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
            paymasterCalculator.Show();
            paymasterCalculator.Owner = this;
        }
        private void Image_MouseDown_GetBackHome(object sender, MouseButtonEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
            "Вы точно хотите выйти?",
            "Сообщение",
            MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                using TimerEndsContext db = new();
                TimerEnd endTime = new TimerEnd
                {
                    end = DateTime.Now.ToString()
                };
                db.TimerEnds.Add(endTime);
                db.SaveChanges();
                MainWindow mainWindow = new();
                mainWindow.Show();
                this.Close();
            }
        }
        private void OpenRecepts(object sender, MouseButtonEventArgs e)
        {
            Recepts recepts = new();
            recepts.Show();

        }
        private void openChat(object sender, MouseButtonEventArgs e)
        {

            ChatRoom chatRoom = new();

            chatRoom.Show();
            chatRoom.chatUser.Content = "Кассир";
        }
        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
