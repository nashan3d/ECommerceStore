using ECommerceStore.IDP.Web.Data;
using ECommerceStore.IDP.Web.Model;
using ECommerceStore.IDP.Web.Services;
using IdentityServer4.Services;
using IdentityServerHost.Quickstart.UI;
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

            builder.Services.AddIdentityServer(options =>
            {
                // see https://identityserver4.readthedocs.io/en/latest/topics/resources.html
                options.EmitStaticAudienceClaim = true;
            })
                .AddInMemoryIdentityResources(Config.IdentityResources)
                .AddInMemoryApiScopes(Config.ApiScopes)
                .AddInMemoryClients(Config.Clients)
                .AddTestUsers(TestUsers.Users)                
                .AddDeveloperSigningCredential();

            builder.Services.AddControllersWithViews();
            builder.Services.AddRazorPages();



            var app = builder.Build();
            app.UseStaticFiles();
            app.UseRouting();
            app.UseIdentityServer();
           
            app.UseEndpoints(endpoints =>
            {
                endpoints.MapDefaultControllerRoute();
            });

            app.Run();
        }
    }
}