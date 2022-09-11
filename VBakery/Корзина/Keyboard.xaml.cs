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
    /// Логика взаимодействия для Keyboard.xaml
    /// </summary>
    public partial class Keyboard : Window
    {
        
        public Keyboard()
        {
            InitializeComponent();
        }

        public void Label_MouseDown1(object sender, MouseButtonEventArgs e)
        {
            Supervisor supervisor = new();
            supervisor.inputIdForDeleteMenu1.Text += "1";
        }
        private void Label_MouseDown2(object sender, MouseButtonEventArgs e)
        {

        }
        private void Label_MouseDown3(object sender, MouseButtonEventArgs e)
        {

        }
        private void Label_MouseDown4(object sender, MouseButtonEventArgs e)
        {

        }
        private void Label_MouseDown5(object sender, MouseButtonEventArgs e)
        {

        }
        private void Label_MouseDown6(object sender, MouseButtonEventArgs e)
        {

        }
        private void Label_MouseDown7(object sender, MouseButtonEventArgs e)
        {

        }
        private void Label_MouseDown8(object sender, MouseButtonEventArgs e)
        {

        }
        private void Label_MouseDown9(object sender, MouseButtonEventArgs e)
        {

        }
        private void Label_MouseDown0(object sender, MouseButtonEventArgs e)
        {

        }
        private void Label_MouseDownClear(object sender, MouseButtonEventArgs e)
        {

        }
    }
}
