using ECommerceStore.IDP.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceStore.IDP.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            var assembly = typeof(Program).Assembly.GetName().Name;
            var defaultConnection = builder.Configuration.GetConnectionString("SqlServerConnection");

            builder.Services.AddDbContext<AspNetIdentityDbContext>(options =>
                options.UseSqlServer(defaultConnection, b => b.MigrationsAssembly(assembly)));

            builder.Services.AddIdentity<IdentityUser, IdentityRole>()
                            .AddEntityFrameworkStores<AspNetIdentityDbContext>();

            builder.Services.AddIdentityServer()
                            .AddAspNetIdentity<IdentityUser>()
                            .AddConfigurationStore(options =>
                            {
                                options.ConfigureDbContext = b =>
                                    b.UseSqlServer(defaultConnection, opt => opt.MigrationsAssembly(assembly));
                            })
                            .AddOperationalStore(options =>
                            {
                                options.ConfigureDbContext = b =>
                                b.UseSqlServer(defaultConnection, opt => opt.MigrationsAssembly(assembly));
                            })
                            .AddDeveloperSigningCredential();


            var app = builder.Build();

            app.UseIdentityServer();

            app.Run();
        }
    }
}