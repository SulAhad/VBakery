using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Controls.Primitives;
using System.Windows.Input;
using System.Windows.Media;
using VBakery.DB;
using VBakery.MenuDB;
namespace VBakery
{
    public partial class Supervisor : Window
    {
        public Supervisor()
        {
            InitializeComponent();
            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandlerKeyDownEvent);
            MethodGetDataRecept();//Повар
            StartWork();//Кассир
            EndWork();//Кассир
            UpdateOrderForBuyer();
            UpdateChatIfClickSend();
            using OrderForBuyersContext db = new();
            System.Int32 customerCount = db.OrderForBuyers.Count();
            CountSales.Content = Convert.ToString(customerCount);
            System.Int32 BuyerCount = db.OrderForBuyers.Count();
            VisitBuyer.Text = Convert.ToString(BuyerCount);
            UpdateDBBuyer();
            UpdateFirstCourseList();
        }
        public void UpdateChatIfClickSend()
        {
            using MessagesContext db = new();
            Chat.ItemsSource = db.Messages.ToList();
        }
        public void ClickButtonSendMessage(object sender, RoutedEventArgs e)
        {
            if(MessageUser.Text != "")
            {
                using MessagesContext db = new MessagesContext();
                Message toms = new Message
                {
                    User = "Директор",
                    TextUser = MessageUser.Text,
                    Time = DateTime.Now.ToString()
                };
                db.Messages.Add(toms);
                db.SaveChanges();
                UpdateChatIfClickSend();
                MessageUser.Text = "";
                downTrayChatRoom.Background = Brushes.AliceBlue;
                downTrayChatRoom.Content = "Обновлено:" + DateTime.Now.ToString();
            }
            else
            {
                downTrayChatRoom.Content = "Не ввели сообщение!";
                downTrayChatRoom.Background = Brushes.LightCoral;
            }
            
        }
        public void UpdateDBBuyer()
        {
            using MenuArea1Context db1 = new();
            Menu1.ItemsSource = db1.Menu1.ToList();

            using MenuArea2Context db2 = new();
            Menu2.ItemsSource = db2.Menu2.ToList();

            using MenuArea3Context db3 = new();
            Menu3.ItemsSource = db3.Menu3.ToList();

            using MenuArea4Context db4 = new();
            Menu4.ItemsSource = db4.Menu4.ToList();
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
        public void UpdateOrderForBuyer()
        {
            using OrderForBuyersContext db = new();
            UserOrder.ItemsSource = db.OrderForBuyers.ToArray();
        }
        //Кассир
        public void StartWork()
        {
            using TimerStartsContext db = new();
            ListBeginTime.ItemsSource = db.TimerStarts.ToList();
        }//Кассир
        public void EndWork()
        {
            using TimerEndsContext db = new();
            ListEndTime.ItemsSource = db.TimerEnds.ToList();
        }//Кассир
        private void GoBackToHome(object sender, RoutedEventArgs e)
        {
            MessageBoxResult result = MessageBox.Show(
            "Вы точно хотите выйти?",
            "Сообщение",
            MessageBoxButton.YesNo);
            if (result == MessageBoxResult.Yes)
            {
                MainWindow mainWindow = new();
                mainWindow.Show();
                this.Close();
            }
        }//Кнопка выхода
        private void ButtonClickPlus(object sender, RoutedEventArgs e)
        {
            if (this.WindowState == WindowState.Maximized)
            {
                this.WindowState = WindowState.Normal;
            }
            else
            {
                this.WindowState = WindowState.Maximized;
            }
        }//Кнопка расширить окно
        private void ButtonClickMinus(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }//Кнопка свернуть окно
        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }//Кнопка закрыть окно
        //Повар
        private void AddNewRecipe(object sender, RoutedEventArgs e)
        {
            if (nameRecipe.Text == "")
            {
                DownTray.Content = "Не ввели название";
                DownTray.Background = Brushes.LightCoral;
            }
            if (RecipeArea.Text == "")
            {
                DownTray.Content = "Не ввели состав";
                DownTray.Background = Brushes.LightCoral;
            }
            if (Price.Text == "")
            {
                DownTray.Content = "Не ввели цену";
                DownTray.Background = Brushes.LightCoral;
            }
            else
            {
                using RecipesContext db = new RecipesContext();
                Recipe toms = new Recipe 
                {
                NameRecipe = nameRecipe.Text,
                TextRecipe = RecipeArea.Text,
                PriceRecipe = Convert.ToInt32(Price.Text)
                };
                db.Recipes.Add(toms);
                db.SaveChanges();
                DownTray.Content = "Добавлено в базу данных";
                DownTray.Background = Brushes.LightGreen;
                nameRecipe.Text = "";
                RecipeArea.Text = "";
                Price.Text = "";
            }
        }//Повар
        private void GetDataRecept(object sender, RoutedEventArgs e)
        {
            MethodGetDataRecept();
            DownTrayDeleteRecept.Content = "Обновлено:" + DateTime.Now.ToString();
            DownTrayDeleteRecept.Background = Brushes.LightGreen;
        }//Повар
        private void MethodGetDataRecept()
        {
            using RecipesContext db = new();
            ReceptList.ItemsSource = db.Recipes.ToList();
            DownTrayDeleteRecept.Content = "Обновлено:" + DateTime.Now.ToString();
            DownTrayDeleteRecept.Background = Brushes.LightGreen;
        }//Повар
        private void ButtonClickDeleteFirstID(object sender, RoutedEventArgs e)
        {
            using RecipesContext db = new RecipesContext();
            Recipe recipe = new();
            var item = db.Recipes.FirstOrDefault();
            if (item != null)
            {
                db.Recipes.Remove(item);
                db.SaveChanges();
                MethodGetDataRecept();
                DownTrayDeleteRecept.Background = Brushes.LightGreen;
                DownTrayDeleteRecept.Content = "Удалена первая запись";
            }
        }//Кнопка удалить первую ID Кухня
        private void ButtonClickDeleteLastID(object sender, RoutedEventArgs e)
        {
            //using ApplicationContext db = new ApplicationContext();
            //Recipe recipe = new();
            //// добавляем их в бд
            //IEnumerable<string> auto = Recipe.OrderBy((s => s), db);
            //var item = db.Recipes.LastOrDefault();



            //if (item == null)
            //{
            //    db.Recipes.Remove(item);
            //    db.SaveChanges();
            //    MethodGetDataRecept();
            //    DownTrayDeleteRecept.Background = Brushes.LightGreen;
            //    DownTrayDeleteRecept.Content = "Удалена последняя запись";
            //}
        }//Кнопка удалить последнюю ID Кухня
        private void RemoveLastData(object sender, RoutedEventArgs e)
        {
            using RecipesContext db = new RecipesContext();
            Recipe recipe = new();
            if (SetId.Text == "")
            {
                DownTrayDeleteRecept.Content = "Не ввели номер!";
                DownTrayDeleteRecept.Background = Brushes.LightCoral;
            }
            else
            {
                SetId.Text = SetId.Text.Trim();
                int key = Convert.ToInt32(SetId.Text.Trim());
                var item = db.Recipes.Find(key);
                if (item != null)
                {

                    db.Recipes.Remove(item);
                    db.SaveChanges();
                    MethodGetDataRecept();
                    DownTrayDeleteRecept.Background = Brushes.LightGreen;
                    DownTrayDeleteRecept.Content = "Удалена запись --" + SetId.Text;
                    SetId.Text = "";
                }
                else
                {
                    MessageBox.Show("Введена некорреткная цифра!");
                }
            }
        }
        //Покупатель
        private void RemoveLastOrder(object sender, RoutedEventArgs e)
        {
            using OrderForBuyersContext db = new OrderForBuyersContext();
            
            if (SetIdBuyer.Text == "")
            {
                DownTrayBuyerOrders.Content = "Не ввели номер!";
                DownTrayBuyerOrders.Background = Brushes.LightCoral;
            }
            else
            {
                SetIdBuyer.Text = SetIdBuyer.Text.Trim();
                int key = Convert.ToInt32(SetIdBuyer.Text.Trim());
                var item = db.OrderForBuyers.Find(key);
                
                if (item != null)
                {
                    db.OrderForBuyers.Remove(item);
                    db.SaveChanges();
                    DownTrayBuyerOrders.Background = Brushes.LightGreen;
                    DownTrayBuyerOrders.Content = "Удалена запись --" + SetIdBuyer.Text;
                    SetIdBuyer.Text = "";
                    UserOrder.ItemsSource = db.OrderForBuyers.ToList();
                    System.Int32 BuyerCount = db.OrderForBuyers.Count();
                    VisitBuyer.Text = Convert.ToString(BuyerCount);
                }
                else
                {
                    MessageBox.Show("Введена некорреткная цифра!");
                }
            }
        }
        private void GetDataBuyer(object sender, RoutedEventArgs e)
        {
            UpdateOrderForBuyer();
            using OrderForBuyersContext db = new();
            System.Int32 BuyerCount = db.OrderForBuyers.Count();
            VisitBuyer.Text = Convert.ToString(BuyerCount);
            DownTrayBuyerOrders.Content = "Обновлено --" + DateTime.Now.ToString();
        }//Обновить данные покупатель
        //Касса
        private void ButtonDeleteTimeBegin(object sender, RoutedEventArgs e)
        {
            using (TimerStartsContext db = new())
            {
                TimerStart user = db.TimerStarts.FirstOrDefault();
                if (user != null)
                {
                    db.Remove(user);
                    db.SaveChanges();
                    StartWork();
                }
            }
        }//Касса начало работы
        private void ButtonDeleteTimeEnd(object sender, RoutedEventArgs e)
        {
            using (TimerEndsContext db = new())
            {
                TimerEnd user = db.TimerEnds.FirstOrDefault();
                if (user != null)
                {
                    db.Remove(user);
                    db.SaveChanges();
                    EndWork();
                }
            }
        }//Касса конец работы
        private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ScrollBar scrollBar = new();
            scrollBar.Value = numeric.Value;
            SetIdBuyer.Text = Convert.ToString(numeric.Value);
        }
        private void ScrollBar_ValueChanged1(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ScrollBar scrollBar = new();
            scrollBar.Value = numeric1.Value;
            NumericId.Text = Convert.ToString(numeric1.Value);

        }
        private void ScrollBar_ValueChangedChat(object sender, RoutedPropertyChangedEventArgs<double> e)
        {
            ScrollBar scrollBar = new();
            scrollBar.Value = numericChat.Value;
            SetIdChat.Text = Convert.ToString(numericChat.Value);
        }
        private void ButtonClickTextBlock(object sender, RoutedEventArgs e)
        {
            ListBox contentList = new ListBox();
            using OrderForBuyersContext db = new();

            TextBox textBox = new TextBox() { Text = "{Binding = Id }", Width = 50, Height = 50 };
            TextBox textBox1 = new TextBox() { Text = "{Binding = name }", Width = 50, Height = 50 };
            TextBox textBox2 = new TextBox() { Text = "{Binding = mobile }", Width = 50, Height = 50 };
            TextBox textBox3 = new TextBox() { Text = "{Binding = weight }", Width = 50, Height = 50 };
            TextBox textBox4 = new TextBox() { Text = "{Binding = nameProduct }", Width = 50, Height = 50 };
            TextBox textBox5 = new TextBox() { Text = "{Binding = address }", Width = 50, Height = 50 };
            TextBox textBox6 = new TextBox() { Text = "{Binding = comm }", Width = 50, Height = 50 };
            TextBox textBox7 = new TextBox() { Text = "{Binding = data }", Width = 50, Height = 50 };
            TextBox textBox8 = new TextBox() { Text = "{Binding = dateTime }", Width = 50, Height = 50 };
            TextBox textBox9 = new TextBox() { Text = "{Binding = status }", Width = 50, Height = 50 };

            //using OrderForBuyersContext db = new();//Кассир
            //UserOrder.ItemsSource = db.OrderForBuyers.ToList();
            contentList.ItemsSource = db.OrderForBuyers.ToList();

            contentList.Items.Add(new TextBox()
            {
                Text = "rtbrtbrt"

            });



            //TextBlock label = new TextBlock();
            //label.Text = "Имя файла";
            //label.VerticalAlignment = VerticalAlignment.Top;
            //label.Width = 100;
            //Add(label);


        }
        public void DoNotHaveName()
        {
            downTrayBuyer.Content = "Не ввели название";
            downTrayBuyer.Background = Brushes.LightCoral;
        }
        public void DoNotHaveConsist()
        {
            downTrayBuyer.Content = "Не ввели состав";
            downTrayBuyer.Background = Brushes.LightCoral;
        }
        public void DoNotHavePrice()
        {
            downTrayBuyer.Content = "Не ввели состав";
            downTrayBuyer.Background = Brushes.LightCoral;
        }
        public void AddTextToTray()
        {
            downTrayBuyer.Content = "Добавлено в базу данных";
            downTrayBuyer.Background = Brushes.LightGreen;
        }
        private void ButtonClickChangeArea1(object sender, RoutedEventArgs e)
        {
            if (changeAreaName1.Text == "")
            {
                DoNotHaveName();
            }
            if (changeAreaConsist1.Text == "")
            {
                DoNotHaveConsist();
            }
            if (changeAreaPrice1.Text == "")
            {
                DoNotHavePrice();
            }
            else
            {
                using MenuArea1Context db = new();
                MenuArea1 toms = new MenuArea1
                {
                    dbAreaName1 = changeAreaName1.Text,
                    dbAreaConsist1 = changeAreaConsist1.Text,
                    dbAreaPrice1 = changeAreaPrice1.Text
                };
                db.Menu1.Update(toms);
                db.SaveChanges();
                AddTextToTray();
                changeAreaName1.Text = "";
                changeAreaConsist1.Text = "";
                changeAreaPrice1.Text = "";
                UpdateDBBuyer();
            }
        }
        private void ButtonClickChangeArea2(object sender, RoutedEventArgs e)
        {
            if (changeAreaName2.Text == "")
            {
                DoNotHaveName();
            }
            if (changeAreaConsist2.Text == "")
            {
                DoNotHaveConsist();
            }
            if (changeAreaPrice2.Text == "")
            {
                DoNotHavePrice();
            }
            else
            {
                using MenuArea2Context db = new();
                MenuArea2 toms = new MenuArea2
                {
                    dbAreaName2 = changeAreaName2.Text,
                    dbAreaConsist2 = changeAreaConsist2.Text,
                    dbAreaPrice2 = changeAreaPrice2.Text
                };
                db.Menu2.Update(toms);
                db.SaveChanges();
                AddTextToTray();
                changeAreaName2.Text = "";
                changeAreaConsist2.Text = "";
                changeAreaPrice2.Text = "";
                UpdateDBBuyer();
            }
        }
        private void ButtonClickChangeArea3(object sender, RoutedEventArgs e)
        {
            if (changeAreaName3.Text == "")
            {
                DoNotHaveName();
            }
            if (changeAreaConsist3.Text == "")
            {
                DoNotHaveConsist();
            }
            if (changeAreaPrice3.Text == "")
            {
                DoNotHavePrice();
            }
            else
            {
                using MenuArea3Context db = new();
                MenuArea3 toms = new MenuArea3
                {
                    dbAreaName3 = changeAreaName3.Text,
                    dbAreaConsist3 = changeAreaConsist3.Text,
                    dbAreaPrice3 = changeAreaPrice3.Text
                };
                db.Menu3.Update(toms);
                db.SaveChanges();
                AddTextToTray();
                changeAreaName3.Text = "";
                changeAreaConsist3.Text = "";
                changeAreaPrice3.Text = "";
                UpdateDBBuyer();
            }
        }
        private void ButtonClickChangeArea4(object sender, RoutedEventArgs e)
        {
            if (changeAreaName4.Text == "")
            {
                DoNotHaveName();
            }
            if (changeAreaConsist4.Text == "")
            {
                DoNotHaveConsist();
            }
            if (changeAreaPrice4.Text == "")
            {
                DoNotHavePrice();
            }
            else
            {
                using MenuArea4Context db = new();
                MenuArea4 toms = new MenuArea4
                {
                    dbAreaName4 = changeAreaName4.Text,
                    dbAreaConsist4 = changeAreaConsist4.Text,
                    dbAreaPrice4 = changeAreaPrice4.Text
                };
                db.Menu4.Update(toms);
                db.SaveChanges();
                AddTextToTray();
                changeAreaName4.Text = "";
                changeAreaConsist4.Text = "";
                changeAreaPrice4.Text = "";
                UpdateDBBuyer();
            }
        }
        private void ButtonClickUpdate(object sender, RoutedEventArgs e)
        {
            UpdateDBBuyer();
        }
        private void ButtonClickDeleteMenu1(object sender, RoutedEventArgs e)
        {
            using (MenuArea1Context db = new())
            {
                MenuArea1 menu1 = db.Menu1.FirstOrDefault();
                if (menu1 != null)
                {
                    db.Remove(menu1);
                    db.SaveChanges();
                    UpdateDBBuyer();
                }
            }
        }
        private void ButtonClickDeleteMenu2(object sender, RoutedEventArgs e)
        {
            using (MenuArea2Context db = new())
            {
                MenuArea2 menu2 = db.Menu2.FirstOrDefault();
                if (menu2 != null)
                {
                    db.Remove(menu2);
                    db.SaveChanges();
                    UpdateDBBuyer();
                }
            }
        }
        private void ButtonClickDeleteMenu3(object sender, RoutedEventArgs e)
        {
            using (MenuArea3Context db = new())
            {
                MenuArea3 menu3 = db.Menu3.FirstOrDefault();
                if (menu3 != null)
                {
                    db.Remove(menu3);
                    db.SaveChanges();
                    UpdateDBBuyer();
                }
            }
        }
        private void ButtonClickDeleteMenu4(object sender, RoutedEventArgs e)
        {
            using (MenuArea4Context db = new())
            {
                MenuArea4 menu4 = db.Menu4.FirstOrDefault();
                if (menu4 != null)
                {
                    db.Remove(menu4);
                    db.SaveChanges();
                    UpdateDBBuyer();
                }
            }
        }
        private void AddFirstCourse(object sender, RoutedEventArgs e)
        {
            if (nameFirstCourse.Text == "")
            {
                downTrayPaymaster.Content = "Не ввели название";
                downTrayPaymaster.Background = Brushes.LightCoral;
            }
            if (NumericId.Text == "")
            {
                downTrayPaymaster.Content = "Не ввели цену";
                downTrayPaymaster.Background = Brushes.LightCoral;
            }
            else
            {
                using FirstCourseContext db = new FirstCourseContext();
                FirstCourse toms = new FirstCourse
                {
                    name = nameFirstCourse.Text,
                    price = NumericId.Text
                };
                db.FirstCourses.Add(toms);
                db.SaveChanges();
                downTrayPaymaster.Content = "Добавлено в базу данных";
                downTrayPaymaster.Background = Brushes.LightGreen;
                nameFirstCourse.Text = "";
                NumericId.Text = "";
            }
        }
        public void UpdateFirstCourseList()
        {
            using FirstCourseContext db = new();
            FirstCourseList.ItemsSource = db.FirstCourses.ToList();
        }
        private void ButtonClickDeleteChat(object sender, RoutedEventArgs e)
        {
            using MessagesContext db = new MessagesContext();
            Message toms = new Message();
            var item = db.Messages.FirstOrDefault();
            if (item != null)
            {
                db.Remove(item);
                db.SaveChanges();
                UpdateChatIfClickSend();
                downTrayChatRoom.Content = "Удалена первая запись:" + "" + DateTime.Now.ToString();
                downTrayChatRoom.Background = Brushes.LightCoral;
            }
        }
        private void ButtonRemoveId(object sender, RoutedEventArgs e)
        {
            using MessagesContext db = new MessagesContext();
            int key = Convert.ToInt32(SetIdChat.Text.Trim());
            var item = db.Messages.Find(key);

            if (item != null)
            {
                db.Messages.Remove(item);
                db.SaveChanges();
                UpdateChatIfClickSend();
                downTrayChatRoom.Content = "Удалена запись:" + "" + key + "" + DateTime.Now.ToString();
                downTrayChatRoom.Background = Brushes.LightCoral;
            }
        }
        private void ButtonUpdateChat(object sender, RoutedEventArgs e)
        {
            UpdateChatIfClickSend();
            downTrayChatRoom.Content = "Обновлено:" + "" + DateTime.Now.ToString();
            downTrayChatRoom.Background = Brushes.LightGreen;
        }
        //СГП
        //public void GetFinishedProduct()
        //{
        //    using OrderForBuyersContext db = new OrderForBuyersContext();
        //    using FinishedProductContext db1 = new();
        //    SetIdBuyer.Text = SetIdBuyer.Text.Trim();
        //    string key = SetIdBuyer.Text.Trim();
        //    var item = db.OrderForBuyers.Find(key);
        //    db1.FinishedProducts.Add(db.OrderForBuyers.Find(item));


        //    if (item != null)
        //    {
        //        var item2 = key;


        //        db.SaveChanges();


        //        db.OrderForBuyers.Remove(item);
        //        db.SaveChanges();
        //        DownTrayBuyerOrders.Background = Brushes.LightGreen;
        //        DownTrayBuyerOrders.Content = "Удалена запись --" + SetIdBuyer.Text;
        //        SetIdBuyer.Text = "";
        //        UserOrder.ItemsSource = db.OrderForBuyers.ToList();
        //    }



        //}
    }
}
