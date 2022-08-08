using System;
using System.Data;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Threading;

namespace VBakery
{
    public partial class PaymasterCalculator : Window
    {
        private int cursor = 1;
        private char cursorView = '|';
        private byte n;

        public PaymasterCalculator()
        {
            InitializeComponent();

            cursor = 1;

            foreach (UIElement item in mainElement.Children)
            {
                if (item is Button button)
                {
                    button.Click += ButtonClick;
                }
            }

            textBox.Text = "0|";

            DispatcherTimer timer = new();
            timer.Interval = TimeSpan.FromSeconds(1);
            timer.Tick += ChangeCursor;
            timer.Start();
        }
        private void ChangeCursor(object sender, EventArgs e)
        {
            string str = GetTextBox();
            if (cursorView == '|')
            {
                cursorView = ' ';
            }
            else
            {
                cursorView = '|';
            }
            SetTextBox(str);
        }
        private void CursorMove(bool direction)
        {
            if (direction)
            {
                if (cursor < textBox.Text.Length - 1)
                {
                    cursor++;
                }
            }
            else
            {
                if (cursor > 0)
                {
                    cursor--;
                }
            }
            SetTextBox(GetTextBox());
        }
        private void SetTextBox(string input)
        {
            textBox.Text = input.Insert(cursor, cursorView.ToString());
        }
        private string GetTextBox()
        {
            return textBox.Text.Replace(cursorView.ToString(), "");
        }
        private void ButtonClick(object sender, RoutedEventArgs e)
        {
            string infoKey = ((Button)sender).Content.ToString();
            switch (infoKey)
            {
                case "+":
                    AddOperator(infoKey);
                    break;
                case "-":
                    AddOperator(infoKey);
                    break;
                case "*":
                    AddOperator(infoKey);
                    break;
                case "/":
                    AddOperator(infoKey);
                    break;
                case ",":
                    AddDivisor();
                    break;
                case "AC":
                    textBox.Text = "0";
                    cursor = 1;
                    break;
                case "+/-":
                    SignChange();
                    break;
                case "<":
                    RemoveLastSymbol();
                    break;
                case "=":
                    Calculation();
                    break;
                case "<-":
                    CursorMove(false);
                    break;
                case "->":
                    CursorMove(true);
                    break;
                case "(":
                    Bracket(true);
                    break;
                case ")":
                    Bracket(false);
                    break;
                case "Close":
                    Application.Current.Shutdown();
                    break;
                default:
                    AddSign(infoKey);
                    break;
            }
        }
        private void AddOperator(string input)
        {
            string str = GetTextBox();
            if (str[cursor - 1] == '.')
            {
                str = str.Remove(cursor - 1, 1);
                str = str.Insert(cursor - 1, input);
            }
            else
            {
                if ((byte)str[cursor - 1] >= 48 && (byte)str[cursor - 1] <= 57)
                {
                    str = str.Insert(cursor, input);
                    cursor++;
                }
            }
            SetTextBox(str);
        }
        private void SignChange()
        {
            string str = GetTextBox();
            if (double.TryParse(str.Replace('.', ','), out double number))
            {
                if (number > 0)
                {
                    cursor++;
                }
                else
                {
                    cursor--;
                }
                number *= -1;
                str = number.ToString().Replace(',', '.');
            }
            SetTextBox(str);
        }
        private void RemoveLastSymbol()
        {
            string str = GetTextBox();
            if (str.Length == 1)
            {
                str = "0";
            }
            else
            {
                if (cursor > 0)
                {
                    str = str.Remove(cursor - 1, 1);
                    cursor--;
                }
            }
            n = 0;
            foreach (char item in str)
            {
                if (item == '(')
                {
                    n++;
                }
                if (item == ')')
                {
                    n--;
                }
            }
            SetTextBox(str);
        }
        private void AddDivisor()
        {
            string str = GetTextBox();
            if ((byte)str[cursor - 1] >= 48 && (byte)str[cursor - 1] <= 57)
            {
                str = str.Insert(cursor, ".");
                cursor++;
            }
            SetTextBox(str);
        }
        private void Calculation()
        {
            string str = GetTextBox();
            if ((byte)str[^1] >= 48 && (byte)str[^1] <= 57)
            {
                str = new DataTable().Compute(str, null).ToString().Replace(',', '.');
            }
            cursor = str.Length;
            SetTextBox(str);
        }
        private void AddSign(string input)
        {
            string str = GetTextBox();
            if (str == "0" && cursor != 0)
            {
                str = input;
            }
            else
            {
                str = str.Insert(cursor, input);
                cursor++;
            }
            SetTextBox(str);
        }
        private void Bracket(bool isStart)
        {
            string str = GetTextBox();
            if (isStart)
            {
                //Открытие скобки
                if (n <= 0)
                {
                    if (cursor == 0 || cursor == str.Length)
                    {
                        if (cursor == str.Length)
                        {
                            if (((byte)str[cursor - 1] < 48 || (byte)str[cursor - 1] > 57) && (byte)str[cursor - 1] != 46)
                            {
                                str = str.Insert(cursor, "(");
                                cursor++;
                            }
                        }
                        else
                        {
                            str = str.Insert(cursor, "(");
                            cursor++;
                        }
                    }
                    else
                    {
                        if ((((byte)str[cursor - 1] < 48 || (byte)str[cursor - 1] > 57) && (byte)str[cursor - 1] != 46) && ((byte)str[cursor] >= 48 && (byte)str[cursor] <= 57))
                        {
                            str = str.Insert(cursor, "(");
                            cursor++;
                        }
                    }
                }
            }
            else
            {
                //Закрытие скобки
                if (n > 0)
                {
                    if (cursor == 0 || cursor == str.Length)
                    {
                        if (cursor == str.Length)
                        {
                            if ((byte)str[cursor - 1] >= 48 && (byte)str[cursor - 1] <= 57)
                            {
                                str = str.Insert(cursor, ")");
                                cursor++;
                            }
                        }
                    }
                    else
                    {
                        if ((byte)str[cursor - 1] >= 48 && (byte)str[cursor - 1] <= 57 && (((byte)str[cursor] != 46) && ((byte)str[cursor] < 48) || ((byte)str[cursor] > 57)))
                        {
                            str = str.Insert(cursor, ")");
                            cursor++;
                        }
                    }
                }
            }
            n = 0;
            foreach (char item in str)
            {
                if (item == '(')
                {
                    n++;
                }
                if (item == ')')
                {
                    n--;
                }
            }
            SetTextBox(str);
        }
        private void ButtonClick_back(object sender, RoutedEventArgs e)
        {
            Close();
        }
        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
