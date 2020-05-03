namespace Store.DB.Models
{
    public class OrderDetails
    {
        public int? Id { get; set; }
        public int? OrderID { get; set; }
        public int Quantity { get; set; }
        public decimal LocalPrice { get; set; }
        public Goods Goods { get; set; }
    }
}
