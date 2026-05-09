using CRM.Domain.Entities.SupplyChainManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CRM.WebApp.Configurations.SupplyChainManagement
{
    public class PurchaseOrderItemConfiguration : IEntityTypeConfiguration<PurchaseOrderItem>
    {
        public void Configure(EntityTypeBuilder<PurchaseOrderItem> builder)
        {
            builder.ToTable("PurchaseOrderItems");

            builder.HasKey(x => x.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(1);

            builder.Property(poi => poi.Quantity)
                .IsRequired();
            builder.Property(poi => poi.Quantity)
                .HasConversion<int>()
                .HasColumnType("int");


                

           
            builder.Property(poi => poi.PurchaseOrderId)
                .IsRequired();




            builder.Property(poi => poi.ItemName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(poi => poi.ItemCode)
                .HasMaxLength(50);

            builder.Property(poi => poi.UnitPrice)
                .HasColumnType("decimal(18,2)");

            builder.Property(poi => poi.Description)
                .HasMaxLength(200);

            // Relationships
            builder.HasOne(poi => poi.PurchaseOrder)
                .WithMany(po => po.Items)
                .HasForeignKey(poi => poi.PurchaseOrderId)
                .OnDelete(DeleteBehavior.Cascade);
        }
    }

}
