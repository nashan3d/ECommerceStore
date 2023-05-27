using Microsoft.Extensions.DependencyInjection;
using System.Reflection;
using MediatR;


namespace ECommerceStore.Core
{
    public static class DependencyInjection
    {
        public static IServiceCollection AddCore(this IServiceCollection services) 
        {
            services.AddMediatR(cfg => cfg.RegisterServicesFromAssembly(Assembly.GetExecutingAssembly()));
            services.AddAutoMapper(Assembly.GetExecutingAssembly());

            return services;
        }
    }
}
