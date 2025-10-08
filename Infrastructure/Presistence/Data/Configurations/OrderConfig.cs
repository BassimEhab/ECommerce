using DomainLayer.Models.OrderModule;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Presistence.Data.Configurations
{
    internal class OrderConfig : IEntityTypeConfiguration<Order>
    {
        public void Configure(EntityTypeBuilder<Order> builder)
        {
            builder.ToTable("Orders");

            builder.Property(sub => sub.Subtotal)
                .HasColumnType("decimal(8,2)");

            builder.HasMany(oi => oi.Items)
                   .WithOne().OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(d => d.DeliveryMethod)
                .WithMany()
                   .HasForeignKey(d => d.DeliveryMethodId);

            builder.OwnsOne(o => o.shipToAddress);
        }
    }
}
