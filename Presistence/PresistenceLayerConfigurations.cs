using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;
using Domain.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Presistence;
using Presistence.Repositories;
using Presistence.UnitOfWork;
using ServiceAbstraction;
using Services.AutoMapperProfile;
using StackExchange.Redis;

namespace Services
{
    public static class PresistenceLayerConfigurations
    {
        public static IServiceCollection AddPresistenceConfig(this IServiceCollection Services, IConfiguration _configuration)
        {
            Services.AddDbContext<Presistence.Data.ApplicationDbContext>(options =>
                options.UseSqlServer(_configuration.GetConnectionString("DefaultConnection")));
            Services.AddScoped<IDataSeeding, DataSeeding>();
            Services.AddScoped<IUnitOfWork, UnitOfWork>();
            Services.AddScoped<IBasketRepository, BasketRepository>();
            Services.AddSingleton<IConnectionMultiplexer>( (_) =>
            {
                return ConnectionMultiplexer.Connect(_configuration.GetConnectionString("RedisConnection"));
            });
            return Services;
        }
    }
}
