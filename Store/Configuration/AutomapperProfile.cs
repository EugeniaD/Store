using AutoMapper;
using Store.API.Models.InputModels;
using Store.DB.Models;
using Store.API.Models.OutputModels;

namespace Store.API.Configuration
{
    public class AutomapperProfile : Profile
    {
        public AutomapperProfile()
        {
            CreateMap<Warehouse, NamedOutputModel>();
            CreateMap<Category, NamedOutputModel>();


            CreateMap<Goods, GoodsOutputModel>()
                .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.Category.Name));
            
            CreateMap<Goods, GoodsWithCategoryOutputModel>()
                .ForMember(dest => dest.CategoryName, opt => opt.MapFrom(src => src.Category.Name)); 
            
            CreateMap<Order, OrderOutputModel>()
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString(@"dd.MM.yyyy")))
                .ForMember(dest => dest.WarehouseName, opt => opt.MapFrom(src => src.Warehouse.Name))
                .ForMember(dest => dest.OrderDetailsOutput, opt => opt.MapFrom(src => src.OrderDetails));

            CreateMap<Order, SalesByIsForeignOutputModel>();

            CreateMap<OrdersByDates, OrdersInTimePeriodOutputModel>()
                .ForMember(dest => dest.WarehouseName , opt => opt.MapFrom(src => src.Warehouse.Name))
                .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.Goods.Brand))
                .ForMember(dest => dest.ProductModel, opt => opt.MapFrom(src => src.Goods.Model))
                .ForMember(dest => dest.TotalQuantityGoods, opt => opt.MapFrom(src => src.TotalQuantity))
                .ForMember(dest => dest.TotalSum, opt => opt.MapFrom(src => src.TotalCost))
                .ForMember(dest => dest.ProductModel, opt => opt.MapFrom(src => src.Goods.Model))
                .ForMember(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.Goods.Category.Name))
                .ForMember(dest => dest.Date, opt => opt.MapFrom(src => src.Date.ToString(@"dd.MM.yyyy")));

            CreateMap<OrderDetails, OrderDetailsOutputModel>()
                .ForMember(dest => dest.ProductBrand, opt => opt.MapFrom(src => src.Goods.Brand))
                .ForMember(dest => dest.ProductModel, opt => opt.MapFrom(src => src.Goods.Model))
                .ForPath(dest => dest.SubCategoryName, opt => opt.MapFrom(src => src.Goods.Category.Name));


            CreateMap<WarehouseBestSellingProduct, BestSellingProductsOutputModel>();
            CreateMap<GoodsSearchInputModel, GoodsSearchModel>();

            CreateMap<WarehouseTotalCost, WarehouseTotalCostOutputModel>()
                .ForMember(dest => dest.WarehouseName, opt => opt.MapFrom(src => src.Name));

            CreateMap<СategoryProduct, СategoriesAndProductsAmountOutputModel>();

            CreateMap<OrderInputModel, Order>()
                 .ForMember(dest => dest.OrderDetails, opt => opt.MapFrom(src => src.OrderDetailsList))
                 .ForPath(dest => dest.Warehouse.Id, opt => opt.MapFrom(src => src.WarehouseId));

             CreateMap<OrderDetailsInputModel, OrderDetails>()
                .ForPath(dest => dest.Goods.Id, opt => opt.MapFrom(src => src.GoodsId));

        }
    }
}
