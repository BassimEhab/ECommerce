using DomainLayer.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Data.Configurations
{
    public class ProductConfig : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.HasOne(p => p.ProductBrand)
                   .WithMany()
                   .HasForeignKey(fk => fk.BrandId);
            builder.HasOne(p => p.ProductType)
                .WithMany()
                .HasForeignKey(fk => fk.TypeId);
            builder.Property(p => p.Price)
                .HasColumnType("decimal(10,2)");
        }
    }
}
