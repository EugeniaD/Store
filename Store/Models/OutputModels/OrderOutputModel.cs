using System.Collections.Generic;

namespace Store.API.Models.OutputModels
{
    public class OrderOutputModel
    {
        public int Id { get; set; }
        public string Date { get; set; }
        public string WarehouseName { get; set; }
        public List<OrderDetailsOutputModel> OrderDetailsOutput { get; set; }

    }
}
