using System.Collections.Generic;
namespace Store.API.Models.InputModels
{
    public class OrderInputModel 
    {
        public int WarehouseId { get; set; }
        public List<OrderDetailsInputModel> OrderDetailsList { get; set; }
    }
}
