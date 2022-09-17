using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Input;

namespace VBakery
{
    public class WindowButtons : Window
    {
        public void ButtonClickPlus(object sender, MouseButtonEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }
        public void ButtonClickMinus(object sender, MouseButtonEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }
        public void ButtonClickClose(object sender, MouseButtonEventArgs e)
        {
            this.Close();
        }
    }
}
