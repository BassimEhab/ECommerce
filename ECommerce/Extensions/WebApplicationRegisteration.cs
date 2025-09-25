using DomainLayer.Contracts;
using ECommerce.CustomMiddleWares;

namespace ECommerce.Extensions
{
    public static class WebApplicationRegisteration
    {
        public static async Task SeedDataAsync(this WebApplication app)
        {
            using var Scope = app.Services.CreateScope();
            var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
            await ObjectOfDataSeeding.DataSeedAsync();
        }

        public static IApplicationBuilder UseCustomExceptionMiddleWare(this IApplicationBuilder app)
        {
            app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
            return app;
        }
    }
}
