using ECommerceStore.API.Extensions;
using ECommerceStore.API.Middleware;
using ECommerceStore.Core;
using ECommerceStore.Infrastrcuture;
using IdentityModel;
using Microsoft.IdentityModel.Logging;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;

namespace ECommerceStore.API
{
    public class Program
    {
        public static void Main(string[] args)
        {
            var builder = WebApplication.CreateBuilder(args);

            // Add services to the container.

            builder.Services.AddControllers();

            IdentityModelEventSource.ShowPII = true;

            builder.Services.AddAuthentication("Bearer")
                 .AddJwtBearer("Bearer", options =>
                 {
                     options.Authority = "https://localhost:5443";
                     //options.Ca

                     options.TokenValidationParameters = new TokenValidationParameters
                     {
                         ValidateAudience = false,
                         RoleClaimType= "role",
                         NameClaimType= JwtClaimTypes.Name,
                         
                     };
                 });

            builder.Services.AddAuthorization(options =>
            {
                options.AddPolicy("ApiScope", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", new string[] { "ECommerceStore.API" ,"role","openid","profile"});
                   
                });

                options.AddPolicy("AdminPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", new string[] { "ECommerceStore.API", "role", "openid", "profile" });                  
                    policy.RequireClaim("UserRole", "Admin");

                });

                options.AddPolicy("UserPolicy", policy =>
                {
                    policy.RequireAuthenticatedUser();
                    policy.RequireClaim("scope", new string[] { "ECommerceStore.API", "role", "openid", "profile" });
                    policy.RequireClaim("UserRole", new List<string>{ "User","Admin"});                  

                });
            });

            // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
            builder.Services.AddEndpointsApiExplorer();
            builder.Services.AddSwaggerGen();

            builder.Services.AddInfrastructure(builder.Configuration);
            builder.Services.AddCore();

            builder.Services.AddScoped<ExceptionHandlerMiddleware>();

            var app = builder.Build();

            // Configure the HTTP request pipeline.
            if (app.Environment.IsDevelopment())
            {
                app.UseSwagger();
                app.UseSwaggerUI();
            }

            app.ConfigureCustomExceptionMiddleware();

            app.UseRouting();
            app.UseHttpsRedirection();

            app.UseAuthentication();
            app.UseAuthorization();


            app.MapControllers();

            app.Run();
        }
    }
}