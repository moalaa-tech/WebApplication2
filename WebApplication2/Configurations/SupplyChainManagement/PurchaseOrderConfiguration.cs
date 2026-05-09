using CRM.Domain.Entities.SupplyChainManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.SupplyChainManagement
{
    public class PurchaseOrderConfiguration : IEntityTypeConfiguration<PurchaseOrder>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrder> builder)
        {
            builder.ToTable("PurchaseOrders");

            builder.HasKey(x => x.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(1);

            builder.Property(po => po.PONumber)
                .IsRequired()
                .HasMaxLength(20);

            builder.Property(po => po.Status)
                .HasMaxLength(20)
                .HasDefaultValue("Draft");

            builder.Property(po => po.TotalAmount)
                .HasColumnType("decimal(18,2)");

           
           
            // Relationships
            builder.HasOne(po => po.Supplier)
                .WithMany(s => s.PurchaseOrders)
                .HasForeignKey(po => po.SupplierId)
                .OnDelete(DeleteBehavior.Restrict);
            // Additional Indexes for performance optimization



             // Indexes
            builder.HasIndex(po => po.PONumber)
                .IsUnique();

            builder.HasIndex(po => po.SupplierId);
            builder.HasIndex(po => po.Status);
            builder.HasIndex(po => po.OrderDate);

        }
    }
}
