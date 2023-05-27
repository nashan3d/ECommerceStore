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

            builder.Services.AddIdentityServer()
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