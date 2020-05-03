namespace Store.API.Models.InputModels
{
    public class OrderDetailsInputModel 
    {
        public int GoodsId { get; set; }
        public int Quantity { get; set; }
        public decimal LocalPrice { get; set; }
    }
}
