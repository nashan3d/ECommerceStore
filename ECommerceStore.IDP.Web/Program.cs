using ECommerceStore.IDP.Web.Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace ECommerceStore.IDP.Web
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var seed = args.Contains("/seed");
            if (seed)
            {
                args = args.Except(new[] { "/seed" }).ToArray();
            }


            var builder = WebApplication.CreateBuilder(args);

            var assembly = typeof(Program).Assembly.GetName().Name;
            var defaultConnection = builder.Configuration.GetConnectionString("SqlServerConnection");

            if (seed)
            {
                SeedData.EnsureSeedData(defaultConnection);
            }

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

            builder.Services.AddControllersWithViews();

            var app = builder.Build();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
            app.UseAuthorization();
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.Run();
        }
    }
}