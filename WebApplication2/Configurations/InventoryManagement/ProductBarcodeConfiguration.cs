using CRM.Domain.Entities.InventoryManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.InventoryManagement
{
    public class ProductBarcodeConfiguration : IEntityTypeConfiguration<ProductBarcode>
    {
        public void Configure(EntityTypeBuilder<ProductBarcode> builder)
        {
            builder.HasKey(b => b.Id);
            builder.Property(b => b.Code).IsRequired().HasMaxLength(64);
            builder.Property(b => b.Symbology).HasMaxLength(32);

            builder.HasIndex(b => b.Code).IsUnique();

            builder.HasOne(b => b.Product)
                   .WithMany(p => p.Barcodes)
                   .HasForeignKey(b => b.ProductId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(b => b.ProductVariant)
                   .WithMany()
                   .HasForeignKey(b => b.ProductVariantId)
                   .OnDelete(DeleteBehavior.NoAction);
        }
    }
}