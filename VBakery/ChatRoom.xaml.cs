using System;
using System.Linq;
using System.Windows;
using System.Windows.Input;
using System.Windows.Media;
using VBakery.DB;

namespace VBakery
{
    public partial class ChatRoom : Window
    {
        public ChatRoom()
        {
            InitializeComponent();

            AddHandler(Keyboard.KeyDownEvent, (KeyEventHandler)HandlerKeyDownEvent);

            UpdateChatIfClickSend();
        }
        private void HandlerKeyDownEvent(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.Enter:
                    ClickButtonSendMessage();
                    break;
                case Key.Escape:
                    Close();
                    break;
                default:
                    break;
            }
        }//Клавиатура
        private void ClickButtonSendMessage()
        {
            if(MessageUser.Text != "")
            {
                Chat.ItemsSource += DateTime.Now.ToString() + "" + MessageUser.Text.Trim() + "\n";
                using MessagesContext db = new MessagesContext();
                Message toms = new Message
                {
                    Time = DateTime.Now.ToString(),
                    User = (string)chatUser.Content,
                    TextUser = MessageUser.Text,
                };
                db.Messages.Add(toms);
                db.SaveChanges();
                UpdateChatIfClickSend();
                MessageUser.Text = "";
                downTray.Background = Brushes.Transparent;
                downTray.Content = "";
            }
            else
            {
                downTray.Content = "Не ввели сообщение!";
                downTray.Background = Brushes.AliceBlue;
            }
        }
        public void UpdateChatIfClickSend()
        {
            using MessagesContext db = new();
            Chat.ItemsSource = db.Messages.ToList();
        }
        public void ClickButtonSendMessage(object sender, RoutedEventArgs e)
        {
            if (MessageUser.Text != "")
            {
                Chat.ItemsSource += DateTime.Now.ToString() + "" + MessageUser.Text.Trim() + "\n";
                using MessagesContext db = new MessagesContext();
                Message toms = new Message
                {
                    Time = DateTime.Now.ToString(),
                    User = (string)chatUser.Content,
                    TextUser = MessageUser.Text,
                };
                db.Messages.Add(toms);
                db.SaveChanges();
                UpdateChatIfClickSend();
                MessageUser.Text = "";
                downTray.Background = Brushes.Transparent;
                downTray.Content = "";
            }
            else
            {
                downTray.Content = "Не ввели сообщение!";
                downTray.Background = Brushes.AliceBlue;
            }
        }
        private void ButtonClickClose(object sender, RoutedEventArgs e)
        {
            this.Close();
        }
    }
}
