using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using Store.Core;
using Store.DB.Models;
using Store.DB.Storages;
using Store.Core.Currencies;
using Store.Core.Enums;


namespace StoreRepository.Repositories
{
    public class OrderRepository : IOrderRepository
    {
        private IOrderStorage _orderStorage;
        private CurrencyRequest _sendRequest = new CurrencyRequest();
        public OrderRepository(IOrderStorage orderStorage)
        {
            _orderStorage = orderStorage;
        }


        public async ValueTask<RequestResult<Order>> AddOrder(Order model)
        {
            var result = new RequestResult<Order>();
            DateTime today = DateTime.Today;
            try
            {
                if (CurrencyRates.ActualCurrencyRates == null || CurrencyRates.ActualCurrencyRates.Find(i => Equals(i.Date, today)).Date == null)
                {
                    string path = "";
                    switch (model.Warehouse.Id)
                    {
                        case (int)WarehouseEnum.Minsk:
                            path = "BYN";
                            _sendRequest.GetLocalCurrency(path);
                            break;
                        case (int)WarehouseEnum.Kiev:
                            path = "UAH";
                            _sendRequest.GetLocalCurrency(path);
                            break;
                        default: break;
                    }

                }


                foreach (OrderDetails item in model.OrderDetails)
                {
                    item.LocalPrice /= CurrencyRates.ActualCurrencyRates.Find(i => Equals(i.Date, today)).Rate;
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
