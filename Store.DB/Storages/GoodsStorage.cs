using Dapper;
using Store.Core;
using Store.DB.Models;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.Extensions.Options;
using Store.Core.ConfigurationOptions;

namespace Store.DB.Storages
{
    public class GoodsStorage : IGoodsStorage
    {
        private IDbConnection connection;

        public GoodsStorage(IOptions<StorageOptions> storageOptions)
        {
            this.connection = new SqlConnection(storageOptions.Value.DBConnectionString);
        }

        internal static class SpName
        {
            public const string ProductBestSellingByCites = "Product_BestSellingByCites";
            public const string ProductNoOneWants = "Product_NoOneWants";
            public const string ProductOutOfStock = "Product_OutOfStock";
            public const string ProductIsInStorageNoInSPBMoscow = "Product_IsInStorage_NoInSPBMoscow";
            public const string СategoryMoreThenXProducts = "Сategory_MoreThenXProducts";
            public const string GoodsSearch = "Goods_Search";
        }

        public async ValueTask<List<WarehouseBestSellingProduct>> GetBestSellingProductByCity()
        {
            var result = await connection.QueryAsync<WarehouseBestSellingProduct, int, WarehouseBestSellingProduct>(
                    SpName.ProductBestSellingByCites,
                   (w, g) =>
                   {
                       WarehouseBestSellingProduct warehouse = w;
                       warehouse.GoodsId = g;
                       return warehouse;
                   },
                    param: null,
                    commandType: CommandType.StoredProcedure,
                    splitOn: "GoodsId");
            return result.ToList();
        }

        public async ValueTask<List<Goods>> GetGoodsWithCategoryReport(ReportTypeEnum reportType)
        {
            string proc = "";
            switch (reportType)
            {
                case ReportTypeEnum.GetProductsNoOneWants:
                    proc = SpName.ProductNoOneWants;
                    break;
                case ReportTypeEnum.GetProductOutOfStock:
                    proc = SpName.ProductOutOfStock;
                    break;
                case ReportTypeEnum.GetProductIsInStorageNoInSPBMoscow:
                    proc = SpName.ProductIsInStorageNoInSPBMoscow;
                    break;
            }

            var result = await connection.QueryAsync<Category, Goods, Goods>(
                proc,
                (c, g) =>
                {
                    Goods goods = g;
                    goods.Category = c;
                    return goods;
                },
                param: null,
                commandType: CommandType.StoredProcedure);
            return result.ToList();
        }

        public async ValueTask<List<СategoryProduct>> GetСategoriesMoreThenXProducts(int amount)
        {
            var result = await connection.QueryAsync<СategoryProduct, int, СategoryProduct>(
                SpName.СategoryMoreThenXProducts,
                (c, cp) =>
                {
                    СategoryProduct category = c;
                    category.CountProducts = cp;
                    return category;
                },
                param: new { amount },
                commandType: CommandType.StoredProcedure,
                splitOn: "CountProducts");
            return result.ToList();
        }


        public async ValueTask<List<Goods>> GoodsSearch(GoodsSearchModel dataModel)
        {
            DynamicParameters parameters = new DynamicParameters(new
            {
                dataModel.Id,
                dataModel.Brand,
                dataModel.Model,
                dataModel.Price,
                dataModel.CategoryId,
                dataModel.SubcategoryId
            });
            var result = await connection.QueryAsync<Goods, Category, Goods>(
                SpName.GoodsSearch,
                (g, c) =>
                {
                    Goods goods = g;
                    goods.Category = c;
                    return goods;
                },
                param: parameters,
                commandType: CommandType.StoredProcedure);
            return result.ToList();
        }
    }
}
