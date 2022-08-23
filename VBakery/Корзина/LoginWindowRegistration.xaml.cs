using System.Windows;
using System.Windows.Media;
using System.Linq;
using VBakery.DB;
using System;

namespace VBakery
{
    public partial class LoginWindowRegistration : Window
    {
        public LoginWindowRegistration()
        {
            InitializeComponent();

            Name.Text = null;
            LastName.Text = null;
            Mobile.Text = null;
            Password.Password = null;
            SecondPassword.Password = null;

            Name.Text = Name.Text.Trim(' ');
            LastName.Text = LastName.Text.Trim(' ');
            Mobile.Text = Mobile.Text.Trim(' ');
            Password.Password = Password.Password.Trim(' ');
            SecondPassword.Password = SecondPassword.Password.Trim(' ');

            //if (Name.Text != "" || LastName.Text != "" || Mobile.Text != "" || Password.Password != "" || SecondPassword.Password != "")
            //{
            //    MessageBox.Show("Опаньки!");
            //}
            //else
            //{
            //    MessageBox.Show("Задолбал!");
            //}
        }
        //public void ConnectDB()
        //{
        //    UsersContext usersContext = new();
        //    User tim = new()
        //    {
        //        Name = Name.Text,
        //        LastName = LastName.Text,
        //        Mobile = Mobile.Text,
        //        Password = Password.Password
        //    };
        //    usersContext.Users.Add(tim);
        //    usersContext.SaveChanges();
        //}
        private void Button_ClickRegistration(object sender, RoutedEventArgs e)
        {
            bool flag = true;

            Name.Text = Name.Text.Trim(' ');
            LastName.Text = LastName.Text.Trim(' ');
            Mobile.Text = Mobile.Text.Trim(' ');
            Password.Password = Password.Password.Trim(' ');
            SecondPassword.Password = SecondPassword.Password.Trim(' ');

            if (Name.Text.Length <= 2)
            {
                Name.ToolTip = "Не ввели имя";
                Name.Background = Brushes.LightCoral;
                flag = false;
            }
            if (LastName.Text.Length <= 2)
            {
                LastName.ToolTip = "Не ввели фамилию";
                LastName.Background = Brushes.LightCoral;
                flag = false;
            }
            if (Mobile.Text.Length <= 10)
            {
                Mobile.ToolTip = "Не ввели телефон";
                Mobile.Background = Brushes.LightCoral;
                flag = false;
            }
            if (Password.Password != SecondPassword.Password && Password.Password != "" && SecondPassword.Password != "")
            {
                Password.ToolTip = "Пароли не совпадают";
                Password.Background = Brushes.LightCoral;
                SecondPassword.ToolTip = "Пароли не совпадают";
                SecondPassword.Background = Brushes.LightCoral;
                flag = false;
            }
            if (flag)
            {
                UsersContext usersContext = new();
                User usersContext1 = new User
                {
                    Name = Name.Text,
                    LastName = LastName.Text,
                    Mobile = Mobile.Text,
                    Password = Password.Password,
                    DateOfRegistration = DateTime.Now.ToString()
                };
                usersContext.Users.Add(usersContext1);
                usersContext.SaveChanges();
                MessageBox.Show("Зарегал");

                //var u = usersContext.Users.Where(p => p.Name == Name.Text);
                //foreach (User user in u)
                //{
                //    if (Name.Text != user.Name || false)
                //    {
                //            Registr();
                //            MessageBox.Show("Зарегал");
                //    }
                //    if(Name.Text == user.Name)
                //    {
                //        _ = MessageBox.Show("Пользователь с таким именем уже зарегистрирован!");
                //    }
            //}
            }
        }
        //public void Registr()
        //{
        //    UsersContext usersContext = new();
        //    User usersContext1 = new()
        //    {
        //        Name = Name.Text,
        //        LastName = LastName.Text,
        //        Mobile = Mobile.Text,
        //        Password = Password.Password,
        //        DateOfRegistration = DateTime.Now.ToString()
        //    };
        //    usersContext.Users.Add(usersContext1);
        //    usersContext.SaveChanges();
        //}
    }
}
