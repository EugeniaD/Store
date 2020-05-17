using Store.DB.Models;
using Store.Core;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreRepository.Repositories
{
    public interface IOrderRepository
    {     
        ValueTask<RequestResult<Order>> AddOrder(Order order);
        ValueTask<RequestResult<Order>> GetOrderWithDetailsById(int id);
        ValueTask<RequestResult<List<WarehouseTotalCost>>> GetTotalCostByWarehouse();
        ValueTask<RequestResult<List<OrdersByDates>>> GetOrdersByDates(DateTime start, DateTime end);
        ValueTask<RequestResult<Order>> GetTotalCostByIsForeign();
    }
}