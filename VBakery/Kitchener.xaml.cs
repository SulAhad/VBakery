using Microsoft.AspNetCore.Mvc.ModelBinding;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using static VBakery.BuyerWindow;

namespace VBakery
{
    public partial class Kitchener : Window
    {

        public Kitchener()
        {
            InitializeComponent();
            ApplicationContext db = new ApplicationContext();
            var users = db.Orders.ToList();
            foreach (Order u in users)
            {
                string ID = $"{u.Id}.";
                string WEIGHT = $"{u.Weight}";
                string VISUAL = $"- {u.Visual}";
                string ADDRESS = $"- {u.Address }";
                string COMM = $"- {u.Comm}";

                DataGridTextColumn dataGridTextColumn = new();

                dataGridTextColumn.Binding = new Binding("id");
                dataGridTextColumn.Header = ID;

                dataGridTextColumn.Binding = new Binding("KitchenWeight");
                dataGridTextColumn.Header = WEIGHT;




                //GridViewColumn gridViewColumn = new GridViewColumn();
                //gridViewColumn.DisplayMemberBinding = new Binding("id");
                //gridViewColumn.Header = ID;

                //Kitchener_Order.Text += text + "\r\n";
            }
            
        }
        private void Button_Click(object sender, RoutedEventArgs e)
        {
            
            MainWindow mainWindow = new();
            mainWindow.Show();
            Close();
        }
    }
}
