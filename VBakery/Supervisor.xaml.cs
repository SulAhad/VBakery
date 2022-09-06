using Microsoft.Win32;
using System;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
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
        readonly RecipesContext recipesContext = new();
        readonly MessagesContext messagesContext = new();
        readonly OrderForBuyersContext orderForBuyersContext = new();
        readonly TimerStartsContext timerStartsContext = new();
        readonly TimerEndsContext timerEndsContext = new();
        readonly LogOrdersContext logOrders = new();
        readonly UsersContext usersContext = new();
        readonly PriceForMenusContext priceForMenusContext = new();
        readonly FirstMenuContext firstMenuContext = new();
        readonly SecondMenuContext secondMenuContext = new();
        readonly ThirdMenuContext thirdMenuContext = new();
        public int countNums1 = 0;///Подсчет количества нажатий на цифровые кнопки 1 окно
        public int countNums2 = 0;///Подсчет количества нажатий на цифровые кнопки 2 окно
        public int countNums3 = 0;///Подсчет количества нажатий на цифровые кнопки 3 окно
        public int countNums4 = 0;///Подсчет количества нажатий на цифровые кнопки 3 окно
        public int countNums5 = 0;///Подсчет количества нажатий на цифровые кнопки 3 окно
        public int countNums6 = 0;///Подсчет количества нажатий на цифровые кнопки 3 окно
        public Supervisor()
        {
            InitializeComponent();
            Menu1.ItemsSource = firstMenuContext.FirstMenus.ToList();
            Menu2.ItemsSource = secondMenuContext.SecondMenus.ToList();
            Menu3.ItemsSource = thirdMenuContext.ThirdMenus.ToList();
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
        ///Тестовые методы отпарвки данных
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
        private void AddNameToBase(object sender, RoutedEventArgs e)
        {
            if (priceFruct.Text == "")
            {
                downTrayBuyer.Content = "Не ввели название";
                downTrayBuyer.Background = Brushes.LightCoral;
            }
            if (priceMan.Text == "")
            {
                downTrayBuyer.Content = "Не ввели название";
                downTrayBuyer.Background = Brushes.LightCoral;
            }
            if (priceShoko.Text == "")
            {
                downTrayBuyer.Content = "Не ввели название";
                downTrayBuyer.Background = Brushes.LightCoral;
            }
            if (priceMussoviy.Text == "")
            {
                downTrayBuyer.Content = "Не ввели название";
                downTrayBuyer.Background = Brushes.LightCoral;
            }
            if (priceSvadebniy.Text == "")
            {
                downTrayBuyer.Content = "Не ввели название";
                downTrayBuyer.Background = Brushes.LightCoral;
            }
            if (priceBirthday.Text == "")
            {
                downTrayBuyer.Content = "Не ввели название";
                downTrayBuyer.Background = Brushes.LightCoral;
            }
            if (priceKids.Text == "")
            {
                downTrayBuyer.Content = "Не ввели название";
                downTrayBuyer.Background = Brushes.LightCoral;
            }
            else
            {
                PriceForMenu toms = new()
                {
                    PriceFruct = priceFruct.Text,
                    PriceMan = priceMan.Text,
                    PriceShoko = priceShoko.Text,
                    PriceMussoviy = priceMussoviy.Text,
                    PriceSvadebniy = priceSvadebniy.Text,
                    PriceBirthday = priceBirthday.Text,
                    PriceKids = priceKids.Text
                };
                priceForMenusContext.PriceForMenus.Add(toms);
                priceForMenusContext.SaveChanges();
                downTrayBuyer.Content = "Добавлено";
                downTrayBuyer.Background = Brushes.LightGreen;
            }
        }
        private void ButtonAddMenu1(object sender, RoutedEventArgs e)
        {
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
                Menu1.ItemsSource = firstMenuContext.FirstMenus.ToList();
                downTrayPaymaster.Content = "Добавлено в меню!";
                downTrayPaymaster.Background = Brushes.LightGreen;
                nameMenu1.Text = "";
                priceMenu1.Text = "";
            }
        }
        private void ButtonAddMenu2(object sender, RoutedEventArgs e)
        {
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
                Menu2.ItemsSource = secondMenuContext.SecondMenus.ToList();
                downTrayPaymaster.Content = "Добавлено в меню!";
                downTrayPaymaster.Background = Brushes.LightGreen;
                nameMenu2.Text = "";
                priceMenu2.Text = "";
            }
        }
        private void ButtonAddMenu3(object sender, RoutedEventArgs e)
        {
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
                Menu3.ItemsSource = thirdMenuContext.ThirdMenus.ToList();
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
        private void priceMenu1_MouseDown(object sender, MouseButtonEventArgs e)///Действие при нажатие кнопки на окно цена 
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
        }
        private void priceMenu2_MouseDown(object sender, MouseButtonEventArgs e)
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
        }
        private void priceMenu3_MouseDown(object sender, MouseButtonEventArgs e)
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
                    Menu1.ItemsSource = firstMenuContext.FirstMenus.ToList();
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
                    Menu2.ItemsSource = secondMenuContext.SecondMenus.ToList();
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
                    Menu3.ItemsSource = thirdMenuContext.ThirdMenus.ToList();
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
            dispatcherTimer.Tick += new EventHandler(dispatcherTimer_Tick);
            dispatcherTimer.Interval = new TimeSpan(0, 0, 4);
            dispatcherTimer.Start();
        }///Таймер через 4 чекунды закрашивает нижний трей в исходный цвет
        private void dispatcherTimer_Tick(object sender, EventArgs e)
        {
            downTrayPaymaster.Content = "";
            downTrayPaymaster.Background = Brushes.AliceBlue;
        }///Таймер заданных параметров
    }
}
