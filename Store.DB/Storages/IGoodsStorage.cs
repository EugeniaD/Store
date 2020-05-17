using Store.Core.Enums;
using Store.DB.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.DB.Storages
{
    public interface IGoodsStorage
    {
        ValueTask<List<WarehouseBestSellingProduct>> GetBestSellingProductByCity();
        ValueTask<List<Goods>> GetGoodsWithCategoryReport(ReportTypeEnum reportType);
        ValueTask<List<СategoryProduct>> GetСategoriesMoreThenXProducts(int amount);
        ValueTask<List<Goods>> GoodsSearch(GoodsSearchModel dataModel);
    }
}