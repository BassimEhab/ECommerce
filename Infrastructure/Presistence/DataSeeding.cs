using DomainLayer.Contracts;
using DomainLayer.Models.IdentityModule;
using DomainLayer.Models.ProductModule;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Presistence.Data;
using Presistence.Identity;
using System.Text.Json;

namespace Presistence
{
    public class DataSeeding(StoreDbContext _dbContext,
                            UserManager<ApplicationUser> _userManager,
                            RoleManager<IdentityRole> _roleManager,
                            StoreIdentityDbContext _identityDbContext) : IDataSeeding
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

        public async Task IdentityDataSeedAsync()
        {
            try
            {

                if (!_roleManager.Roles.Any())
                {
                    await _roleManager.CreateAsync(new IdentityRole("Admin"));
                    await _roleManager.CreateAsync(new IdentityRole("SuperAdmin"));
                }
                if (!_userManager.Users.Any())
                {
                    var User01 = new ApplicationUser()
                    {
                        Email = "Mohamed@gmail.com",
                        DisplayName = "Mohamed  Tarek",
                        PhoneNumber = "0123456789",
                        UserName = "MohamedTarek"
                    };
                    var User02 = new ApplicationUser()
                    {
                        Email = "Salma@gmail.com",
                        DisplayName = "Salma Mohamed",
                        PhoneNumber = "0123456789",
                        UserName = "SalmaMohamed"
                    };
                    await _userManager.CreateAsync(User01, "P@ssw0rd");
                    await _userManager.CreateAsync(User02, "P@ssw0rd");
                    await _userManager.AddToRoleAsync(User01, "Admin");
                    await _userManager.AddToRoleAsync(User02, "SuperAdmin");
                }
                await _identityDbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {


            }

        }
    }
}
