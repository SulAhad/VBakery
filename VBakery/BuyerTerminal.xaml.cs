using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Windows;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Threading;

namespace VBakery
{
    public partial class BuyerTerminal : Window
    {
        static Supervisor supervisor = new();
        
        public string priceMan = supervisor.priceMan.Text;
        public string priceShoko = supervisor.priceShoko.Text;
        public string priceMussoviy = supervisor.priceMussoviy.Text;
        public string priceSvadebniy = supervisor.priceSvadebniy.Text;
        public string priceBirthday = supervisor.priceBirthday.Text;
        public string priceKids = supervisor.priceKids.Text;
        readonly PriceForMenusContext priceForMenusContext = new();
        public BuyerTerminal()
        {
            InitializeComponent();
            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandlerKeyDownEvent);
            CakeList();

            //PriceFruct.Content = priceForMenusContext.PriceForMenus.OrderBy(p => p.PriceFruct).ToString();

            ////PriceFruct.Content = priceFruct + " " + "руб.кг";
            //PriceMan.Content = priceMan + " " + "руб.кг";
            //PriceShoko.Content = priceShoko + " " + "руб.кг";
            //PriceMussoviy.Content = priceMussoviy + " " + "руб.кг";
            //PriceSvadebniy.Content = priceSvadebniy + " " + "руб.кг";
            //PriceBirthday.Content = priceBirthday + " " + "руб.кг";
            //PriceKids.Content = priceKids + " " + "руб.кг";
            
        }
        private void CakeList()
        {
            var CakeList = new List<Cake>
        {
            new Cake() { Name="Фруктовый торт", Price = 900},
            new Cake() { Name="Торт для мальчиков", Price = 900},
            //new Cake() { Name="Шоколадный торт", Price = 900},
            //new Cake() { Name="Муссовый торт", Price = 900},
            //new Cake() { Name="Свадебный торт", Price = 900},
            //new Cake() { Name="Торт на день рождения", Price = 900},
            //new Cake() { Name="Детский торт", Price = 900},
        };

            foreach (Cake theCake in CakeList)
            {
                FructCakeName.Content = theCake.Name;
                PriceFruct.Content = theCake.Price;
                ManCakeName.Content = theCake.Name;
                PriceMan.Content = theCake.Price;
                //Console.WriteLine(theGalaxy.Name + "  " + theGalaxy.MegaLightYears);
            }

            // Output:
            //  Tadpole  400
            //  Pinwheel  25
            //  Milky Way  0
            //  Andromeda  3
        }

