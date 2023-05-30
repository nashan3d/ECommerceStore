using ECommerceStore.Core;
using ECommerceStore.Infrastrcuture;
using IdentityModel;
using Microsoft.IdentityModel.Tokens;

namespace ECommerceStore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            builder.Services.AddAuthentication("Bearer")
                 .AddJwtBearer("Bearer", options =>
                 {
                     options.Authority = "https://localhost:5443";

                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateAudience = false,
                         RoleClaimType = JwtClaimTypes.Role,
                         NameClaimType = JwtClaimTypes.Name,
                     };
                 });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", "ECommerceStore.API");
                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddCore();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }
            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers().RequireAuthorization("ApiScope");

            app.Run();
        }
    }
}