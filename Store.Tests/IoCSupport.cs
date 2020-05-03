using System;
using System.Collections.Generic;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using AutoMapper;
using Store.API.Configuration;
using Autofac.Core;
using Autofac;
using Store.Core.ConfigurationOptions;

namespace Store.IntegrationTests
{
    public class IoCSupport<TModule> where TModule : IModule, new()
    {
        private IContainer container;
        protected IMapper mapper;

        public IoCSupport()
        {
            var builder = new ContainerBuilder();
            var config = InitConfiguration();
            builder.RegisterModule(new TModule());
            var storageOptionsValue = config.Get<StorageOptions>();
            var storageOptions = Options.Create(storageOptionsValue);

            builder.RegisterInstance(storageOptions).As<IOptions<UrlOptions>>();

            var mappingConfig = new MapperConfiguration(c=> 
            {
                c.AddProfile(new AutomapperProfile());
            });

            mapper = mappingConfig.CreateMapper();
            builder.RegisterInstance(mapper).As<IMapper>();
            container = builder.Build();
        }
        
        protected TEntity Resolve<TEntity>()
        {
            return container.Resolve<TEntity>();
        }

        protected void ShutdownIoC()
        {
            container.Dispose();
        }
        
        public static IConfiguration InitConfiguration()
        {
            var config = new ConfigurationBuilder()
                .AddJsonFile("config.test.json")
                .Build();
            return config;
        }
    }
}
