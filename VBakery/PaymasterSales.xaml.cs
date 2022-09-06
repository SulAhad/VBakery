using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using VBakery.DB;
using VBakery.MenuDB;
using VBakery.Model;
namespace VBakery
{
    public partial class PaymasterSales : Window
    {
        private readonly LoginWindow loginWindow = new();
        private readonly TimerEndsContext timerEndsContext = new();
        private readonly TimerStartsContext timerStartsContext = new();
        private readonly OrderForBuyersContext orderForBuyersContext = new();
        private readonly LogOrdersContext dbLogOrder = new();
        public int clickDiscount = 0;
        public int clickAllowance = 0;
        public int countFirst;
        public int countSecond;
        public int countThird;
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
                        Close();
                        mainWindow.Close();
                    }
                    break;
                default:
                    break;
            }
        }//Клавиатура
        private void Button_Click_Total(object sender, RoutedEventArgs e)
        {
            total.Text = new DataTable().Compute(subtotal.Text, null).ToString();
        }///Суммирование полей в общую сумму
        private void Image_MouseDown_Clear(object sender, MouseButtonEventArgs e)
        {
            NumArea.Text = "";
            TextArea.Text = "";
            subtotal.Text = Convert.ToString(0);
            total.Text = Convert.ToString(0);
            Errors.Text = "";
            ErrorsColor.Color = Color.FromRgb(135, 206, 250);
        }///Картинка стрелка для очистки полей
        private void Image_MouseDown_Lock(object sender, MouseButtonEventArgs e)
        {
            Close();
            _ = loginWindow.ShowDialog();
            TimerEnd endTime = new()
            {
                end = DateTime.Now.ToString()
            };
            _ = timerEndsContext.TimerEnds.Add(endTime);
            _ = timerEndsContext.SaveChanges();

        }///Блокировка экрана и запись в базу о выходе
        private void TimeKitchener()
        {
            TimerStart time = new()
            {
                begin = OpenTime.Text,
            };
            _ = timerStartsContext.TimerStarts.Add(time);
            _ = timerStartsContext.SaveChanges();
        }///Время запуска приложения и запись в базу данных
        public void TimerForEmty()
        {
            DispatcherTimer dispatcherTimer = new();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
            dispatcherTimer.Start();
        }///Диспетчер времени для отсчета времени для очистки уведмлений
        private void DispatcherTimer_Tick(object sender, EventArgs e)
        {
            Errors.Text = "";
            ErrorsColor.Color = Color.FromRgb(135, 206, 250);
        }///Исходное положение после отсчета таймера
        private void Button_Click_GotoKitchen(object sender, RoutedEventArgs e)
        {
            if (total.Text == "0" || TextArea.Text == "")
            {
                Errors.Text = "Не ввели значения!";
                ErrorsColor.Color = Color.FromRgb(240, 128, 128);
                TimerForEmty();
            }
            else
            {
                clickDiscount = 0;
                clickAllowance = 0;
                Errors.Text = "Заказ отправлен повару!";
                ErrorsColor.Color = Color.FromRgb(0, 100, 0);
                TimerForEmty();
                OrderForBuyer tim = new()
                {
                    BuyerName = "\"Кассир\"",
                    NameProduct = TextArea.Text,
                    StaffComment = commForKitchen.Text,
                    OrderDateTime = DateTime.Now.ToString(),
                    OrderPrice = (float)Convert.ToSingle(total.Text)
                };
                LogOrder logOrder = new LogOrder
                {
                    BuyerName = "\"Кассир\"",
                    NameProduct = TextArea.Text,
                    StaffComment = commForKitchen.Text,
                    OrderDateTime = DateTime.Now.ToString(),
                    OrderPrice = (float)Convert.ToSingle(total.Text)
                };
                _ = dbLogOrder.LogOrders.Add(logOrder);
                _ = dbLogOrder.SaveChanges();
                _ = orderForBuyersContext.OrderForBuyers.Add(tim);
                _ = orderForBuyersContext.SaveChanges();
                NumArea.Text = "";
                TextArea.Text = "";
                subtotal.Text = "0";
                total.Text = "0"; 
                commForKitchen.Text = "";
            }
        }///Кнопка отправки данных в базу данных
        private void Button_Click_Discount(object sender, RoutedEventArgs e)
        {
            clickDiscount ++;
            if(clickDiscount <= 1)
            {
                float Total = Convert.ToSingle(total.Text);
                float Discount = Total / 100 * 5;
                float Total_Discount = Total - Discount;
                total.Text = Convert.ToString(Total_Discount);
            }
        }///Кнопка скидки на 5%
        private void Button_Click_Allowance(object sender, RoutedEventArgs e)
        {
            clickAllowance++;
            if(clickAllowance <= 1)
            {
                float Total = Convert.ToSingle(total.Text);
                float Allowance = Total / 100 * 5;
                float Total_Discount = Total + Allowance;
                total.Text = Convert.ToString(Total_Discount);
            } 
        }///Кнопка надбавки на 5%
        private void OpenCalculator(object sender, MouseButtonEventArgs e)
        {
            PaymasterCalculator paymasterCalculator = new();
            _ = paymasterCalculator.ShowDialog();
        }///Открыть калькулятор
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
                _ = timerEndsContext.TimerEnds.Add(endTime);
                _ = timerEndsContext.SaveChanges();
                MainWindow mainWindow = new();
                mainWindow.Show();
                Close();
            }
        }///Кнопка выхода домой
        private void OpenRecepts(object sender, MouseButtonEventArgs e)
        {
            Recepts recepts = new();
            _ = recepts.ShowDialog();
        }///Кнопка открыть рецепты
        private void OpenChat(object sender, MouseButtonEventArgs e)
        {
            ChatRoom chatRoom = new();
            _ = chatRoom.ShowDialog();
            chatRoom.chatUser.Content = "Кассир";
        }///Кнопка открыть чат
        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            Close();
        }///Верхняя кнопка закрыть окно
        private void Image_MouseDown_FindOrder(object sender, MouseButtonEventArgs e)
        {
            Orders orders = new();
            _ = orders.ShowDialog();
        }///Кнопка открыть заказы
        private void Label_MouseEnter(object sender, MouseEventArgs e)
        {
            MouseDownClickTotal.Background = Brushes.Gray;
        }///Вхождение указателя в кнопку ИТОГ и изменения цвета
        private void Label_MouseLeave(object sender, MouseEventArgs e)
        {
            MouseDownClickTotal.Background = Brushes.Black;
        }///Выход указателя в кнопку ИТОГ и изменения цвета
        private void Click_GotoKitchen_MouseEnter(object sender, MouseEventArgs e)
        {
            Click_GotoKitchen.Background = Brushes.Gray;
        }///Вхождение указателя в кнопку ОТПРАВИТЬ и изменения цвета
        private void Click_GotoKitchen_MouseLeave(object sender, MouseEventArgs e)
        {
            Click_GotoKitchen.Background = Brushes.LightCoral;
        }///Выход указателя в кнопку ОТПРАВИТЬ и изменения цвета
        private void Label_MouseDownFirstMenu(object sender, MouseButtonEventArgs e)
        {
            countFirst++;
            if (countFirst <= 1)
            {
                AddButtonFirst();
            }
        }///Кнопка вызова первого блюда
        private void Label_MouseDownSecondMenu(object sender, MouseButtonEventArgs e)
        {
            countSecond++;
            if (countSecond <= 1)
            {
                AddButtonSecond();
            }
        }///Кнопка вызова второго блюда
        private void Label_MouseDownThirdMenu(object sender, MouseButtonEventArgs e)
        {
            countThird++;
            if (countThird <= 1)
            {
                AddButtonThird();
            }
        }///Кнопка вызова третьего блюда
        public void AddButtonFirst()
        {
            FirstMenuContext firstMenuContext = new();
            foreach (FirstMenu first in firstMenuContext.FirstMenus)
            {
                Button button = new()
                {
                    Content = first.Name,
                    FontSize = 14,
                    Width = 120,
                    Height = 120,
                    Margin = new Thickness(2, 2, 2, 2),
                    Name = "btn",
                    DataContext = first.Price
                };
                button.Click += ButtonOnClickFirst;
                _ = WrapPanelAddButton1.Children.Add(button);
            }
        }///Создание кнопок для первого блюда
        public void AddButtonSecond()
        {
            SecondMenuContext secondMenuContext = new();
            foreach (SecondMenu second in secondMenuContext.SecondMenus)
            {
                Button button = new()
                {
                    Content = second.Name,
                    FontSize = 14,
                    Width = 120,
                    Height = 120,
                    Margin = new Thickness(2, 2, 2, 2),
                    Name = "btn",
                    DataContext = second.Price
                };
                button.Click += ButtonOnClickSecond;
                _ = WrapPanelAddButton2.Children.Add(button);
            }
        }///Создание кнопок для второго блюда
        private void AddButtonThird()
        {
            ThirdMenuContext thirdMenuContext = new();
            foreach (ThirdMenu third in thirdMenuContext.ThirdMenus)
            {
                Button button = new()
                {
                    Content = third.Name,
                    FontSize = 14,
                    Width = 120,
                    Height = 120,
                    Margin = new Thickness(2, 2, 2, 2),
                    Name = "btn",
                    DataContext = third.Price
                };
                button.Click += ButtonOnClickThird;
                _ = WrapPanelAddButton3.Children.Add(button);
            }
        }///Создание кнопок для третьего блюда
        private void ButtonOnClickSecond(object sender, RoutedEventArgs e)
        {
            Button button = (Button)sender;
            string name = (string)button.Content;
            string price = (string)button.DataContext;
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
        }///Действия для кнопок
        public void ButtonOnClickFirst(object sender, EventArgs eventArgs)
        {
            Button button = (Button)sender;
            string name = (string)button.Content;
            string price = (string)button.DataContext;
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
        }///Действия для кнопок
        public void ButtonOnClickThird(object sender, EventArgs eventArgs)
        {
            Button button = (Button)sender;
            string name = (string)button.Content;
            string price = (string)button.DataContext;
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
        }///Действия для кнопок
    }
}
