using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;
using ServiceAbstraction;
using Services.AutoMapperProfile;

namespace Services
{
    public static class ServiceLayerConfigurations
    {
        public static IServiceCollection AddServiceConfig(this IServiceCollection Services)
        {
            Services.AddScoped<IServiceManager, ServiceManagerWithFactoryDelegate>();
            Services.AddAutoMapper(typeof(ProductProfile).Assembly);

            Services.AddScoped<IProductService, ProductService>();
            Services.AddScoped<Func<IProductService>>(x => () => x.GetRequiredService<IProductService>());
            Services.AddScoped<IBasketService, BasketService>();
            Services.AddScoped<Func<IBasketService>>(x => () => x.GetRequiredService<IBasketService>());
            Services.AddScoped<IAuthenticationServices, AuthenticationService>();
            Services.AddScoped<Func<IAuthenticationServices>>(x => () => x.GetRequiredService<IAuthenticationServices>());
            Services.AddScoped<IOrderServices, OrderServices>();
            Services.AddScoped<Func<IOrderServices>>(x => () => x.GetRequiredService<IOrderServices>());

            return Services;
        }
    }
}
