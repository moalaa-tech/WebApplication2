using CRM.Domain.Entities.InventoryManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.InventoryManagement
{
    public class PurchaseOrderLineConfiguration : IEntityTypeConfiguration<PurchaseOrderLine>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderLine> builder)
        {
            builder.HasKey(l => l.Id);
            builder.Property(l => l.Quantity).HasColumnType("decimal(18,2)");
            builder.Property(l => l.UnitPrice).HasColumnType("decimal(18,2)");
        }
    }
}
