using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace VBakery
{
    public class WindowStateButtons
    {
        private WindowState WindowState;

        public void CloseWindow()
        {
            Close();
        }

        private void Close()
        {
            this.Close();
        }

        public void MinimizeWindow()
        {
            this.WindowState = WindowState.Minimized;
        }
        public void MaximizeWindow()
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
    }
}
