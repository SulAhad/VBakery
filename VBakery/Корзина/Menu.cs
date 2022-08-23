namespace VBakery.DB
{
    public class Menu
    {
        public Menu(int id, string name, string text, string price)
        {
            Id = id;
            Name = name;
            Text = text;
            Price = price;
        }

        private int Id { get; set; }
        private string Name { get; set; }
        private string Text { get; set; }
        private string Price { get; set; }
    }
}
