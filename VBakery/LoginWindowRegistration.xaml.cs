using System.Windows;
using System.Windows.Media;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using VBakery.DB;

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
            if (Name.Text != "" || LastName.Text != "" || Mobile.Text != "" || Password.Password != "" || SecondPassword.Password != "")
            {
                MessageBox.Show("Опаньки!");
            }
            else
            {
                MessageBox.Show("Задолбал!");
            }
        }
        public void ConnectDB()
        {
            UsersContext db = new();
            User tim = new User
            {
                Name = Name.Text,
                LastName = LastName.Text,
                Mobile = Mobile.Text,
                Password = Password.Password
            };
            db.Users.Add(tim);
            db.SaveChanges();
        }
        private void Button_ClickRegistration(object sender, RoutedEventArgs e)
        {
            bool flag = true;

            Name.Text = Name.Text.Trim(' ');
            LastName.Text = LastName.Text.Trim(' ');
            Mobile.Text = Mobile.Text.Trim(' ');
            Password.Password = Password.Password.Trim(' ');
            SecondPassword.Password = SecondPassword.Password.Trim(' ');

            if (Name.Text.Length <=2)
            {
                Name.ToolTip = "Не ввели имя";
                Name.Background = Brushes.LightCoral;
                flag = false;
            }
            if (LastName.Text.Length <= 2 )
            {
                LastName.ToolTip = "Не ввели фамилию";
                LastName.Background = Brushes.LightCoral;
                flag = false;
            }
            if (Mobile.Text.Length <=10 )
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
            if(flag)
            {
                using (UsersContext db1 = new UsersContext())
                {
                    var users = db1.Users.Where(p => p.Name == Name.Text);
                    foreach (User user in users)
                    {
                        if(user.Name != Name.Text)
                        {
                            MessageBox.Show("зашел в 1");
                        }
                        if (user.Name == Name.Text)
                        {
                            _ = MessageBox.Show("Ты уже зарегистрировался!");
                        }
                    }
                }
            }
        }
    }
}
