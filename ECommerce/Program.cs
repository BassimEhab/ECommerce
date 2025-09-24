using DomainLayer.Contracts;
using ECommerce.CustomMiddleWares;
using ECommerce.Factories;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Presistence;
using Presistence.Data;
using Presistence.Repositories;
using Service;
using Service.MappingProfiles;
using ServiceAbstraction;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

#region DbContextDI
builder.Services.AddDbContext<StoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection"));
});
#endregion

#region DataSeedingDI
builder.Services.AddScoped<IDataSeeding, DataSeeding>();
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();
#endregion

#region AutoMapperDI
builder.Services.AddAutoMapper(cfg => { }, typeof(ProductProfile).Assembly);
#endregion

#region ServiceDI
builder.Services.AddScoped<IServiceManager, ServiceManager>();
#endregion

builder.Services.Configure<ApiBehaviorOptions>((options) =>
{
    options.InvalidModelStateResponseFactory = ApiResponseFactory.GenerateApiValidationErrorResponse;
});
var app = builder.Build();

#region DataSeeding
using var Scope = app.Services.CreateScope();
var ObjectOfDataSeeding = Scope.ServiceProvider.GetRequiredService<IDataSeeding>();
ObjectOfDataSeeding.DataSeed();
#endregion
app.UseMiddleware<CustomExceptionHandlerMiddleWare>();
// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
// To use Static Files *product images*
app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();
