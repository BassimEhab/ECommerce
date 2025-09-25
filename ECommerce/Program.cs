using ECommerce.Extensions;
using Presistence;
using Service;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddInfrastructureServices(builder.Configuration);
builder.Services.AddApplicationServices();
builder.Services.AddWebApplicationServices();

var app = builder.Build();
await app.SeedDataAsync();

app.UseCustomExceptionMiddleWare();
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
