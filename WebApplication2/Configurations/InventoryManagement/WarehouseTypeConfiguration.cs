using CRM.Domain.Entities.InventoryManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.InventoryManagement
{
    public class WarehouseTypeConfiguration : IEntityTypeConfiguration<WarehouseType>
    {
        public void Configure(EntityTypeBuilder<WarehouseType> builder)
        {
            builder.ToTable("WarehouseTypes");

            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(t => t.Name)
                   .IsRequired()
                   .HasMaxLength(150);

            builder.Property(t => t.NameAr)
                   .IsRequired()
                   .HasMaxLength(150);

           

        
        }
    }

}
