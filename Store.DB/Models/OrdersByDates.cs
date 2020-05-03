namespace Store.DB.Models
{
    public class OrdersByDates : Order
    {
        public Goods Goods { get; set; }
        public int TotalQuantity { get; set; }
        public decimal TotalCost { get; set; }
    }
}
