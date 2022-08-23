using System;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using VBakery.DB;
using VBakery.MenuDB;
namespace VBakery
{
    public partial class Supervisor : Window
    {
        private readonly int customerCount;
        readonly RecipesContext recipesContext = new();
        readonly MessagesContext messagesContext = new();
        readonly OrderForBuyersContext orderForBuyersContext = new();
        readonly TimerStartsContext timerStartsContext = new();
        readonly TimerEndsContext timerEndsContext = new();
        readonly LogOrdersContext logOrders = new();
        readonly UsersContext usersContext = new();
        public Supervisor()
        {
            InitializeComponent();

            //if(checkKitchen.IsChecked)
            //{
                
            //}
            //if (checkPymaster.IsChecked == true)
            //{
            //    accessTextBoxRegistration.Text = "Кассир";
            //}
            //if (checkDelivery.IsChecked == true)
            //{
            //    accessTextBoxRegistration.Text = "Доставщик";
            //}
            //if (checkBuyer.IsChecked == true)
            //{
            //    accessTextBoxRegistration.Text = "Покупатель";
            //}
            //if (checkSupervisor.IsChecked == true)
            //{
            //    accessTextBoxRegistration.Text = "Директор";
            //}

            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandlerKeyDownEvent);

            MethodGetDataRecept();//Повар

            StartWork();//Кассир

            EndWork();//Кассир
            UpdateOrderForBuyer();

            UpdateChatIfClickSend();

            customerCount = orderForBuyersContext.OrderForBuyers.Count();//Подсчет количества продаж
            CountSales.Content = Convert.ToString(customerCount) + " " + "заказов";//Вывод результата подсчета

            System.Int32 BuyerCount = orderForBuyersContext.OrderForBuyers.Count();
            VisitBuyer.Text = Convert.ToString(BuyerCount);

            TotalSales.Content = orderForBuyersContext.OrderForBuyers.Sum(p => p.OrderPrice).ToString() + " " + "руб";//Получение общей суммы продаж из базы данных

            LogOrder.ItemsSource = logOrders.LogOrders.ToList();

