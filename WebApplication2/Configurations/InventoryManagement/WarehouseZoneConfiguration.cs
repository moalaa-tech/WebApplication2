using CRM.Domain.Entities.InventoryManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.InventoryManagement
{
    public class WarehouseZoneConfiguration : IEntityTypeConfiguration<WarehouseZone>
    {
        public void Configure(EntityTypeBuilder<WarehouseZone> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(150);
            builder.Property(a => a.NameAR).HasMaxLength(150);
            builder.HasOne(a => a.Warehouse)
                   .WithMany(w => w.Zones)
                   .HasForeignKey(a => a.WarehouseId)
                   .OnDelete(DeleteBehavior.Cascade);



        }
    }
}
