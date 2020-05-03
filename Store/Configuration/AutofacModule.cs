using Autofac;
using Store.DB.Storages;
using StoreRepository.Repositories;

namespace Store.API.Configuration
{
    public class AutofacModule: Module
    {
        protected override void Load(ContainerBuilder builder)
        {
            builder.RegisterType<OrderStorage>().As<IOrderStorage>();
            builder.RegisterType<OrderRepository>().As<IOrderRepository>();
            builder.RegisterType<GoodsRepository>().As<IGoodsRepository>();
            builder.RegisterType<GoodsStorage>().As<IGoodsStorage>();
        }
    }
}
