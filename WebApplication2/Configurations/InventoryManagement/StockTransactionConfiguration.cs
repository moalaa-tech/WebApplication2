using CRM.Domain.Entities.InventoryManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.InventoryManagement
{
    public class StockTransactionConfiguration : IEntityTypeConfiguration<StockTransaction>
    {
        public void Configure(EntityTypeBuilder<StockTransaction> builder)
        {
            builder.HasKey(s => s.Id);
            builder.Property(s => s.TransactionType).IsRequired();
            builder.Property(s => s.Quantity).HasColumnType("decimal(18,2)");
            builder.Property(s => s.Reference).HasMaxLength(100);
            builder.Property(s => s.Note).HasMaxLength(500);
            builder.Property(s => s.ScannedBarcode).HasMaxLength(128);

            builder.HasOne(s => s.Item)
            .WithMany(i => i.StockTransactions)
            .HasForeignKey(s => s.ItemId);

            builder.HasOne(s => s.Warehouse)
            .WithMany(w => w.StockTransactions)
            .HasForeignKey(s => s.WarehouseId);

            builder.HasOne(s => s.Batch)
            .WithMany(b => b.StockTransactions)
            .HasForeignKey(s => s.BatchId)
            .OnDelete(DeleteBehavior.SetNull);
        }
    }
}
