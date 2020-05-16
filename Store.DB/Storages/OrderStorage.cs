using Dapper;
using Store.DB.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Store.Core.ConfigurationOptions;

namespace Store.DB.Storages
{
    public class OrderStorage : IOrderStorage
    {

        private IDbConnection connection;
        private IDbTransaction transaction;

        public OrderStorage(IOptions<StorageOptions> storageOptions)
        {
            this.connection = new SqlConnection(storageOptions.Value.DBConnectionString);
        }
        public void TransactionStart()
        {
            if (this.connection == null)
            {
                connection = new SqlConnection("Data Source = (local); Initial Catalog = Store; Integrated Security=True;");
            }

            connection.Open();
            transaction = this.connection.BeginTransaction();
        }
        public void TransactionCommit()
        {
            this.transaction?.Commit();
            connection?.Close();
        }
        public void TransactionRollBack()
        {
            this.transaction?.Rollback();
            connection?.Close();
        }

        internal static class SpName
        {
            public const string OrderAdd = "Order_Add";
            public const string OrderDetailsAdd = "OrderDetails_Add";
            public const string OrderWithDetailsSelectById = "Order_WithDetails_SelectById";
            public const string OrderByDates = "Order_ByDates";
            public const string ProductTotalCostByWarehouse = "Product_TotalCostByWarehouse";
            public const string TotalSumByIsForeign = "TotalSum_ByIsForeign";

        }

        public async ValueTask<Order> AddOrder(Order model)
        {
            DynamicParameters parameters = new DynamicParameters(new
            {
                warehouseId = model.Warehouse.Id
            });

            var OrderID = await connection.QueryAsync<int>(
                SpName.OrderAdd,
                parameters,
                transaction: transaction,
                commandType: CommandType.StoredProcedure);

            model.Id = OrderID.FirstOrDefault();

            await AddOrderDetails(model.OrderDetails, (int)model.Id);
            return await GetOrderWithDetailsById((int)model.Id);
        }

        public async ValueTask AddOrderDetails(List<OrderDetails> models, int orderId)
        {
            foreach (OrderDetails orderDetails in models)
            {
                int goodsId = orderDetails.Goods.Id;
                DynamicParameters parameters = new DynamicParameters(new
                {
                    orderId,
                    goodsId,
                    orderDetails.Quantity,
                    orderDetails.LocalPrice
                });

                await connection.QueryAsync<int>(
                SpName.OrderDetailsAdd,
                parameters,
                transaction: transaction,
                commandType: CommandType.StoredProcedure);
            }

        }

        public async ValueTask<Order> GetOrderWithDetailsById(int Id)
        {
            var orderDictionary = new Dictionary<int, Order>();
            var result = await connection.QueryAsync<Order, Warehouse, OrderDetails, Goods, Category, Order>(
                SpName.OrderWithDetailsSelectById,
                (o, w, od, g, c) =>
                {
                    Order orderEntry;
                    if (!orderDictionary.TryGetValue((int)o.Id, out orderEntry))
                    {
                        orderEntry = o;
                        orderEntry.OrderDetails = new List<OrderDetails>();
                        orderDictionary.Add((int)orderEntry.Id, orderEntry);
                    }
                    orderEntry.Warehouse = w;
                    orderEntry.OrderDetails.Add(od);
                    od.Goods = g;
                    g.Category = c;
                    return orderEntry;
                },
                param: new { Id },
                transaction: transaction,
                commandType: CommandType.StoredProcedure,
                splitOn: "Id");
            return result.FirstOrDefault();
        }

        public async ValueTask<List<WarehouseTotalCost>> GetTotalCostByWarehouse()
        {
            var result = await connection.QueryAsync<decimal, WarehouseTotalCost, WarehouseTotalCost>(
                SpName.ProductTotalCostByWarehouse,
                (t, w) =>
                {
                    WarehouseTotalCost warehouse = w;
                    warehouse.TotalMoney = t;
                    return warehouse;
                },
                param: null,
                commandType: CommandType.StoredProcedure,
                splitOn: "Id");
            return result.ToList();
        }
        public async ValueTask<List<OrdersByDates>> GetOrdersByDates(DateTime FromDate, DateTime ToDate)
        {
            var result = await connection.QueryAsync<OrdersByDates, Warehouse, Goods, Category, int, decimal, OrdersByDates>(
                SpName.OrderByDates,
                (o, w, g, c, tq, tc) =>
                {
                    OrdersByDates order = o;
                    o.Warehouse = w;
                    o.TotalQuantity = tq;
                    o.TotalCost = tc;
                    o.Goods = g;
                    g.Category = c;
                    return order;
                },
                param: new { FromDate, ToDate },
                commandType: CommandType.StoredProcedure,
                splitOn: "Id,Id,Id,TotalQuantity,TotalCost");
            return result.ToList();
        }

        public async ValueTask<Order> GetTotalCostByIsForeign()
        {
            var result = await connection.QueryAsync<Order>(
                SpName.TotalSumByIsForeign,
                param: null,
                commandType: CommandType.StoredProcedure);
            return result.FirstOrDefault();
        }
    }
}
