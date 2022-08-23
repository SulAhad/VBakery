using System;
using System.Windows.Input;

namespace VBakery
{
    public abstract class Keys
    {
        private void HandlerKeyDownEvent(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    break;
                case Key.Escape:
                    this.Close();
                    break;
                default:
                    break;
            }
        }//Клавиатура

        public static explicit operator KeyEventHandler(Keys v)
        {
            throw new NotImplementedException();
        }

        private void Close()
        {
            this.Close();
        }
    }
}
