using CRM.Domain.Entities.InventoryManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.InventoryManagement
{
    public class WarehouseConfiguration : IEntityTypeConfiguration<Warehouse>
    {
        public void Configure(EntityTypeBuilder<Warehouse> builder)
        {
            builder.HasKey(w => w.Id);
            builder.Property(w => w.Name).IsRequired().HasMaxLength(150);
            builder.Property(w => w.Location).HasMaxLength(250);


            builder.HasMany(a => a.StockTransactions)
            .WithOne(s => s.Warehouse)
            .HasForeignKey(s => s.WarehouseId);
        }
    }
}
