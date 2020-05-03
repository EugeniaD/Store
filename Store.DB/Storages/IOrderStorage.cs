using Store.DB.Models;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DB.Storages
{
    public interface IOrderStorage
    {
        ValueTask<Order> AddOrder(Order model);
        ValueTask AddOrderDetails(List<OrderDetails> models, int OrderID);
        ValueTask<Order> GetOrderWithDetailsById(int Id);
        ValueTask<List<WarehouseTotalCost>> GetTotalCostByWarehouse();
        ValueTask<List<OrdersByDates>> GetOrdersByDates(DateTime start, DateTime end);
        ValueTask<Order> GetTotalCostByIsForeign();
        void TransactionCommit();
        void TransactionStart();
        void TransactionRollBack();
    }
}