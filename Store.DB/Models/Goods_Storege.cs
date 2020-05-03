namespace Store.DB.Models
{
    public class Goods_Storege
    {
        public int? Id { get; set; } 
        public Goods Goods { get; set; } 
        public Warehouse Warehouse { get; set; } 
        public int Quantity { get; set; } 
    }
}
