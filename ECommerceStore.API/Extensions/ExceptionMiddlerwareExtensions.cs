using ECommerceStore.API.Middleware;

namespace ECommerceStore.API.Extensions
{
    public static class ExceptionMiddlerwareExtensions
    {
        public static void ConfigureCustomExceptionMiddleware(this IApplicationBuilder app)
        {
            app.UseMiddleware<ExceptionHandlerMiddleware>();
        }
    }
}
