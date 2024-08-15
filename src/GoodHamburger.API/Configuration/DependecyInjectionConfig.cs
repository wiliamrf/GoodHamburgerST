using GoodHamburger.Business.Interfaces;
using GoodHamburger.Business.Models;
using GoodHamburger.Business.Notifications;
using GoodHamburger.Business.Services;
using GoodHamburger.Data.Context;
using GoodHamburger.Data.Repository;
using System.Runtime.CompilerServices;

namespace GoodHamburger.API.Configuration
{
    public static class DependecyInjectionConfig
    { 

         // Injeção de dependencia de Servicos 
        public static IServiceCollection AddDependeciesInjection(this IServiceCollection services)
        {
            services.AddScoped<GHContext>();            
            services.AddScoped<IProductRepository, ProductRepository>();
            services.AddScoped<IOrderRepository,OrderRepository>();
            services.AddScoped<IOrderItemRepository,OrderItemRepository>();
            services.AddScoped<IOrderService,OrderService>();
            services.AddScoped<INotifier, Notifier>();
            services.AddTransient<SeedService>();




            return services;

        }
    }
}
