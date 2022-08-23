using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace VBakery
{
    /// <summary>
    /// Логика взаимодействия для Orders.xaml
    /// </summary>
    public partial class Orders : Window
    {
        readonly OrderForBuyersContext orderForBuyersContext = new();
        public Orders()
        {
            InitializeComponent();
            OrdertList.ItemsSource = orderForBuyersContext.OrderForBuyers.ToList();
            DownTray.Content = "Обновлено --" + DateTime.Now.ToString();
        }
        private void MouseDownRefreshRecepts(object sender, MouseButtonEventArgs e)
        {
            OrdertList.ItemsSource = orderForBuyersContext.OrderForBuyers.ToList();
            DownTray.Content = "Обновлено --" + DateTime.Now.ToString();
        }
        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
