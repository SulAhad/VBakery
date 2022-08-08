using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Windows;



namespace VBakery
{
    public partial class OrderAcceptWindow : Window
    {
        public OrderAcceptWindow()
        {
            InitializeComponent();
            using OrderForBuyersContext db = new();
            //OrderArea.ItemsSource = (System.Collections.IEnumerable)db.OrderForBuyers.FirstOrDefault();

        }
    }
}
