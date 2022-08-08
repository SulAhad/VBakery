using Microsoft.EntityFrameworkCore;
using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using VBakery.DB;

namespace VBakery
{
    public partial class BuyerToOrder : Window
    {
        public BuyerToOrder()
        {
            InitializeComponent();
            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandlerKeyDownEvent);
            CommToolTip.ToolTip = "Необязательно";
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
            weight.ToolTip = null;
            weight.Background = Brushes.AliceBlue;
            nameProduct.ToolTip = null;
            nameProduct.Background = Brushes.AliceBlue;
            address.ToolTip = null;
            address.Background = Brushes.AliceBlue;
            dateOrder.ToolTip = null;
            dateOrder.Background = Brushes.AliceBlue;

            if (name.Text.Length <= 2)
            {
                name.ToolTip = "Не ввели имя";
                name.Background = Brushes.LightCoral;
                flag = false;
            }
            if (mobile.Text.Length != 11 )
            {
                mobile.ToolTip = "Введите номер телефона";
                mobile.Background = Brushes.LightCoral;
                flag = false;
            }
            if (weight.Text == "")
            {
                weight.ToolTip = "Не ввели вес";
                weight.Background = Brushes.LightCoral;
                flag = false;
            }
            if (nameProduct.Text == "")
            {
                nameProduct.ToolTip = "Не ввели название продукта";
                nameProduct.Background = Brushes.LightCoral;
                flag = false;
            }
            if (address.Text == "")
            {
                address.ToolTip = "Не ввели адресс";
                address.Background = Brushes.LightCoral;
                flag = false;
            }
            if (dateOrder.Text == "")
            {
                dateOrder.ToolTip = "Выберите дату доставки";
                dateOrder.Background = Brushes.LightCoral;
            }
            if(flag)
            {
                OrderForBuyersContext db = new();
                OrderForBuyer tim = new OrderForBuyer
                {
                    name = name.Text,
                    mobile = mobile.Text,
                    weight = weight.Text,
                    nameProduct = nameProduct.Text,
                    address = address.Text,
                    comm = comm.Text,
                    data = dateOrder.Text,
                    dateTime = DateTime.Now.ToString()
                };
                db.OrderForBuyers.Add(tim);
                db.SaveChanges();
                
                MessageBox.Show("Спасибо" + "\n" + name.Text + "\n" + "за заказ");
                Buyer buyer = new();
                buyer.Show();
                this.Close();
            }
        }
        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
