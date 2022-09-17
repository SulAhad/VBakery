using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;

namespace VBakery
{
    public partial class Orders : Window
    {
        public Orders()
        {
            InitializeComponent();
            OrderForBuyersContext orderForBuyersContext = new();
            OrdertList.ItemsSource = orderForBuyersContext.OrderForBuyers.ToList();
            DownTray.Content = "Обновлено --" + DateTime.Now.ToString();
        }
        private void MouseDownRefreshRecepts(object sender, MouseButtonEventArgs e)
        {
            OrderForBuyersContext orderForBuyersContext = new();
            OrdertList.ItemsSource = orderForBuyersContext.OrderForBuyers.ToList();
            DownTray.Content = "Обновлено --" + DateTime.Now.ToString();
        }
        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
