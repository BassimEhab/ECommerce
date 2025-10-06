using ECommerce.Extensions;
using Microsoft.OpenApi.Models;
using Presistence;
using Service;
using Swashbuckle.AspNetCore.SwaggerUI;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(options =>
{
    options.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme()
    {
        In = ParameterLocation.Header,
        Name = "Authorization",
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        Description = "Enter 'Bearer' Followed By Space And Your Token"
    });
    options.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Id = "Bearer",
                    Type = ReferenceType.SecurityScheme
                }
            },
            new string[] {}
        }
    });
});

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddWebApplicationServices();
builder.Services.AddJwtService(builder.Configuration);
var app = builder.Build();
await app.SeedDataAsync();

app.UseCustomExceptionMiddleWare();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI(options =>
    {
        options.DisplayRequestDuration();
        options.DocumentTitle = "My E-commerce API";
        options.DocExpansion(DocExpansion.None);
        options.EnableFilter();
        options.EnablePersistAuthorization();
    });
}

app.UseHttpsRedirection();
// To use Static Files *product images*
app.UseStaticFiles();
app.UseRouting();
app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
