using ECommerceStore.Core.Context;
using ECommerceStore.Infrastrcuture.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ECommerceStore.Infrastrcuture
{
    public static class DependencyInjection
    {

        public static IServiceCollection AddInfrastructure(this IServiceCollection services,IConfiguration config) 
        {

            services.AddDbContext<ECommerceStoreDbContext>(options => {
                options.UseSqlServer(config.GetConnectionString("SqlServerConnection"));

            }
            );

            services.AddScoped<IECommerceDbContext>(provider => provider.GetService<ECommerceStoreDbContext>());

            return services;
        }
    }
}
