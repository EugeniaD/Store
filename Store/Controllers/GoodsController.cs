using System.Collections.Generic;
using System.Threading.Tasks;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using Store.API.Models.InputModels;
using Store.API.Models.OutputModels;
using Store.Core;
using Store.DB.Models;
using StoreRepository.Repositories;

namespace Store.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoodsController : ControllerBase, IGoodsController
    {
        private readonly IMapper _mapper;
        private readonly IGoodsRepository _goodsRepository;

        public GoodsController(IMapper mapper, IGoodsRepository goodsRepository)
        {
            _mapper = mapper;
            _goodsRepository = goodsRepository;
        }


        // GET: api/goods/best-selling/by-city
        [HttpGet("best-selling/by-city")] 
        public async ValueTask<ActionResult<List<BestSellingProductsOutputModel>>> GetBestSellingProductByCity()
        {
            var result = await _goodsRepository.GetBestSellingProductByCity();
            if (result.IsOk)
            {
                if (result.RequestData == null) return NotFound("Products not found");
                return Ok(_mapper.Map<List<BestSellingProductsOutputModel>>(result.RequestData));
            }
            return Problem($"Geting products failed {result.ExMessage}", statusCode: 520);
        }

        // GET: api/goods/storage/available/moscow/not-available/spb/not-available
        [HttpGet("storage/available/moscow/not-available/spb/not-available")] 
        public async ValueTask<ActionResult<List<GoodsWithCategoryOutputModel>>> GetProductIsInStorageNoInSPBMoscow()
        {
            ReportTypeEnum reportType = ReportTypeEnum.GetProductIsInStorageNoInSPBMoscow;
            var result = await _goodsRepository.GetGoodsWithCategoryReport(reportType);
            if (result.IsOk)
            {
                if (result.RequestData == null) return NotFound("Products not found");
                return Ok(_mapper.Map<List<GoodsWithCategoryOutputModel>>(result.RequestData));
            }
            return Problem($"Geting products failed {result.ExMessage}", statusCode: 520); ;
        }

        // GET: api/goods/never-ordered
        [HttpGet("never-ordered")] 
        public async ValueTask<ActionResult<List<GoodsWithCategoryOutputModel>>> GetProductsNoOneWants()
        {
            ReportTypeEnum reportType = ReportTypeEnum.GetProductsNoOneWants;
            var result = await _goodsRepository.GetGoodsWithCategoryReport(reportType);
            if (result.IsOk)
            {
                if (result.RequestData == null) return NotFound("Products not found");
                return Ok(_mapper.Map<List<GoodsWithCategoryOutputModel>>(result.RequestData));
            }
            return Problem($"Geting products failed {result.ExMessage}", statusCode: 520); ;
        }

        // GET: api/goods/out-of-stock
        [HttpGet("out-of-stock")] 
        public async ValueTask<ActionResult<List<GoodsWithCategoryOutputModel>>> GetProductOutOfStock()
        {
            ReportTypeEnum reportType = ReportTypeEnum.GetProductOutOfStock;
            var result = await _goodsRepository.GetGoodsWithCategoryReport(reportType);
            if (result.IsOk)
            {
                if (result.RequestData == null) return NotFound("Products not found");
                return Ok(_mapper.Map<List<GoodsWithCategoryOutputModel>>(result.RequestData));
            }
            return Problem($"Geting products failed {result.ExMessage}", statusCode: 520); ;
        }

        // GET: api/goods/categories/more-than/2000
        [HttpGet("categories/more-than/{number}")] 
        public async ValueTask<ActionResult<List<СategoriesAndProductsAmountOutputModel>>> GetСategoriesMoreThenXProducts(int number)
        {
            if (number <= 0) return BadRequest("Number of goods must be greater than 0");

            var result = await _goodsRepository.GetСategoriesMoreThenXProducts(number);
            if (result.IsOk)
            {
                if (result.RequestData == null) return NotFound("Сategories not found");
                return Ok(_mapper.Map<List<СategoriesAndProductsAmountOutputModel>>(result.RequestData));
            }
            return Problem($"Geting categories failed {result.ExMessage}", statusCode: 520); ;
        }

        // GET: api/goods/search
        [HttpGet("search")]
        public async ValueTask<ActionResult<List<GoodsOutputModel>>> GoodsSearch(GoodsSearchInputModel inputModel)
        {
            if (inputModel.Id != null && inputModel.Id < 1) return BadRequest("Order Id must be greater than 1");
            if (inputModel.CategoryId != null && inputModel.CategoryId < 1) return BadRequest("Category Id must be greater than 1");
            if (inputModel.SubcategoryId != null && inputModel.SubcategoryId < 1) return BadRequest("Subcategory Id must be greater than 1");
            if (inputModel.Price != null && inputModel.Price < 0) return BadRequest("Price must be greater than 0");
           
            if (inputModel.Id != null && inputModel.Id < 1) return BadRequest("Order Id must be greater than 1");
            var result = await _goodsRepository.GoodsSearch(_mapper.Map<GoodsSearchModel>(inputModel));

            if (result.IsOk)
            {
                if (result.RequestData == null) return NotFound("Product not found");
                return Ok(_mapper.Map<List<GoodsOutputModel>>(result.RequestData));
            }
            return Problem($"Geting products failed {result.ExMessage}", statusCode: 520); ;
        }
    }
}
