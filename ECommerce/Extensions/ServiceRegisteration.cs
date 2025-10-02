using ECommerce.Factories;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;
using System.Text;

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
        public static IServiceCollection AddJwtService(this IServiceCollection services, IConfiguration _configuration)
        {
            services.AddAuthentication(Config =>
            {
                Config.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
                Config.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;

            })
            .AddJwtBearer(options =>
            {
                options.TokenValidationParameters = new()
                {
                    ValidateIssuer = true,
                    ValidIssuer = _configuration["JwtOptions:Issuer"],

                    ValidateAudience = true,
                    ValidAudience = _configuration["JwtOptions:Audience"],

                    ValidateLifetime = true,
                    IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JwtOptions:SecretKey"])),
                };
            });
            return services;
        }
    }
}
