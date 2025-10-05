using DomainLayer.Models.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Data.Configurations
{
    internal class DeliveryMethodConfig : IEntityTypeConfiguration<DeliveryMethod>
    {
        public void Configure(EntityTypeBuilder<DeliveryMethod> builder)
        {
            builder.ToTable("DeliveryMethods");
            builder.Property(pr => pr.Price)
                   .HasColumnType("decimal(8,2)");

            builder.Property(sh => sh.ShortName)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

            builder.Property(de => de.Description)
                   .HasColumnType("varchar")
                   .HasMaxLength(100);

            builder.Property(del => del.DeliveryTime)
                   .HasColumnType("varchar")
                   .HasMaxLength(50);

        }
    }
}
