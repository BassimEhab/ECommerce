using ECommerce.Factories;
using Microsoft.AspNetCore.Mvc;

namespace ECommerce.Extensions
{
    public static class ServiceRegisteration
    {
        public static IServiceCollection AddWebApplicationServices(this IServiceCollection services)
        {
            services.Configure<ApiBehaviorOptions>((options) =>
             {
                 options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;
             });
            return services;
        }
    }
}
