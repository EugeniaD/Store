namespace Store.API.Models.OutputModels
{
    public class OrdersInTimePeriodOutputModel
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string WarehouseName { get; set; }
        public int TotalQuantityGoods { get; set; }
        public decimal TotalSum { get; set; }
        public string ProductBrand { get; set; }
        public string ProductModel { get; set; }
        public string SubCategoryName { get; set; }
    }
}
