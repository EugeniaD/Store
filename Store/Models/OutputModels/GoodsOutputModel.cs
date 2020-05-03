namespace Store.API.Models.OutputModels
{
    public class GoodsOutputModel
    {
        public int Id { get; set; }
        public decimal Price { get; set; }
        public string Brand { get; set; }
        public string Model { get; set; }
        public string SubCategoryName { get; set; }
    }
}
