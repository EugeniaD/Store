namespace Store.API.Models.OutputModels
{
    public class OrderDetailsOutputModel
    {
        public int Id { get; set; }
        public int Quantity { get; set; }
        public decimal LocalPrice { get; set; }
        public string ProductBrand { get; set; }
        public string ProductModel { get; set; }
        public string SubCategoryName { get; set; }
    }
}
