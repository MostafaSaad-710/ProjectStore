using Microsoft.Extensions.Configuration;
using Domaine.Contracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Persistence.Data.Contexts;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using StackExchange.Redis;
using Persistence.Repositories;

namespace Persistence
{
    public static class InfrastructureServicesRegistration
    {
        public static IServiceCollection AddInfrastructureServices(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddDbContext<StoreDbContext>(option =>
            {
                option.UseSqlServer(configuration.GetConnectionString("DefualtConnection"));

            });
            services.AddScoped<IDbIntializer, DbIntializer>();
            services.AddScoped<IUnitofwork, Unitofwork>();
            services.AddScoped<IBasketRepository, BasketRepository>();
            services.AddScoped<ICacheRepository, CacheRepository>();

            services.AddSingleton<IConnectionMultiplexer>( (serviceProvider) => ConnectionMultiplexer.Connect(configuration.GetConnectionString("RedisConnection")) );

            return services;

        }
    }
}
