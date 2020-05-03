using System;
using System.Collections.Generic;

namespace Store.DB.Models
{
    public class Order
    {
        public int? Id { get; set; }
        public DateTime Date { get; set; }
        public Warehouse Warehouse { get; set; }
        public List<OrderDetails> OrderDetails { get; set; }
    }
}
