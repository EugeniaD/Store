using Store.Core;
using Store.DB.Models;
using Store.DB.Storages;
using StoreRepository.Common;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreRepository.Repositories
{
    public class GoodsRepository : IGoodsRepository
    {
        private IGoodsStorage _goodsStorage;

        public GoodsRepository(IGoodsStorage goodsStorage)
        {
            _goodsStorage = goodsStorage;
        }

        public async ValueTask<RequestResult<List<WarehouseBestSellingProduct>>> GetBestSellingProductByCity()
        {
            var result = new RequestResult<List<WarehouseBestSellingProduct>>();
            try
            {
                result.RequestData = await _goodsStorage.GetBestSellingProductByCity();
                result.IsOk = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<List<СategoryProduct>>> GetСategoriesMoreThenXProducts(int amount)
        {
            var result = new RequestResult<List<СategoryProduct>>();
            try
            {
                result.RequestData = await _goodsStorage.GetСategoriesMoreThenXProducts(amount);
                result.IsOk = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }

        public async ValueTask<RequestResult<List<Goods>>> GetGoodsWithCategoryReport(ReportTypeEnum reportType)
        {
            var result = new RequestResult<List<Goods>>();
            try
            {
                result.RequestData = await _goodsStorage.GetGoodsWithCategoryReport(reportType);
                result.IsOk = true;
            }
            catch (Exception ex)
            {
                result.ExMessage = ex.Message;
            }
            return result;
        }
        
        public async ValueTask<RequestResult<List<Goods>>> GoodsSearch(GoodsSearchModel model)
        {
            var result = new RequestResult<List<Goods>>();
            try
            {
                result.RequestData = await _goodsStorage.GoodsSearch(model);
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
