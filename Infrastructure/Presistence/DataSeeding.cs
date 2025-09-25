using DomainLayer.Contracts;
using DomainLayer.Models.ProductModule;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using System.Text.Json;

namespace Presistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public async Task DataSeedAsync()
        {
            try
            {
                // Apply any pending migrations
                var pendingMigrations = await _dbContext.Database.GetPendingMigrationsAsync();
                if (pendingMigrations.Any())
                    await _dbContext.Database.MigrateAsync();

                if (!_dbContext.ProductBrands.Any())
                {
                    var ProductBrandData = File.OpenRead(@"..\Infrastructure\Presistence\Data\DataSeed\brands.json");
                    // Convert to C# objects (ProductBrand)
                    var ProductBrands = await JsonSerializer.DeserializeAsync<List<ProductBrand>>(ProductBrandData);
                    // Add Data to the Database
                    if (ProductBrands is not null && ProductBrands.Any())
                        await _dbContext.ProductBrands.AddRangeAsync(ProductBrands);
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.OpenRead(@"..\Infrastructure\Presistence\Data\DataSeed\types.json");
                    // convert to C# objects (ProductType)
                    var ProductTypes = await JsonSerializer.DeserializeAsync<List<ProductType>>(ProductTypeData);
                    // Add Data to the Database
                    if (ProductTypes is not null && ProductTypes.Any())
                        await _dbContext.ProductTypes.AddRangeAsync(ProductTypes);
                }
                if (!_dbContext.Products.Any())
                {
                    var ProductData = File.OpenRead(@"..\Infrastructure\Presistence\Data\DataSeed\products.json");
                    // convert to C# objects (Product)
                    var Products = await JsonSerializer.DeserializeAsync<List<Product>>(ProductData);
                    // Add Data to the Database
                    if (Products is not null && Products.Any())
                        await _dbContext.Products.AddRangeAsync(Products);
                }
                // Save changes to the Database
                await _dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {

                //To do
            }
        }
    }
}
