using Microsoft.AspNetCore.Mvc;
using Store.API.Models.InputModels;
using Store.API.Models.OutputModels;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Store.API.Controllers
{
    public interface IGoodsController
    {
        ValueTask<ActionResult<List<BestSellingProductsOutputModel>>> GetBestSellingProductByCity();
        ValueTask<ActionResult<List<СategoriesAndProductsAmountOutputModel>>> GetСategoriesMoreThenXProducts(int amount);
        ValueTask<ActionResult<List<GoodsWithCategoryOutputModel>>> GetProductsNoOneWants();
        ValueTask<ActionResult<List<GoodsWithCategoryOutputModel>>> GetProductOutOfStock();
        ValueTask<ActionResult<List<GoodsWithCategoryOutputModel>>> GetProductIsInStorageNoInSPBMoscow();
        ValueTask<ActionResult<List<GoodsOutputModel>>> GoodsSearch(GoodsSearchInputModel inputModel);
    }
}