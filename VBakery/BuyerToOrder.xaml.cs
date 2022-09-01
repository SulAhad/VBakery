using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;
using VBakery.DB;
using VBakery.Model;

namespace VBakery
{
    public partial class BuyerToOrder : Window
    {
        BuyerTerminal buyerTerminal = new();
        public BuyerToOrder()
        {
            InitializeComponent();
            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandlerKeyDownEvent);
            CommToolTip.ToolTip = "Необязательно";

            InputOrderArea.Text += buyerTerminal.TextAreaTrash.Text;
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
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            name.Text = name.Text.Trim(' ');
            mobile.Text = mobile.Text.Trim(' ');
            address.Text = address.Text.Trim(' ');

            name.ToolTip = null;
            name.Background = Brushes.Transparent;
            mobile.ToolTip = null;
            mobile.Background = Brushes.Transparent;
            address.ToolTip = null;
            address.Background = Brushes.Transparent;
            dateOrder.ToolTip = null;
            dateOrder.Background = Brushes.Transparent;
        }
        private void SendButtonNotificdtions()
        {
            DispatcherTimer dispatcherTimer = new();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
            dispatcherTimer.Start();
        }
        public void Send(object sender, RoutedEventArgs e)
        {
            bool flag = true;

            name.Text = name.Text.Trim(' ');
            mobile.Text = mobile.Text.Trim(' ');
            address.Text = address.Text.Trim(' ');

            name.ToolTip = null;
            name.Background = Brushes.AliceBlue;
            mobile.ToolTip = null;
            mobile.Background = Brushes.AliceBlue;
            address.ToolTip = null;
            address.Background = Brushes.AliceBlue;
            dateOrder.ToolTip = null;
            dateOrder.Background = Brushes.AliceBlue;

            if (name.Text.Length <= 2)
            {
                name.ToolTip = "Не ввели имя";
                name.Background = Brushes.LightCoral;
                SendButtonNotificdtions();
                flag = false;
            }
            if (mobile.Text.Length != 11 )
            {
                mobile.ToolTip = "Введите номер телефона";
                mobile.Background = Brushes.LightCoral;
                SendButtonNotificdtions();
                flag = false;
            }
            if (address.Text == "")
            {
                address.ToolTip = "Не ввели адресс";
                address.Background = Brushes.LightCoral;
                SendButtonNotificdtions();
                flag = false;
            }
            if (dateOrder.Text == "")
            {
                dateOrder.ToolTip = "Выберите дату доставки";
                dateOrder.Background = Brushes.LightCoral;
                SendButtonNotificdtions();
            }
            if(flag)
            {
                OrderForBuyersContext db = new();
                OrderForBuyer tim = new OrderForBuyer
                {
                    BuyerName = "\"Киоск\"" + " " + name.Text,
                    BuyerMobile = mobile.Text,
                    NameProduct = InputOrderArea.Text,
                    DeliveryAddress = address.Text,
                    StaffComment = comm.Text,
                    DeliveryDate = dateOrder.Text,
                    OrderPrice = Convert.ToInt32(TotalPrice.Text),
                    OrderDateTime = DateTime.Now.ToString(),
                };
                OrderForDeliverysContext dbDelivery = new();
                OrderForDelivery order = new OrderForDelivery
                {
                    BuyerName = "\"Киоск\"" + " " + name.Text,
                    BuyerMobile = mobile.Text,
                    NameProduct = InputOrderArea.Text,
                    DeliveryAddress = address.Text,
                    StaffComment = comm.Text,
                    DeliveryDate = dateOrder.Text,
                    OrderPrice = Convert.ToInt32(TotalPrice.Text),
                    OrderDateTime = DateTime.Now.ToString()
                };
                LogOrdersContext dbLogOrder = new();
                LogOrder logOrder = new LogOrder
                {
                    BuyerName = "\"Киоск\"" + " " + name.Text,
                    BuyerMobile = mobile.Text,
                    NameProduct = InputOrderArea.Text,
                    DeliveryAddress = address.Text,
                    StaffComment = comm.Text,
                    DeliveryDate = dateOrder.Text,
                    OrderPrice = Convert.ToInt32(TotalPrice.Text),
                    OrderDateTime = DateTime.Now.ToString()
                };
                db.OrderForBuyers.Add(tim);
                db.SaveChanges();
                dbDelivery.OrderForDeliverys.Add(order);
                dbDelivery.SaveChanges();
                dbLogOrder.LogOrders.Add(logOrder);
                dbLogOrder.SaveChanges();

                MessageBox.Show("Спасибо" + "\n" + name.Text + "\n" + "за заказ");
                this.Close();
            }
        }
        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }

        private void SendOrderButton_MouseEnter(object sender, MouseEventArgs e)
        {
            SendOrderButton.Background = Brushes.Gray;
        }

        private void SendOrderButton_MouseLeave(object sender, MouseEventArgs e)
        {
            SendOrderButton.Background = Brushes.Transparent;
        }
    }
}
