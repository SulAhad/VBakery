namespace VBakery.Model
{
    public class LogOrder
    {
        public int Id { get; set; } //порядковый номер  
        public string BuyerName { get; set; } //имя покупактеля
        public string BuyerMobile { get; set; } //телефон покупателя
        public string WeightProduct { get; set; }//вес продукта
        public string NameProduct { get; set; }//название продукта
        public string DeliveryAddress { get; set; }//аддресс доставки
        public string StaffComment { get; set; }//комментарии для персонала 
        public string DeliveryDate { get; set; }//дата доставки
        public string OrderDateTime { get; set; }//дата заказа
        public int OrderPrice { get; set; }//общая цена заказа
    }
}