        public class Cake
        {
            public string Name { get; set; }
            public int Price { get; set; }
        }
        private void HandlerKeyDownEvent(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    break;
                case Key.Escape:
                    Close();
                    break;

                default:
                    break;
            }
        }//Клавиатура
        public void VisibilityButton()
        {
            if(totalPrice.Text != "")
            {
                GoToOrder.Visibility = Visibility.Visible;
                VisibilityTotalPrice.Visibility = Visibility.Visible;
                ClickCancelOrder.Visibility = Visibility.Visible;
            }
            else
            {
                GoToOrder.Visibility = Visibility.Hidden;
                VisibilityTotalPrice.Visibility = Visibility.Hidden;
                ClickCancelOrder.Visibility = Visibility.Hidden;
            }
        }
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            nameTrash.Background = Brushes.Green;
            nameTrash.Content = "Корзина";
        }
        private void TrashNotifications()
        {
            nameTrash.Background = Brushes.LightCoral;
            nameTrash.Content = "Не выбран вес продукта!";
            DispatcherTimer dispatcherTimer = new();
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
            dispatcherTimer.Start();
        }
        private void AddFructToTrash(object sender, RoutedEventArgs e)
        {
            if (WeightFructTort.Text == "" || WeightFructTort.Text == null)
            {
                TrashNotifications();
            }
            else
            {
                double price = Convert.ToInt32(PriceFruct.Content);
                int weight = Convert.ToInt32(WeightFructTort.Text);
                if (WeightFructTort.Text == "")
                {
                    TrashNotifications();
                }
                else
                {
                    computeNums.Text += "+";
                    computeNums.Text += Convert.ToString(weight * price) + "\r\n";
                    TextAreaTrash.Text += "Фруктовый торт" + " " + WeightFructTort.Text + " " + "кг" + "\r\n";
                }
                totalPrice.Text = new DataTable().Compute(computeNums.Text, null).ToString().Replace(',', '.');
                VisibilityButton();
            } 
        }
        private void AddManToTrash(object sender, RoutedEventArgs e)
        {
            if(WeightManTort.Text == "" || WeightManTort.Text == null)
            {
                TrashNotifications();
            }
            int weight = Convert.ToInt32(WeightManTort.Text);
            if (WeightManTort.Text == "")
            {
                TrashNotifications();
            }
            else
            {
                computeNums.Text += "+";
                computeNums.Text += Convert.ToString(weight * 800) + "\r\n";
                TextAreaTrash.Text += "Торт для мальчиков" + " " + WeightManTort.Text + " " + "кг" + "\r\n";
            }
            totalPrice.Text = new DataTable().Compute(computeNums.Text, null).ToString().Replace(',', '.');
            VisibilityButton();
        }
        private void ButtonClickCancelOrder(object sender, RoutedEventArgs e)
        {
            computeNums.Text = "";
            totalPrice.Text = "";
            TextAreaTrash.Text = "";
            VisibilityButton();
        }
        private void AddShokoladToTrash(object sender, RoutedEventArgs e)
        {
            if(WeightShokoladTort.Text == "" || WeightShokoladTort.Text == null)
            {
                TrashNotifications();
            }
            else
            {
                int weight = Convert.ToInt32(WeightShokoladTort.Text);
                if (WeightShokoladTort.Text == "")
                {
                    TrashNotifications();
                }
                else
                {
                    computeNums.Text += "+";
                    computeNums.Text += Convert.ToString(weight * 800) + "\r\n";
                    TextAreaTrash.Text += "Шоколадный торт" + " " + WeightShokoladTort.Text + " " + "кг" + "\r\n";
                }
                totalPrice.Text = new DataTable().Compute(computeNums.Text, null).ToString().Replace(',', '.');
                VisibilityButton();
            }
        }
        private void AddMussoviyToTrash(object sender, RoutedEventArgs e)
        {
            if(WeightMussoviyTort.Text == "" || WeightMussoviyTort.Text == null)
            {
                TrashNotifications();
            }
            else
            {
                int weight = Convert.ToInt32(WeightMussoviyTort.Text);
                if (WeightMussoviyTort.Text == "")
                {
                    TrashNotifications();
                }
                else
                {
                    computeNums.Text += "+";
                    computeNums.Text += Convert.ToString(weight * 800) + "\r\n";
                    TextAreaTrash.Text += "Муссовый торт" + " " + WeightMussoviyTort.Text + " " + "кг" + "\r\n";
                }
                totalPrice.Text = new DataTable().Compute(computeNums.Text, null).ToString().Replace(',', '.');
                VisibilityButton();
            }
        }
        private void AddSvadebniyToTrash(object sender, RoutedEventArgs e)
        {
            if(WeightSvadebniyTort.Text == "" || WeightSvadebniyTort.Text == null)
            {
                TrashNotifications();
            }
            else
            {
                int weight = Convert.ToInt32(WeightSvadebniyTort.Text);
                if (WeightSvadebniyTort.Text == "")
                {
                    TrashNotifications();
                }
                else
                {
                    computeNums.Text += "+";
                    computeNums.Text += Convert.ToString(weight * 800) + "\r\n";
                    TextAreaTrash.Text += "Свадебный торт" + " " + WeightSvadebniyTort.Text + " " + "кг" + "\r\n";
                }
                totalPrice.Text = new DataTable().Compute(computeNums.Text, null).ToString().Replace(',', '.');
                VisibilityButton();
            }
        }
        private void AddBirthdayToTrash(object sender, RoutedEventArgs e)
        {
            if(WeightBirthdayTort.Text == "" || WeightBirthdayTort.Text == null)
            {
                TrashNotifications();
            }
            else
            {
                int weight = Convert.ToInt32(WeightBirthdayTort.Text);
                if (WeightBirthdayTort.Text == "")
                {
                    TrashNotifications();
                }
                else
                {
                    computeNums.Text += "+";
                    computeNums.Text += Convert.ToString(weight * 800) + "\r\n";
                    TextAreaTrash.Text += "Торт для мальчиков" + " " + WeightBirthdayTort.Text + " " + "кг" + "\r\n";
                }
                totalPrice.Text = new DataTable().Compute(computeNums.Text, null).ToString().Replace(',', '.');
                VisibilityButton();
            }
        }
        private void AddKidsToTrash(object sender, RoutedEventArgs e)
        {
            if(WeightKidsTort.Text == "" || WeightKidsTort.Text == null)
            {
                TrashNotifications();
            }
            else
            {
                int weight = Convert.ToInt32(WeightKidsTort.Text);
                if (WeightKidsTort.Text == "")
                {
                    TrashNotifications();
                }
                else
                {
                    computeNums.Text += "+";
                    computeNums.Text += Convert.ToString(weight * 800) + "\r\n";
                    TextAreaTrash.Text += "Торт для мальчиков" + " " + WeightKidsTort.Text + " " + "кг" + "\r\n";
                }
                totalPrice.Text = new DataTable().Compute(computeNums.Text, null).ToString().Replace(',', '.');
                VisibilityButton();
            }
        }

        //private void GoToOrder_MouseDown(object sender, MouseButtonEventArgs e)
        //{
        //    BuyerToOrder buyerToOrder = new();
        //    buyerToOrder.ShowDialog();
        //}
        private void MouseDownGoToOrder(object sender, RoutedEventArgs e)
        {
            BuyerToOrder buyerToOrder = new();
            buyerToOrder.InputOrderArea.Text = TextAreaTrash.Text;
            buyerToOrder.TotalPrice.Text = totalPrice.Text;
            buyerToOrder.ShowDialog();

        }
        private void Image_MouseEnterFruct(object sender, MouseEventArgs e)
        {
            //ImageFruct.Width = 250;
            //ImageFruct.Height = 250;
            //ContentFruct.FontSize = 22;
            ImageFruct.Background = Brushes.Pink;
        }
        private void Image_MouseLeaveFruct(object sender, MouseEventArgs e)
        {
            //ImageFruct.Width = 200;
            //ImageFruct.Height = 200;
            //ContentFruct.FontSize = 19;
            ImageFruct.Background = Brushes.White;
        }
        private void Image_MouseEnterMan(object sender, MouseEventArgs e)
        {
            //ImageMan.Width = 250;
            //ImageMan.Height = 250;
            //ContentMan.FontSize = 22;
            ImageMan.Background = Brushes.Pink;
        }
        private void Image_MouseLeaveMan(object sender, MouseEventArgs e)
        {
            //ImageMan.Width = 200;
            //ImageMan.Height = 200;
            //ContentMan.FontSize = 19;
            ImageMan.Background = Brushes.White;
        }
        private void Image_MouseEnterShokolad(object sender, MouseEventArgs e)
        {
            ImageShokolad.Background = Brushes.Chocolate;
        }
        private void Image_MouseLeaveShokolad(object sender, MouseEventArgs e)
        {
            ImageShokolad.Background = Brushes.White;
        }
        private void Image_MouseEnterMusoviy(object sender, MouseEventArgs e)
        {
            ImageMusoviy.Background = Brushes.Orchid;
        }
        private void Image_MouseLeaveMusoviy(object sender, MouseEventArgs e)
        {
            ImageMusoviy.Background = Brushes.White;
        }
        private void Image_MouseEnterSvadebniy(object sender, MouseEventArgs e)
        {
            ImageSvadebniy.Background = Brushes.FloralWhite;
        }
        private void Image_MouseLeaveSvadebniy(object sender, MouseEventArgs e)
        {
            ImageSvadebniy.Background = Brushes.White;
        }
        private void Image_MouseEnterBirthDay(object sender, MouseEventArgs e)
        {
            ImageBirthDay.Background = Brushes.SkyBlue;
        }
        private void Image_MouseLeaveBirthDay(object sender, MouseEventArgs e)
        {
            ImageBirthDay.Background = Brushes.White;
        }
        private void Image_MouseEnterKids(object sender, MouseEventArgs e)
        {
            ImageKids.Background = Brushes.Khaki;
        }
        private void Image_MouseLeaveKids(object sender, MouseEventArgs e)
        {
            ImageKids.Background = Brushes.White;
        }
        private void GoToOrder_MouseEnter(object sender, MouseEventArgs e)
        {
            GoToOrder.Background = Brushes.White;
            GoToOrder.Foreground = Brushes.Green;
        }
        private void GoToOrder_MouseLeave(object sender, MouseEventArgs e)
        {
            GoToOrder.Background = Brushes.Green;
            GoToOrder.Foreground = Brushes.White;
        }
        private void ClickCancelOrder_MouseEnter(object sender, MouseEventArgs e)
        {
            ClickCancelOrder.Background = Brushes.White;
            ClickCancelOrder.Foreground = Brushes.Red;
        }
        private void ClickCancelOrder_MouseLeave(object sender, MouseEventArgs e)
        {
            ClickCancelOrder.Background = Brushes.Red;
            ClickCancelOrder.Foreground = Brushes.White;
        }
    }
}
