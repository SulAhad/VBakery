using System;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using Microsoft.EntityFrameworkCore;
using DbContext = Microsoft.EntityFrameworkCore.DbContext;

namespace VBakery
{
    public partial class BuyerWindow : Window
    {
        public BuyerWindow()
        {
            InitializeComponent();

        }
        private void Button_Apply(object sender, RoutedEventArgs e)
        {
            (sender as Button).IsEnabled = true;

            if ((bool)check2.IsChecked)
            {
                choose_Weight.Text += "Начинка 2" + "\r\n";
            }
            if ((bool)check3.IsChecked)
            {
                choose_Weight.Text += "Начинка 3" + "\r\n";
            }
            if ((bool)check4.IsChecked)
            {
                choose_Weight.Text += "Начинка 4" + "\r\n";
            }
            if ((bool)check5.IsChecked)
            {
                choose_Weight.Text += "Начинка 5" + "\r\n";
            }
            if ((bool)check6.IsChecked)
            {
                choose_Weight.Text += "Начинка 6" + "\r\n";
            }
            if ((bool)check7.IsChecked)
            {
                choose_Weight.Text += "Начинка 7" + "\r\n";
            }
            if ((bool)check8.IsChecked)
            {
                choose_Weight.Text += "Начинка 8" + "\r\n";
            }
            if ((bool)check9.IsChecked)
            {
                choose_Weight.Text += "Начинка 9" + "\r\n";
            }
            if ((bool)check10.IsChecked)
            {
                choose_Weight.Text += "Начинка 10" + "\r\n";
            }
            if ((bool)check11.IsChecked)
            {
                choose_Weight.Text += "Начинка 11" + "\r\n";
            }
            if ((bool)check12.IsChecked)
            {
                choose_Weight.Text += "Начинка 12" + "\r\n";
            }
            if ((bool)check13.IsChecked)
            {
                choose_Weight.Text += "Начинка 13" + "\r\n";
            }
        }
        private void Button_Clear(object sender, RoutedEventArgs e)
        {
            _ = new CheckBox { Content = "", MinHeight = 20, IsChecked = false };
            choose_Weight.Text = string.Empty;
        }
        private void Back_to_Buyer(object sender, RoutedEventArgs e)
        {
            Buyer buyer = new();
            buyer.Show();
            this.Close();
        }
        private void Button_apply_address(object sender, RoutedEventArgs e)
        {
            var text = address_Message1.Text.Trim();
            address_Message2.Text += text;
            address_Message1.Text = string.Empty;
        }
        private void Button_clear_address(object sender, RoutedEventArgs e)
        {
            address_Message2.Text = string.Empty;
        }
        private void Button_clear_comm(object sender, RoutedEventArgs e)
        {
            text_comm1.Text = string.Empty;
            text_comm2.Text = string.Empty;
        }
        private void Button_apply_comm(object sender, RoutedEventArgs e)
        {
            var text = text_comm1.Text.Trim();
            text_comm2.Text += text;
            text_comm1.Text = string.Empty;
        }
        private void Button_clear_visual(object sender, RoutedEventArgs e)
        {
            visual_text.Text = string.Empty;
        }
        private void Button_apply_visual(object sender, RoutedEventArgs e)
        {
            if ((bool)check14.IsChecked)
            {
                visual_text.Text += "Оформление" + "\r\n";
            }
            if ((bool)check15.IsChecked)
            {
                visual_text.Text += "Оформление3" + "\r\n";
            }
            if ((bool)check16.IsChecked)
            {
                visual_text.Text += "Оформление4" + "\r\n";
            }
            if ((bool)check17.IsChecked)
            {
                visual_text.Text += "Оформление5" + "\r\n";
            }
            if ((bool)check18.IsChecked)
            {
                visual_text.Text += "Оформление6" + "\r\n";
            }
            if ((bool)check19.IsChecked)
            {
                visual_text.Text += "Оформление7" + "\r\n";
            }
            if ((bool)check20.IsChecked)
            {
                visual_text.Text += "Оформление8" + "\r\n";
            }
            if ((bool)check21.IsChecked)
            {
                visual_text.Text += "Оформление9" + "\r\n";
            }
            if ((bool)check21.IsChecked)
            {
                visual_text.Text += "Оформление10" + "\r\n";
            }
            if ((bool)check22.IsChecked)
            {
                visual_text.Text += "Оформление11" + "\r\n";
            }
            if ((bool)check23.IsChecked)
            {
                visual_text.Text += "Оформление12" + "\r\n";
            }
            if ((bool)check24.IsChecked)
            {
                visual_text.Text += "Оформление13" + "\r\n";
            }
        }

        public void Button_Send(object sender, RoutedEventArgs e)
        {
            using ApplicationContext db = new ApplicationContext();
            // создаем два объекта Order

            Order tom = new Order { Weight = choose_Weight.Text, Visual = visual_text.Text, Address = address_Message2.Text, Comm = text_comm2.Text };
            //User alice = new User { Name = "Alice", Age = 26 };

            // добавляем их в бд
            db.Orders.Add(tom);
            db.SaveChanges();
            Console.WriteLine("Объекты успешно сохранены");

            // получаем объекты из бд и выводим на консоль

            //ContentWindow contentWindow = new();
            //app.CreateNewUser(choose_Weight.Text, visual_text.Text, address_Message2.Text, text_comm2.Text);

        }
        public class Order
        {
            public int Id { get; set; }
            public string? Weight { get; set; }
            public string Visual { get; set; }
            public string Address { get; set; }
            public string Comm { get; set; }



        }
        public class ApplicationContext : DbContext
        {
            public DbSet<Order> Orders => Set<Order>();
            public ApplicationContext() => Database.EnsureCreated();

            protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
            {
                optionsBuilder.UseSqlite("Data Source=order.db");
            }
        }

        //public class ApplicationInfo
        //{
        //    private MyDbContext dbContext = new MyDbContext();

        //    public ApplicationInfo()
        //    {

        //    }
        //    public int GetUserId(string input)
        //    {
        //        foreach (UserInfo item in dbContext.UserInfos)
        //        {
        //            if (item.Choose_Weight == input)
        //            {
        //                return item.Id;
        //            }
        //        }
        //        return 0;
        //    }
        //    public void CreateNewUser(string choose_Weight, string visual_text, string address_Message2, string text_comm2)
        //    {
        //        dbContext.UserInfos.Add(new UserInfo { Choose_Weight = choose_Weight, Visual_text = visual_text, Address_Message2 = address_Message2, Text_comm2 = text_comm2 });
        //        dbContext.SaveChanges();
        //    }
        //}
        //class MyDbContext : DbContext// Наследоваие чтобы воспользоваться этим калссом
        //{
        //    public DbSet<UserInfo> UserInfos { get; set; }
        //    public DbSet<Message> Messages { get; set; }

        //    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)// Метод подключения
        //    {
        //        optionsBuilder.UseSqlite($"Data Source={Path.Combine(@"E:\Мои проекты\C#\VBakery", "User.db")}");// Свойства куда подключать 
        //        base.OnConfiguring(optionsBuilder);
        //    }
        //}
        //public class ContentWindow : Window, IDisposable
        //{
        //    Buyer_window buyer_Window = new();
        //    public string user;
        //    ApplicationInfo app = null;

        //    private MyDbContext dbContext = new MyDbContext();

        //    public ContentWindow(ApplicationInfo app, string login, CollectionViewSource categoryViewSourсe)//Конструктор
        //    {
        //        InitializeComponent();
        //        this.app = app;
        //        user = login;
        //        infoUser.Text = app.GetUserId(user).ToString();

        //        categoryViewSourсe = (CollectionViewSource)FindResource(nameof(categoryViewSourсe));

        //        dbContext.Database.EnsureCreated(); // класс который отвечает за взаимодействие с базами данных. Метод создает базу данных если ее еще нет

        //        dbContext.UserInfos.Load();// загружаем данные в UserInfos
        //        dbContext.Messages.Load();

        //        categoryViewSourсe.Source = dbContext.UserInfos.Local.ToObservableCollection();// Записать то что мы получили
        //    }

        //    public ContentWindow(ApplicationInfo app, string text)
        //    {
        //        this.app = app;
        //    }

        //    protected override void OnClosing(CancelEventArgs e)// Метод при закрытии окна
        //    {
        //        dbContext.Dispose(); //закрыть соединение
        //        base.OnClosing(e);
        //    }

        //    private void SaveChanges(Object sender, RoutedEventArgs e) //Метод сохранения
        //    {
        //        dbContext.SaveChanges();// Метод Сохранения изменений
        //                                //categoryDataGrid.Items.Refresh();// Перезагрузка DataGrid
        //    }

        //    //private void users_SelectionChanged(Object sender, SelectionChangedEventArgs e)// метод при изменении
        //    //{
        //    //    infoUser.Text = ((UserInfo)users.SelectedValue).Id.ToString();
        //    //}
        //    private void AddMessage(object sender, RoutedEventArgs e)
        //    {
        //        if (buyer_Window.choose_Weight.Text != "")
        //        {
        //            if (((UserInfo)users.SelectedValue).Id != 0)
        //            {

        //            }
        //        }
        //    }

        //    public void Dispose()
        //    {
        //        throw new NotImplementedException();
        //    }
        //}
        //class Message //
        //{
        //    public int MessageId { get; set; } //Свойства для получения id 
        //    public string MessageText { get; set; } // свойтсво для получения текста

        //    //[DataType(DataType.DateTime)]
        //    public DateTime MessageDateTime { get; set; } //свойство для получения времени и даты

        //    public string MessageState { get; set; } // свойство для получения статуса о прочтении. Прочитано сообщение или нет

        //    public int UserSenderId { get; set; }// отправитель
        //    public virtual UserInfo UserSender { get; set; }//база данных должна подтягивать нужного нам user
        //    public int UserRecipientId { get; set; }// получатель
        //    public virtual UserInfo UserRecipient { get; set; }//база данных должна подтягивать нужного нам user
        //}
        //public partial class RegisterWindow : Window
        //{
        //    ApplicationInfo app = null;

        //    public RegisterWindow(ApplicationInfo app)
        //    {
        //        this.app = app;
        //    }
        //    private void Register(object sender, RoutedEventArgs e)
        //    {
        //        bool flag = true;
        //        if (!app.IsUserForLogin(loginBox.Text))
        //        {

        //        }
        //        else
        //        {
        //            loginBox.ToolTip = "Пользователь уже существует";
        //            loginBox.Background = Brushes.LightCoral;
        //        }
        //    }
        //}
        //class UserInfo
        //{
        //    public int Id { get; set; }
        //    public string Choose_Weight { get; set; }//Login
        //    public string Visual_text { get; set; }//Name
        //    public string Address_Message2 { get; set; }//LastName
        //    public string Text_comm2 { get; set; }//Password
        //}
    }
}