            UserRegistration.ItemsSource = usersContext.Users.ToList();
        }
        public void UpdateChatIfClickSend()
        {
            Chat.ItemsSource = messagesContext.Messages.ToList();
        }//Обновить чат если нажата кнопка отправить
        public void UpdateOrderForBuyer()
        {
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
        }// Обновление меню покупаетля
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
            ListBeginTime.ItemsSource = timerStartsContext.TimerStarts.ToList();
        }//Кассир начало работы
        public void EndWork()
        {
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
                Recipe toms = new()
                {
                    NameRecipe = nameRecipe.Text,
                    TextRecipe = RecipeArea.Text,
                    PriceRecipe = Convert.ToInt32(Price.Text)
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
            ReceptList.ItemsSource = recipesContext.Recipes.ToList();
            DownTrayDeleteRecept.Content = "Обновлено:" + DateTime.Now.ToString();
            DownTrayDeleteRecept.Background = Brushes.LightGreen;
        }//Повар получить данные рецепта из сервера
        private void ButtonClickDeleteFirstID(object sender, RoutedEventArgs e)
        {
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
            UpdateOrderForBuyer();
            System.Int32 BuyerCount = orderForBuyersContext.OrderForBuyers.Count();
            VisitBuyer.Text = Convert.ToString(BuyerCount);
            DownTrayBuyerOrders.Content = "Обновлено --" + DateTime.Now.ToString();
        }//Обновить данные покупатель


        ////////////////Касса
        private void ButtonDeleteTimeBegin(object sender, RoutedEventArgs e)
        {
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
            TimerEnd user = timerEndsContext.TimerEnds.FirstOrDefault();
            if (user != null)
            {
                timerEndsContext.Remove(user);
                timerEndsContext.SaveChanges();
                EndWork();
            }
        }//Касса конец работы кнопка удалить
        //private void ScrollBar_ValueChanged(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    ScrollBar scrollBar = new();
        //    scrollBar.Value = numeric.Value;
        //    SetIdBuyer.Text = Convert.ToString(numeric.Value);
        //} //Кнопка прокурутки номера в окне покупаетля
        //private void ScrollBar_ValueChangedChat(object sender, RoutedPropertyChangedEventArgs<double> e)
        //{
        //    ScrollBar scrollBar = new();
        //    scrollBar.Value = numericChat.Value;
        //    SetIdChat.Text = Convert.ToString(numericChat.Value);
        //}//Кнопка прокурутки номера в окне чата

        ///Разные методы
        public void DoNotHaveName()
        {
            downTrayBuyer.Content = "Не ввели название";
            downTrayBuyer.Background = Brushes.LightCoral;
        }//метод отсутствия имени
        public void DoNotHaveConsist()
        {
            downTrayBuyer.Content = "Не ввели состав";
            downTrayBuyer.Background = Brushes.LightCoral;
        }//метод отсутствия состава
        public void DoNotHavePrice()
        {
            downTrayBuyer.Content = "Не ввели состав";
            downTrayBuyer.Background = Brushes.LightCoral;
        }//метод отсутствия цены
        public void AddTextToTray()
        {
            downTrayBuyer.Content = "Добавлено в базу данных";
            downTrayBuyer.Background = Brushes.LightGreen;
        }//метод подтвержедения снизу строки
        private void ButtonClickUpdate(object sender, RoutedEventArgs e)
        {
            UpdateDBBuyer();
        }// кнопка обновления окна покупателя


        ///Тестовые методы отпарвки данных
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
                MenuArea1 toms = new()
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
        }//первое поле
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
                MenuArea2 toms = new()
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
        }//второе поле
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
                MenuArea3 toms = new()
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
        }//3 поле
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
                MenuArea4 toms = new()
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
        }//4 поле 
        private void ButtonClickDeleteMenu1(object sender, RoutedEventArgs e)
        {
            using MenuArea1Context db = new();
            MenuArea1 menu1 = db.Menu1.FirstOrDefault();
            if (menu1 != null)
            {
                db.Remove(menu1);
                db.SaveChanges();
                UpdateDBBuyer();
            }
        }// удаление первое поле
        private void ButtonClickDeleteMenu2(object sender, RoutedEventArgs e)
        {
            using MenuArea2Context db = new();
            MenuArea2 menu2 = db.Menu2.FirstOrDefault();
            if (menu2 != null)
            {
                db.Remove(menu2);
                db.SaveChanges();
                UpdateDBBuyer();
            }
        }//удаление второе поле
        private void ButtonClickDeleteMenu3(object sender, RoutedEventArgs e)
        {
            using MenuArea3Context db = new();
            MenuArea3 menu3 = db.Menu3.FirstOrDefault();
            if (menu3 != null)
            {
                db.Remove(menu3);
                db.SaveChanges();
                UpdateDBBuyer();
            }
        }// удаление 3 поле
        private void ButtonClickDeleteMenu4(object sender, RoutedEventArgs e)
        {
            using MenuArea4Context db = new();
            MenuArea4 menu4 = db.Menu4.FirstOrDefault();
            if (menu4 != null)
            {
                db.Remove(menu4);
                db.SaveChanges();
                UpdateDBBuyer();
            }
        }// удаление 4 поле 

        private void ButtonClickDeleteChat(object sender, RoutedEventArgs e)
        {
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
        public void ButtonClick_1(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 1;
        }
        public void ButtonClick_2(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 2;
        }
        public void ButtonClick_3(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 3;
        }
        public void ButtonClick_4(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 4;
        }
        public void ButtonClick_5(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 5;
        }
        public void ButtonClick_6(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 6;
        }
        public void ButtonClick_7(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 7;
        }
        public void ButtonClick_8(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 8;
        }
        public void ButtonClick_9(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 9;
        }
        public void ButtonClick_0(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text += 0;
        }
        public void ButtonClickClear(object sender, RoutedEventArgs e)
        {
            InputIdForDelete.Text = "";
        }
        public void ButtonClick_11(object sender, RoutedEventArgs e)
        {
            InputIdForDeleteChat.Text += 1;
        }
        public void ButtonClick_21(object sender, RoutedEventArgs e)
        {
            InputIdForDeleteChat.Text += 2;
        }
        public void ButtonClick_31(object sender, RoutedEventArgs e)
        {
            InputIdForDeleteChat.Text += 3;
        }
        public void ButtonClick_41(object sender, RoutedEventArgs e)
        {
            InputIdForDeleteChat.Text += 4;
        }
        public void ButtonClick_51(object sender, RoutedEventArgs e)
        {
            InputIdForDeleteChat.Text += 5;
        }
        public void ButtonClick_61(object sender, RoutedEventArgs e)
        {
            InputIdForDeleteChat.Text += 6;
        }
        public void ButtonClick_71(object sender, RoutedEventArgs e)
        {
            InputIdForDeleteChat.Text += 7;
        }
        public void ButtonClick_81(object sender, RoutedEventArgs e)
        {
            InputIdForDeleteChat.Text += 8;
        }
        public void ButtonClick_91(object sender, RoutedEventArgs e)
        {
            InputIdForDeleteChat.Text += 9;
        }
        public void ButtonClick_01(object sender, RoutedEventArgs e)
        {
            InputIdForDeleteChat.Text += 0;
        }
        public void ButtonClickClear1(object sender, RoutedEventArgs e)
        {
            InputIdForDeleteChat.Text = "";
        }
        private void WrapPanelVisible(object sender, MouseButtonEventArgs e)
        {
            WrapPanelNums.Visibility = Visibility.Visible;
        }
        public void ButnClick_1(object sender, RoutedEventArgs e)
        {
            Price.Text += 1;
        }

        public void ButnClick_2(object sender, RoutedEventArgs e)
        {
            Price.Text += 2;
        }

        public void ButnClick_3(object sender, RoutedEventArgs e)
        {
            Price.Text += 3;
        }

        public void ButnClick_4(object sender, RoutedEventArgs e)
        {
            Price.Text += 4;
        }

        public void ButnClick_5(object sender, RoutedEventArgs e)
        {
            Price.Text += 5;
        }

        public void ButnClick_6(object sender, RoutedEventArgs e)
        {
            Price.Text += 6;
        }

        public void ButnClick_7(object sender, RoutedEventArgs e)
        {
            Price.Text += 7;
        }

        public void ButnClick_8(object sender, RoutedEventArgs e)
        {
            Price.Text += 8;
        }

        public void ButnClick_9(object sender, RoutedEventArgs e)
        {
            Price.Text += 9;
        }

        public void ButnClick_0(object sender, RoutedEventArgs e)
        {
            Price.Text += 0;
        }

        public void ButnClickClearNums(object sender, RoutedEventArgs e)
        {
            Price.Text = "";
        }

        private void ButtonClickRegistrationUser(object sender, RoutedEventArgs e)
        {
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
            if (mobileTextBoxRegistration.Text == "")
            {
                DownTrayRegistration.Content = "Не ввели номер телефона!";
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
                    Mobile = mobileTextBoxRegistration.Text,
                    Password = passwordTextBoxRegistration.Text,
                    DateOfRegistration = DateTime.Now.ToString(),
                    CheckKitchen = 
                    accessKitchen.Text,
                    CheckPymaster = accessPaymast.Text,
                    CheckDelivery = accessDelivery.Text,
                    CheckBuyer = accessBuyer.Text,
                    CheckSupervisor = accessSupervisor.Text
                };
                usersContext.Users.Add(usersContext1);
                usersContext.SaveChanges();
                UserRegistration.ItemsSource = usersContext.Users.ToList();
                DownTrayRegistration.Content = "Пользователь зарегистрирован!";
                DownTrayRegistration.Background = Brushes.LightGreen;
                nameTextBoxRegistration.Text = "";
                lastNameTextBoxRegistration.Text = "";
                mobileTextBoxRegistration.Text = "";
                passwordTextBoxRegistration.Text = "";
                accessBuyer.Text = "";
                accessBuyer.Background = Brushes.LightCoral;
                accessKitchen.Text = "";
                accessKitchen.Background = Brushes.LightCoral;
                accessPaymast.Text = "";
                accessPaymast.Background = Brushes.LightCoral;
                accessDelivery.Text = "";
                accessDelivery.Background = Brushes.LightCoral;
                accessSupervisor.Text = "";
                accessSupervisor.Background = Brushes.LightCoral;
            }
        }
        private void ButtonClick_1Reg(object sender, RoutedEventArgs e)
        {
            RegIdSearch.Text += 1;
        }

        private void ButtonClick_2Reg(object sender, RoutedEventArgs e)
        {
            RegIdSearch.Text += 2;
        }

        private void ButtonClick_3Reg(object sender, RoutedEventArgs e)
        {
            RegIdSearch.Text += 3;
        }

        private void ButtonClick_4Reg(object sender, RoutedEventArgs e)
        {
            RegIdSearch.Text += 4;
        }

        private void ButtonClick_5Reg(object sender, RoutedEventArgs e)
        {
            RegIdSearch.Text += 5;
        }

        private void ButtonClick_6Reg(object sender, RoutedEventArgs e)
        {
            RegIdSearch.Text += 6;
        }

        private void ButtonClick_7Reg(object sender, RoutedEventArgs e)
        {
            RegIdSearch.Text += 7;
        }

        private void ButtonClick_8Reg(object sender, RoutedEventArgs e)
        {
            RegIdSearch.Text += 8;
        }

        private void ButtonClick_9Reg(object sender, RoutedEventArgs e)
        {
            RegIdSearch.Text += 9;
        }

        private void ButtonClick_0Reg(object sender, RoutedEventArgs e)
        {
            RegIdSearch.Text += 0;
        }

        private void ButtonClickClearReg(object sender, RoutedEventArgs e)
        {
            RegIdSearch.Text = "";
        }

        private void ButtonClickDeleteReg(object sender, RoutedEventArgs e)
        {
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
                    usersContext.Users.Remove(item);
                    usersContext.SaveChanges();
                    UpdateChatIfClickSend();
                    DownTrayRegistration.Content = "Удалена запись:" + " " + key + " --" + DateTime.Now.ToString();
                    DownTrayRegistration.Background = Brushes.LightCoral;
                    RegIdSearch.Text = "";
                    UserRegistration.ItemsSource = usersContext.Users.ToList();
                }
                else
                {
                    DownTrayRegistration.Content = "Нет такого значения!";
                    DownTrayRegistration.Background = Brushes.LightCoral;
                }
            }
        }
        
                
                
                
                
        private void checkKitchenCheked(object sender, RoutedEventArgs e)
        {
            accessKitchen.Text = "Повар";
            Hidden.Text += "Повар";
            accessKitchen.Background = Brushes.LightGreen;
        }
        private void checkPymasterCheked(object sender, RoutedEventArgs e)
        {
            accessPaymast.Text = "Кассир";
            Hidden.Text += "Кассир";
            accessPaymast.Background = Brushes.LightGreen;
        }
        private void checkDeliveryCheked(object sender, RoutedEventArgs e)
        {
            accessDelivery.Text = "Доставщик";
            Hidden.Text += "Доставщик";
            accessDelivery.Background = Brushes.LightGreen;
        }
        private void checkBuyerCheked(object sender, RoutedEventArgs e)
        {
            accessBuyer.Text = "Покупатель";
            Hidden.Text += "Покупатель";
            accessBuyer.Background = Brushes.LightGreen;
        }
        private void checkSupervisorCheked(object sender, RoutedEventArgs e)
        {
            accessSupervisor.Text = "Директор";
            Hidden.Text += "Директор";
            accessSupervisor.Background = Brushes.LightGreen;
        }
        private void checkBuyerUnchecked(object sender, RoutedEventArgs e)
        {
            accessBuyer.Text = "";
            accessBuyer.Background = Brushes.LightCoral;
        }
        private void checkKitchenUnchecked(object sender, RoutedEventArgs e)
        {
            accessKitchen.Text = "";
            accessKitchen.Background = Brushes.LightCoral;
        }
        private void checkPymasterUnchecked(object sender, RoutedEventArgs e)
        {
            accessPaymast.Text = "";
            accessPaymast.Background = Brushes.LightCoral;
        }
        private void checkDeliveryUnchecked(object sender, RoutedEventArgs e)
        {
            accessDelivery.Text = "";
            accessDelivery.Background = Brushes.LightCoral;
        }
        private void checkSupervisorUnchecked(object sender, RoutedEventArgs e)
        {
            accessSupervisor.Text = "";
            accessSupervisor.Background = Brushes.LightCoral;
        }
    }
}
