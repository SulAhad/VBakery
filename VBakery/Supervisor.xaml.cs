using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Threading;
using VBakery.DB;
using VBakery.MenuDB;
using VBakery.Model;
using Brushes = System.Windows.Media.Brushes;

namespace VBakery
{
    public partial class Supervisor : Window
    {
        private readonly int customerCount;
        public int countNums1;///Подсчет количества нажатий на цифровые кнопки 1 окно
        public int countNums2;///Подсчет количества нажатий на цифровые кнопки 2 окно
        public int countNums3;///Подсчет количества нажатий на цифровые кнопки 3 окно
        public int countNums4;///Подсчет количества нажатий на цифровые кнопки 3 окно
        public int countNums5;///Подсчет количества нажатий на цифровые кнопки 3 окно
        public int countNums6;///Подсчет количества нажатий на цифровые кнопки 3 окно
        public int countKeyRecepts;///Подсчет количества нажатий на вызов кнопок в рецепатх
        public int countKeyUpdateRecepts; ///Подсчет количества нажатий на вызов клавы в редактирование рецепта
        public int countKeyBuyer;///Подсчет количества нажатий на вызов клавы в Заказе покупателя
        public int countKeyChat;///Подсчет количества нажатий на вызов клавы в Чате
        public int countKeyRegUser;///Подсчет количества нажатий на вызов клавы в Чате
        public Supervisor()
        {
            InitializeComponent();
            FirstMenuContext firstMenuContext = new();
            stackPanelMenu1.ItemsSource = firstMenuContext.FirstMenus.ToList();
            SecondMenuContext secondMenuContext = new();
            stackPanelMenu2.ItemsSource = secondMenuContext.SecondMenus.ToList();
            ThirdMenuContext thirdMenuContext = new();
            stackPanelMenu3.ItemsSource = thirdMenuContext.ThirdMenus.ToList();
            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandlerKeyDownEvent);
            MethodGetDataRecept();//Повар
            StartWork();//Кассир
            EndWork();//Кассир
            UpdateOrderForBuyer();
            UpdateChatIfClickSend();
            OrderForBuyersContext orderForBuyersContext = new();
            customerCount = orderForBuyersContext.OrderForBuyers.Count();//Подсчет количества продаж
            CountSales.Content = Convert.ToString(customerCount) + " " + "заказов";//Вывод результата подсчета
            System.Int32 BuyerCount = orderForBuyersContext.OrderForBuyers.Count();
            VisitBuyer.Text = Convert.ToString(BuyerCount);
            TotalSales.Content = orderForBuyersContext.OrderForBuyers.Sum(p => p.OrderPrice).ToString() + " " + "руб";//Получение общей суммы продаж из базы данных
            LogOrdersContext logOrders = new();
            LogOrder.ItemsSource = logOrders.LogOrders.ToList();
            UsersContext usersContext = new();
            UserRegistration.ItemsSource = usersContext.Users.ToList();
        }
        public void UpdateChatIfClickSend()
        {
            MessagesContext messagesContext = new();
            Chat.ItemsSource = messagesContext.Messages.ToList();
        }//Обновить чат если нажата кнопка отправить
        public void UpdateOrderForBuyer()
        {
            OrderForBuyersContext orderForBuyersContext = new();
            UserOrder.ItemsSource = orderForBuyersContext.OrderForBuyers.ToList();
        }//Обновить список Заказов
        public void ButtonClickPlus(object sender, RoutedEventArgs e)
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
        public void ButtonClickMinus(object sender, RoutedEventArgs e)
        {
            this.WindowState = WindowState.Minimized;
        }//Кнопка свернуть окно
        public void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }//Кнопка закрыть окно
        public void ClickButtonSendMessage(object sender, RoutedEventArgs e)
        {
            MessagesContext messagesContext = new();
            if (MessageUser.Text != "")
            {
                Message toms = new()
                {
                    User = "Директор",
                    TextUser = MessageUser.Text,
                    Time = DateTime.Now.ToString()
                };
                messagesContext.Messages.Add(toms);
                messagesContext.SaveChanges();
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

        }//Отправить сообщение в чат
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
        ////////////////Кассир
        public void StartWork()
        {
            TimerStartsContext timerStartsContext = new();
            ListBeginTime.ItemsSource = timerStartsContext.TimerStarts.ToList();
        }//Кассир начало работы
        public void EndWork()
        {
            TimerEndsContext timerEndsContext = new();
            ListEndTime.ItemsSource = timerEndsContext.TimerEnds.ToList();
        }//Кассир конец работы
        public void GoBackToHome(object sender, RoutedEventArgs e)
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
        }//Кнопка выхода домой
        ///////////////////Повар
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
                RecipesContext recipesContext = new();
                Recipe toms = new()
                {
                    NameRecipe = nameRecipe.Text,
                    TextRecipe = RecipeArea.Text,
                    PriceRecipe = Price.Text
                };
                recipesContext.Recipes.Add(toms);
                recipesContext.SaveChanges();
                DownTray.Content = "Добавлено в базу данных";
                DownTray.Background = Brushes.LightGreen;
                nameRecipe.Text = "";
                RecipeArea.Text = "";
                Price.Text = "";
            }
        }//Повар добавить новый рецепт
        private void GetDataRecept(object sender, RoutedEventArgs e)
        {
            MethodGetDataRecept();
            DownTrayDeleteRecept.Content = "Обновлено:" + DateTime.Now.ToString();
            DownTrayDeleteRecept.Background = Brushes.LightGreen;
        }//Повар получить данные рецепта из сервера
        private void MethodGetDataRecept()
        {
            RecipesContext recipesContext = new();
            ReceptList.ItemsSource = recipesContext.Recipes.ToList();
            DownTrayDeleteRecept.Content = "Обновлено:" + DateTime.Now.ToString();
            DownTrayDeleteRecept.Background = Brushes.LightGreen;
        }//Повар получить данные рецепта из сервера
        private void ButtonClickDeleteFirstID(object sender, RoutedEventArgs e)
        {
            RecipesContext recipesContext = new();
            var item = recipesContext.Recipes.FirstOrDefault();
            if (item != null)
            {
                recipesContext.Recipes.Remove(item);
                recipesContext.SaveChanges();
                MethodGetDataRecept();
                DownTrayDeleteRecept.Background = Brushes.LightGreen;
                DownTrayDeleteRecept.Content = "Удалена первая запись";
            }
        }//Кнопка удалить первую ID Кухня
        private void RemoveLastData(object sender, RoutedEventArgs e)
        {
            RecipesContext recipesContext = new();
            if (SetId.Text == "")
            {
                DownTrayDeleteRecept.Content = "Не ввели номер!";
                DownTrayDeleteRecept.Background = Brushes.LightCoral;
            }
            else
            {
                SetId.Text = SetId.Text.Trim();
                int key = Convert.ToInt32(SetId.Text.Trim());
                var item = recipesContext.Recipes.Find(key);
                if (item != null)
                {

                    recipesContext.Recipes.Remove(item);
                    recipesContext.SaveChanges();
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
        }//Удалить запись рецепта
        ////////////////// //Покупатель
        private void RemoveLastOrder(object sender, RoutedEventArgs e)
        {
            OrderForBuyersContext orderForBuyersContext = new();
            if (InputIdForDelete.Text == "")
            {
                DownTrayBuyerOrders.Content = "Не ввели номер!";
                DownTrayBuyerOrders.Background = Brushes.LightCoral;
            }
            else
            {
                InputIdForDelete.Text = InputIdForDelete.Text.Trim();
                int key = Convert.ToInt32(InputIdForDelete.Text.Trim());
                var item = orderForBuyersContext.OrderForBuyers.Find(key);

                if (item != null)
                {
                    orderForBuyersContext.OrderForBuyers.Remove(item);
                    orderForBuyersContext.SaveChanges();
                    DownTrayBuyerOrders.Background = Brushes.LightGreen;
                    DownTrayBuyerOrders.Content = "Удалена запись --" + InputIdForDelete.Text;
                    InputIdForDelete.Text = "";
                    UserOrder.ItemsSource = orderForBuyersContext.OrderForBuyers.ToList();
                    System.Int32 BuyerCount = orderForBuyersContext.OrderForBuyers.Count();
                    VisitBuyer.Text = Convert.ToString(BuyerCount);
                }
                else
                {
                    MessageBox.Show("Введена некорреткная цифра!");
                }
            }
        }//Удалить заказ покупаетля
        private void GetDataBuyer(object sender, RoutedEventArgs e)
        {
            OrderForBuyersContext orderForBuyersContext = new();
            UpdateOrderForBuyer();
            System.Int32 BuyerCount = orderForBuyersContext.OrderForBuyers.Count();
            VisitBuyer.Text = Convert.ToString(BuyerCount);
            DownTrayBuyerOrders.Content = "Обновлено --" + DateTime.Now.ToString();
        }//Обновить данные покупатель
        ////////////////Касса
        private void ButtonDeleteTimeBegin(object sender, RoutedEventArgs e)
        {
            TimerStartsContext timerStartsContext = new();
            TimerStart user = timerStartsContext.TimerStarts.FirstOrDefault();
            if (user != null)
            {
                timerStartsContext.Remove(user);
                timerStartsContext.SaveChanges();
                StartWork();
            }
        }//Касса начало работы кнопка удалить
        private void ButtonDeleteTimeEnd(object sender, RoutedEventArgs e)
        {
            TimerEndsContext timerEndsContext = new();
            TimerEnd user = timerEndsContext.TimerEnds.FirstOrDefault();
            if (user != null)
            {
                timerEndsContext.Remove(user);
                timerEndsContext.SaveChanges();
                EndWork();
            }
        }//Касса конец работы кнопка удалить
        private void ButtonClickDeleteChat(object sender, RoutedEventArgs e)
        {
            MessagesContext messagesContext = new();
            var item = messagesContext.Messages.FirstOrDefault();
            if (item == null)
            {
                downTrayChatRoom.Content = "Отсутствует запись!";
                downTrayChatRoom.Background = Brushes.LightCoral;
            }
            else
            {
                if (item != null)
                {
                    messagesContext.Remove(item);
                    messagesContext.SaveChanges();
                    UpdateChatIfClickSend();
                    downTrayChatRoom.Content = "Удалена первая запись:" + "" + DateTime.Now.ToString();
                    downTrayChatRoom.Background = Brushes.LightCoral;
                }
            }
        } //кнопка удаления чата первой строки
        private void ButtonRemoveId(object sender, RoutedEventArgs e)
        {
            MessagesContext messagesContext = new();
            if (InputIdForDeleteChat.Text == "")
            {
                downTrayChatRoom.Content = "Не ввели номер!";
                downTrayChatRoom.Background = Brushes.LightCoral;
            }
            else
            {
                int key = Convert.ToInt32(InputIdForDeleteChat.Text.Trim());
                var item = messagesContext.Messages.Find(key);

                if (item != null)
                {
                    messagesContext.Messages.Remove(item);
                    messagesContext.SaveChanges();
                    UpdateChatIfClickSend();
                    downTrayChatRoom.Content = "Удалена запись:" + "==" + key + " --" + DateTime.Now.ToString();
                    downTrayChatRoom.Background = Brushes.LightCoral;
                }
                else
                {
                    downTrayChatRoom.Content = "Нет такого значения!";
                    downTrayChatRoom.Background = Brushes.LightCoral;
                }
            }

        }// кнопка удаления чата по id
        private void ButtonUpdateChat(object sender, RoutedEventArgs e)
        {
            UpdateChatIfClickSend();
            downTrayChatRoom.Content = "Обновлено:" + "" + DateTime.Now.ToString();
            downTrayChatRoom.Background = Brushes.LightGreen;
        }//кнопка обновления чата
        private void WrapPanelVisible(object sender, MouseButtonEventArgs e)
        {
            countKeyUpdateRecepts++;
            if (countKeyUpdateRecepts <= 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    Button button = new Button();
                    button.Content = i;
                    button.Click += ButtonOnClickKeyUpdateRecepts;
                    button.Width = 45;
                    button.Height = 45;
                    button.Margin = new Thickness(2, 2, 2, 2);
                    WrapPanelNums.Children.Add(button);
                }
            }
            else
            {
                countKeyUpdateRecepts = 0;
                Price.Text = "";
                WrapPanelNums.Children.Clear();
            }
        }
        public void ButtonOnClickKeyUpdateRecepts(object sender, RoutedEventArgs e)///Метод нажатия кнопки в цифровые кнопки
        {
            if (Price.Text.Length < 4)
            {
                var button = (Button)sender;
                var num = button.Content;
                Price.Text += num;

            }
            else
            {
                return;
            }
        }
        private void ButtonClickRegistrationUser(object sender, RoutedEventArgs e)
        {
            UsersContext usersContext = new();
            bool flag = true;
            if (nameTextBoxRegistration.Text == "")
            {
                DownTrayRegistration.Content = "Не ввели имя!";
                DownTrayRegistration.Background = Brushes.LightCoral;
                flag = false;
            }
            if (lastNameTextBoxRegistration.Text == "")
            {
                DownTrayRegistration.Content = "Не ввели фамилию!";
                DownTrayRegistration.Background = Brushes.LightCoral;
                flag = false;
            }
            if (passwordTextBoxRegistration.Text == "")
            {
                DownTrayRegistration.Content = "Не ввели пароль пользователя!";
                DownTrayRegistration.Background = Brushes.LightCoral;
                flag = false;
            }
            if (flag)
            {
                User usersContext1 = new User
                {
                    Name = nameTextBoxRegistration.Text,
                    LastName = lastNameTextBoxRegistration.Text,
                    Password = passwordTextBoxRegistration.Text,
                    DateOfRegistration = DateTime.Now.ToString(),
                };
                usersContext.Users.Add(usersContext1);
                usersContext.SaveChanges();
                UserRegistration.ItemsSource = usersContext.Users.ToList();
                DownTrayRegistration.Content = "Пользователь зарегистрирован!";
                DownTrayRegistration.Background = Brushes.LightGreen;
                nameTextBoxRegistration.Text = "";
                lastNameTextBoxRegistration.Text = "";
                passwordTextBoxRegistration.Text = "";
            }
        }
        private void ButtonClickDeleteReg(object sender, RoutedEventArgs e)
        {
            UsersContext usersContext = new();
            if (RegIdSearch.Text == "")
            {
                DownTrayRegistration.Content = "Не ввели номер!";
                DownTrayRegistration.Background = Brushes.LightCoral;
            }
            else
            {
                int key = Convert.ToInt32(RegIdSearch.Text.Trim());
                var item = usersContext.Users.Find(key);
                if (item != null)
                {
                    var login = usersContext.Users.Count();
                    if (login <= 1)
                    {
                        MessageBoxResult result = MessageBox.Show(
                        "Вы точно хотите удалить?",
                        "Последний пользователь, при удалении программа перезапустится!",
                        MessageBoxButton.YesNo);
                        if (result == MessageBoxResult.Yes)
                        {
                            usersContext.Users.Remove(item);
                            usersContext.SaveChanges();
                            LoginWindow loginWindow = new();
                            loginWindow.Show();
                            this.Close();
                        }
                    }
                    else
                    {
                        usersContext.Users.Remove(item);
                        usersContext.SaveChanges();
                        UpdateChatIfClickSend();
                        DownTrayRegistration.Content = "Удалена запись:" + " " + key + " --" + DateTime.Now.ToString();
                        DownTrayRegistration.Background = Brushes.LightCoral;
                        RegIdSearch.Text = "";
                        UserRegistration.ItemsSource = usersContext.Users.ToList();
                    }
                }
                else
                {
                    DownTrayRegistration.Content = "Нет такого номера!";
                    DownTrayRegistration.Background = Brushes.LightCoral;
                    RegIdSearch.Text = "";
                    DispatcherTimer dispatcherTimer = new();
                    dispatcherTimer.Tick += new EventHandler(DispatcherTimer_TickUsers);
                    dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
                    dispatcherTimer.Start();
                }
            }
        }
        private void DispatcherTimer_TickUsers(object sender, EventArgs e)
        {
            DownTrayRegistration.Content = "";
            DownTrayRegistration.Background = Brushes.LightCyan;
        }
        private void ButtonAddMenu1(object sender, RoutedEventArgs e)
        {
            FirstMenuContext firstMenuContext = new();
            bool flag = true;
            if (nameMenu1.Text == "")
            {
                downTrayPaymaster.Content = "Не ввели название!";
                downTrayPaymaster.Background = Brushes.LightCoral;
                flag = false;
            }
            if (priceMenu1.Text == "")
            {
                downTrayPaymaster.Content = "Не ввели название!";
                downTrayPaymaster.Background = Brushes.LightCoral;
                flag = false;
            }
            if(flag)
            {
                FirstMenu firstMenu = new()
                {
                    Name = nameMenu1.Text,
                    Price = (string)priceMenu1.Text,
                };
                firstMenuContext.FirstMenus.Add(firstMenu);
                firstMenuContext.SaveChanges();
                stackPanelMenu1.ItemsSource = firstMenuContext.FirstMenus.ToList();
                downTrayPaymaster.Content = "Добавлено в меню!";
                downTrayPaymaster.Background = Brushes.LightGreen;
                nameMenu1.Text = "";
                priceMenu1.Text = "";
            }
        }
        private void ButtonAddMenu2(object sender, RoutedEventArgs e)
        {
            SecondMenuContext secondMenuContext = new();
            bool flag = true;
            if (nameMenu2.Text == "")
            {
                downTrayPaymaster.Content = "Не ввели название!";
                downTrayPaymaster.Background = Brushes.LightCoral;
                flag = false;
            }
            if (priceMenu2.Text == "")
            {
                downTrayPaymaster.Content = "Не ввели название!";
                downTrayPaymaster.Background = Brushes.LightCoral;
                flag = false;
            }
            if(flag)
            {
                SecondMenu secondMenu = new()
                {
                    Name = nameMenu2.Text,
                    Price = priceMenu2.Text,
                };
                secondMenuContext.SecondMenus.Add(secondMenu);
                secondMenuContext.SaveChanges();
                stackPanelMenu2.ItemsSource = secondMenuContext.SecondMenus.ToList();
                downTrayPaymaster.Content = "Добавлено в меню!";
                downTrayPaymaster.Background = Brushes.LightGreen;
                nameMenu2.Text = "";
                priceMenu2.Text = "";
            }
        }
        private void ButtonAddMenu3(object sender, RoutedEventArgs e)
        {
            ThirdMenuContext thirdMenuContext = new();
            bool flag = true;
            if (nameMenu3.Text == "")
            {
                downTrayPaymaster.Content = "Не ввели название!";
                downTrayPaymaster.Background = Brushes.LightCoral;
                flag = false;
            }
            if (priceMenu3.Text == "")
            {
                downTrayPaymaster.Content = "Не ввели название!";
                downTrayPaymaster.Background = Brushes.LightCoral;
                flag = false;
            }
            if(flag)
            {
                ThirdMenu thirdMenu = new()
                {
                    Name = nameMenu3.Text,
                    Price = priceMenu3.Text,
                };
                thirdMenuContext.ThirdMenus.Add(thirdMenu);
                thirdMenuContext.SaveChanges();
                stackPanelMenu3.ItemsSource = thirdMenuContext.ThirdMenus.ToList();
                downTrayPaymaster.Content = "Добавлено в меню!";
                downTrayPaymaster.Background = Brushes.LightGreen;
                nameMenu3.Text = "";
                priceMenu3.Text = "";
            }
        }
        public void ButtonOnClickNums1(object sender, RoutedEventArgs e)///Метод нажатия кнопки в цифровые кнопки
        {
            if (priceMenu1.Text.Length < 4)
            {
                var button = (Button)sender;
                var num = button.Content;
                priceMenu1.Text += num;

            }
            else
            {
                return;
            }
        }
        private void PriceMenu1_MouseDown(object sender, MouseButtonEventArgs e)///Действие при нажатие кнопки на окно цена 
        {
            countNums1 ++;
            if (countNums1 <= 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    Button button = new Button();
                    button.Content = i;
                    button.Click += ButtonOnClickNums1;
                    button.Width = 45;
                    button.Height = 45;
                    StackPanelForNums1.Children.Add(button);
                }
            }
            else
            {
                countNums1 = 0;
                priceMenu1.Text = "";
                StackPanelForNums1.Children.Clear();
            }    
        }
        private void PriceMenu2_MouseDown(object sender, MouseButtonEventArgs e)
        {
            countNums2 ++;
            if (countNums2 <= 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    Button button = new Button();
                    button.Content = i;
                    button.Click += ButtonOnClickNums2;
                    button.Width = 45;
                    button.Height = 45;
                    StackPanelForNums2.Children.Add(button);
                }
            }
            else
            {
                countNums2 = 0;
                priceMenu2.Text = "";
                StackPanelForNums2.Children.Clear();
            }
        }
        private void PriceMenu3_MouseDown(object sender, MouseButtonEventArgs e)
        {
            countNums3++;
            if (countNums3 <= 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    Button button = new Button();
                    button.Content = i;
                    button.Click += ButtonOnClickNums3;
                    button.Width = 45;
                    button.Height = 45;
                    StackPanelForNums3.Children.Add(button);
                }
            }
            else
            {
                countNums3 = 0;
                priceMenu3.Text = "";
                StackPanelForNums3.Children.Clear();
            }
        }
        public void ButtonOnClickNums2(object sender, RoutedEventArgs e)///Метод нажатия кнопки в цифровые кнопки
        {
            if (priceMenu2.Text.Length < 4)
            {
                var button = (Button)sender;
                var num = button.Content;
                priceMenu2.Text += num;

            }
            else
            {
                return;
            }
        }
        public void ButtonOnClickNums3(object sender, RoutedEventArgs e)///Метод нажатия кнопки в цифровые кнопки
        {
            if (priceMenu3.Text.Length < 4)
            {
                var button = (Button)sender;
                var num = button.Content;
                priceMenu3.Text += num;

            }
            else
            {
                return;
            }
        }
        private void ButtonClearNums1(object sender, RoutedEventArgs e)
        {
            priceMenu1.Text = "";
        }
        private void ButtonClearNums2(object sender, RoutedEventArgs e)
        {
            priceMenu2.Text = "";
        }
        private void ButtonClearNums3(object sender, RoutedEventArgs e)
        {
            priceMenu3.Text = "";
        }
        private void InputIdForDeleteMenu1(object sender, MouseButtonEventArgs e)
        {
            countNums4++;
            if (countNums4 <= 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    Button button = new Button();
                    button.Content = i;
                    button.Click += ButtonOnClickNums4;
                    button.Width = 30;
                    button.Height = 30;
                    numKeysForDelete1.Children.Add(button);
                }
            }
            else
            {
                countNums4 = 0;
                inputIdForDeleteMenu1.Text = "";
                numKeysForDelete1.Children.Clear();
            }
        }
        private void InputIdForDeleteMenu2(object sender, MouseButtonEventArgs e)
        {
            countNums5++;
            if (countNums5 <= 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    Button button = new Button();
                    button.Content = i;
                    button.Click += ButtonOnClickNums5;
                    button.Width = 30;
                    button.Height = 30;
                    numKeysForDelete2.Children.Add(button);
                }
            }
            else
            {
                countNums5 = 0;
                inputIdForDeleteMenu2.Text = "";
                numKeysForDelete2.Children.Clear();
            }
        }
        private void InputIdForDeleteMenu3(object sender, MouseButtonEventArgs e)
        {
            countNums6++;
            if (countNums6 <= 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    Button button = new Button();
                    button.Content = i;
                    button.Click += ButtonOnClickNums6;
                    button.Width = 30;
                    button.Height = 30;
                    numKeysForDelete3.Children.Add(button);
                }
            }
            else
            {
                countNums6 = 0;
                inputIdForDeleteMenu3.Text = "";
                numKeysForDelete3.Children.Clear();
            }
        }
        public void ButtonOnClickNums4(object sender, RoutedEventArgs e)///Метод нажатия кнопки в цифровые кнопки
        {
            if (inputIdForDeleteMenu1.Text.Length < 4)
            {
                var button = (Button)sender;
                var num = button.Content;
                inputIdForDeleteMenu1.Text += num;
            }
            else
            {
                return;
            }
        }
        public void ButtonOnClickNums6(object sender, RoutedEventArgs e)///Метод нажатия кнопки в цифровые кнопки
        {
            if (inputIdForDeleteMenu3.Text.Length < 4)
            {
                var button = (Button)sender;
                var num = button.Content;
                inputIdForDeleteMenu3.Text += num;

            }
            else
            {
                return;
            }
        }
        public void ButtonOnClickNums5(object sender, RoutedEventArgs e)///Метод нажатия кнопки в цифровые кнопки
        {
            if (inputIdForDeleteMenu2.Text.Length < 4)
            {
                var button = (Button)sender;
                var num = button.Content;
                inputIdForDeleteMenu2.Text += num;

            }
            else
            {
                return;
            }
        }
        private void ButtonClickDeleteMenu1(object sender, RoutedEventArgs e)///кнопка удаления Меню1
        {
            FirstMenuContext firstMenuContext = new();
            if (inputIdForDeleteMenu1.Text == "")
            {
                downTrayPaymaster.Content = "Не ввели номер!";
                downTrayPaymaster.Background = Brushes.LightCoral;
                SendButtonNotifications();
            }
            else
            {
                int key = Convert.ToInt32(inputIdForDeleteMenu1.Text.Trim());
                var item = firstMenuContext.FirstMenus.Find(key);

                if (item != null)
                {
                    firstMenuContext.FirstMenus.Remove(item);
                    firstMenuContext.SaveChanges();
                    stackPanelMenu1.ItemsSource = firstMenuContext.FirstMenus.ToList();
                    inputIdForDeleteMenu1.Text = "";
                    downTrayPaymaster.Content = "Удалена запись:" + "==" + key + " --" + DateTime.Now.ToString();
                    downTrayPaymaster.Background = Brushes.LightCoral;
                    SendButtonNotifications();
                }
                else
                {
                    downTrayPaymaster.Content = "Нет такого значения!";
                    downTrayPaymaster.Background = Brushes.LightCoral;
                    SendButtonNotifications();
                }
            }
        }///Удаление по номеру меню 1
        private void ButtonClickDeleteMenu2(object sender, RoutedEventArgs e)///кнопка удаления Меню1
        {
            SecondMenuContext secondMenuContext = new();
            if (inputIdForDeleteMenu2.Text == "")
            {
                downTrayPaymaster.Content = "Не ввели номер!";
                downTrayPaymaster.Background = Brushes.LightCoral;
                SendButtonNotifications();
            }
            else
            {
                int key = Convert.ToInt32(inputIdForDeleteMenu2.Text.Trim());
                var item = secondMenuContext.SecondMenus.Find(key);

                if (item != null)
                {
                    secondMenuContext.SecondMenus.Remove(item);
                    secondMenuContext.SaveChanges();
                    stackPanelMenu2.ItemsSource = secondMenuContext.SecondMenus.ToList();
                    inputIdForDeleteMenu2.Text = "";
                    downTrayPaymaster.Content = "Удалена запись:" + "==" + key + " --" + DateTime.Now.ToString();
                    downTrayPaymaster.Background = Brushes.LightCoral;
                    SendButtonNotifications();
                }
                else
                {
                    downTrayPaymaster.Content = "Нет такого значения!";
                    downTrayPaymaster.Background = Brushes.LightCoral;
                    SendButtonNotifications();
                }
            }
        }///Удаление по номеру меню 2
        private void ButtonClickDeleteMenu3(object sender, RoutedEventArgs e)///кнопка удаления Меню1
        {
            ThirdMenuContext thirdMenuContext = new();
            if (inputIdForDeleteMenu3.Text == "")
            {
                downTrayPaymaster.Content = "Не ввели номер!";
                downTrayPaymaster.Background = Brushes.LightCoral;
                SendButtonNotifications();
            }
            else
            {
                int key = Convert.ToInt32(inputIdForDeleteMenu3.Text.Trim());
                var item = thirdMenuContext.ThirdMenus.Find(key);

                if (item != null)
                {
                    thirdMenuContext.ThirdMenus.Remove(item);
                    thirdMenuContext.SaveChanges();
                    stackPanelMenu3.ItemsSource = thirdMenuContext.ThirdMenus.ToList();
                    inputIdForDeleteMenu3.Text = "";
                    downTrayPaymaster.Content = "Удалена запись:" + "==" + key + " --" + DateTime.Now.ToString();
                    downTrayPaymaster.Background = Brushes.LightCoral;
                    SendButtonNotifications();
                }
                else
                {
                    downTrayPaymaster.Content = "Нет такого значения!";
                    downTrayPaymaster.Background = Brushes.LightCoral;
                    SendButtonNotifications();
                }
            }
        }///Удаление по номеру меню 3
        private void SendButtonNotifications()
        {
            DispatcherTimer dispatcherTimer = new();
            dispatcherTimer.Tick += new EventHandler(DispatcherTimer_TickPaymaster);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
            dispatcherTimer.Start();
        }///Таймер через 4 чекунды закрашивает нижний трей в исходный цвет
        private void DispatcherTimer_TickPaymaster(object sender, EventArgs e)
        {
            downTrayPaymaster.Content = "";
            downTrayPaymaster.Background = Brushes.AliceBlue;
        }
        private void MouseDownOpenKeysRecept(object sender, MouseButtonEventArgs e)
        {
            countKeyRecepts++;
            if (countKeyRecepts <= 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    Button button = new Button();
                    button.Content = i;
                    button.Click += ButtonOnClickKeyRecepts;
                    button.Width = 30;
                    button.Height = 30;
                    button.Margin = new Thickness(2, 2, 2, 2);
                    StackPanelRecepts.Children.Add(button);
                }
            }
            else
            {
                countKeyRecepts = 0;
                SetId.Text = "";
                StackPanelRecepts.Children.Clear();
            }
        }
        public void ButtonOnClickKeyRecepts(object sender, RoutedEventArgs e)///Метод нажатия кнопки в цифровые кнопки
        {
            if (SetId.Text.Length < 4)
            {
                var button = (Button)sender;
                var num = button.Content;
                SetId.Text += num;

            }
            else
            {
                return;
            }
        }
        private void InputWrapPanelKeysBuyer(object sender, MouseButtonEventArgs e)
        {
            countKeyBuyer++;
            if (countKeyBuyer <= 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    Button button = new Button();
                    button.Content = i;
                    button.Click += ButtonOnClickKeyBuyer;
                    button.Width = 30;
                    button.Height = 30;
                    button.Margin = new Thickness(2, 2, 2, 2);
                    wrapPanelKeysBuyer.Children.Add(button);
                }
            }
            else
            {
                countKeyBuyer = 0;
                InputIdForDelete.Text = "";
                wrapPanelKeysBuyer.Children.Clear();
            }
        }
        public void ButtonOnClickKeyBuyer(object sender, RoutedEventArgs e)///Метод нажатия кнопки в цифровые кнопки
        {
            if (InputIdForDelete.Text.Length < 4)
            {
                var button = (Button)sender;
                var num = button.Content;
                InputIdForDelete.Text += num;

            }
            else
            {
                return;
            }
        }
        private void InputIdChat(object sender, MouseButtonEventArgs e)
        {
            countKeyChat++;
            if (countKeyChat <= 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    Button button = new Button();
                    button.Content = i;
                    button.Click += ButtonOnClickKeyChat;
                    button.Width = 45;
                    button.Height = 45;
                    button.Margin = new Thickness(2, 2, 2, 2);
                    wrapPanelChat.Children.Add(button);
                }
            }
            else
            {
                countKeyChat = 0;
                InputIdForDeleteChat.Text = "";
                wrapPanelChat.Children.Clear();
            }
        }
        public void ButtonOnClickKeyChat(object sender, RoutedEventArgs e)///Метод нажатия кнопки в цифровые кнопки
        {
            if (InputIdForDeleteChat.Text.Length < 4)
            {
                var button = (Button)sender;
                var num = button.Content;
                InputIdForDeleteChat.Text += num;

            }
            else
            {
                return;
            }
        }
        private void RegIdSearch_MouseDown(object sender, MouseButtonEventArgs e)
        {
            countKeyRegUser++;
            if (countKeyRegUser <= 1)
            {
                for (int i = 0; i < 10; i++)
                {
                    Button button = new Button();
                    button.Content = i;
                    button.Click += ButtonOnClickRegUser;
                    button.Width = 50;
                    button.Height = 50;
                    button.Margin = new Thickness(2, 2, 2, 2);
                    wrapPanelRegUser.Children.Add(button);
                }
            }
            else
            {
                countKeyRegUser = 0;
                RegIdSearch.Text = "";
                wrapPanelRegUser.Children.Clear();
            }
        }
        public void ButtonOnClickRegUser(object sender, RoutedEventArgs e)///Метод нажатия кнопки в цифровые кнопки
        {
            if (RegIdSearch.Text.Length < 4)
            {
                var button = (Button)sender;
                var num = button.Content;
                RegIdSearch.Text += num;

            }
            else
            {
                return;
            }
        }
    }
}
