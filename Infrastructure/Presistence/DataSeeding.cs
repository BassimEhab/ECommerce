using DomainLayer.Contracts;
using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using System.Text.Json;

namespace Presistence
{
    public class DataSeeding(StoreDbContext _dbContext) : IDataSeeding
    {
        public void DataSeed()
        {
            try
            {
                // Apply any pending migrations
                if (_dbContext.Database.GetPendingMigrations().Any())
                    _dbContext.Database.Migrate();

                if (!_dbContext.ProductBrands.Any())
                {
                    var ProductBrandData = File.ReadAllText(@"..\Infrastructure\Presistence\Data\DataSeed\brands.json");
                    // Convert to C# objects (ProductBrand)
                    var ProductBrands = JsonSerializer.Deserialize<List<ProductBrand>>(ProductBrandData);
                    // Add Data to the Database
                    if (ProductBrands is not null && ProductBrands.Any())
                        _dbContext.ProductBrands.AddRange(ProductBrands);
                }
                if (!_dbContext.ProductTypes.Any())
                {
                    var ProductTypeData = File.ReadAllText(@"..\Infrastructure\Presistence\Data\DataSeed\types.json");
                    // convert to C# objects (ProductType)
                    var ProductTypes = JsonSerializer.Deserialize<List<ProductType>>(ProductTypeData);
                    // Add Data to the Database
                    if (ProductTypes is not null && ProductTypes.Any())
                        _dbContext.ProductTypes.AddRange(ProductTypes);
                }
                if (!_dbContext.Products.Any())
                {
                    var ProductData = File.ReadAllText(@"..\Infrastructure\Presistence\Data\DataSeed\products.json");
                    // convert to C# objects (Product)
                    var Products = JsonSerializer.Deserialize<List<Product>>(ProductData);
                    // Add Data to the Database
                    if (Products is not null && Products.Any())
                        _dbContext.Products.AddRange(Products);
                }
                // Save changes to the Database
                _dbContext.SaveChanges();
            }
            catch (Exception ex)
            {

                //To do
            }
        }
    }
}
