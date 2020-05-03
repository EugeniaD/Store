using Store.Core;
using Store.DB.Models;
using StoreRepository.Common;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace StoreRepository.Repositories
{
    public interface IGoodsRepository
    {
        ValueTask<RequestResult<List<WarehouseBestSellingProduct>>> GetBestSellingProductByCity();
        ValueTask<RequestResult<List<Goods>>> GetGoodsWithCategoryReport(ReportTypeEnum reportType);
        ValueTask<RequestResult<List<СategoryProduct>>> GetСategoriesMoreThenXProducts(int amount);
        ValueTask<RequestResult<List<Goods>>> GoodsSearch(GoodsSearchModel model);
    }
}