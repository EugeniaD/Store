using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Core;
using Store.DB.Models;
using Store.DB.Storages;
using StoreRepository.Common;

namespace StoreRepository.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private IOrderStorage _orderStorage;

        public OrderRepository(IOrderStorage orderStorage)
        {
            _orderStorage = orderStorage;
        }


        public async ValueTask<RequestResult<Order>> AddOrder(Order model)
        {
            var result = new RequestResult<Order>();
            try
            {
                decimal rateResult = 1;
                string path = "";
                switch (model.Warehouse.Id)
                {
                    case (int)WarehouseEnum.Minsk:
                        path = "BYN";
                        rateResult = await SendRequest.GetLocalCurrency(path);
                        break;
                    case (int)WarehouseEnum.Kiev:
                        path = "UAH";
                        rateResult = await SendRequest.GetLocalCurrency(path);
                        break;
                    default: break;
                }


                foreach (OrderDetails item in model.OrderDetails)
                {
                    item.LocalPrice /= rateResult;
                }
                _orderStorage.TransactionStart();
                result.RequestData = await _orderStorage.AddOrder(model);
                _orderStorage.TransactionCommit();
                result.IsOk = true;
            }
            catch (Exception ex)
            {
                _orderStorage.TransactionRollBack();
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<Order>> GetOrderWithDetailsById(int id)
        {
            var result = new RequestResult<Order>();
            try
            {
                result.RequestData = await _orderStorage.GetOrderWithDetailsById(id);

                result.IsOk = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }
        public async ValueTask<RequestResult<List<WarehouseTotalCost>>> GetTotalCostByWarehouse()
        {
            var result = new RequestResult<List<WarehouseTotalCost>>();
            try
            {
                result.RequestData = await _orderStorage.GetTotalCostByWarehouse();

                result.IsOk = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }
        public async ValueTask<RequestResult<List<OrdersByDates>>> GetOrdersByDates(DateTime FromDate, DateTime toDate)
        {
            var result = new RequestResult<List<OrdersByDates>>();
            try
            {
                result.RequestData = await _orderStorage.GetOrdersByDates(FromDate, toDate);

                result.IsOk = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<Order>> GetTotalCostByIsForeign()
        {
            var result = new RequestResult<Order>();
            try
            {
                result.RequestData = await _orderStorage.GetTotalCostByIsForeign();

                result.IsOk = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }
    }
}
