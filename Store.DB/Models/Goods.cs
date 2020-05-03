namespace Store.DB.Models
{
    public class Goods
    {
        public int Id { get; set; } 
        public decimal Price { get; set; } 
        public string Brand { get; set; }
        public string Model { get; set; }
        public Category Category { get; set; } 
    }
}
