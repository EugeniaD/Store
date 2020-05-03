namespace Store.DB.Models
{
    public class GoodsSearchModel
    {
		public int? Id { get; set; }
		public decimal? Price { get; set; }
		public string Brand { get; set; }
		public string Model { get; set; }
		public int? CategoryId { get; set; }
		public int? SubcategoryId { get; set; }
	}
}
