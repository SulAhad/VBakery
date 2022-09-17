using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
namespace VBakery
{
    public partial class Recepts : Window
    {
        public Recepts()
        {
            InitializeComponent();
            using RecipesContext db = new();
            ReceptList.ItemsSource = db.Recipes.ToList();
            DownTrayKitchenerRecepts.Content = "Обновлено --" + DateTime.Now.ToString();
        }
        private void MouseDownRefreshRecepts(object sender, MouseButtonEventArgs e)
        {
            using RecipesContext db = new();
            ReceptList.ItemsSource = db.Recipes.ToList();
            DownTrayKitchenerRecepts.Content = "Обновлено --" + DateTime.Now.ToString();
        }
        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            Close();
        }
    }
}
