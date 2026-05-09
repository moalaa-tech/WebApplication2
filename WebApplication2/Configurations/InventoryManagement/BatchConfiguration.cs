using CRM.Domain.Entities.InventoryManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.InventoryManagement
{
    public class BatchConfiguration : IEntityTypeConfiguration<Batch>
    {
        public void Configure(EntityTypeBuilder<Batch> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.BatchNumber).IsRequired().HasMaxLength(100);
            builder.Property(b => b.InitialQuantity).HasColumnType("decimal(18,2)");
            builder.Property(b => b.QuantityOnHand).HasColumnType("decimal(18,2)");

            builder.HasOne(b => b.Product)
                   .WithMany(p => p.Batches)
                   .HasForeignKey(b => b.ProductId)
                   .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(b => b.ProductVariant)
                   .WithMany()
                   .HasForeignKey(b => b.ProductVariantId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(b => b.Warehouse)
                   .WithMany()
                   .HasForeignKey(b => b.WarehouseId)
                   .OnDelete(DeleteBehavior.Restrict);
        }
    }
}