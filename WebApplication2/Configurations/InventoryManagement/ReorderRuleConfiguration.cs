using CRM.Domain.Entities.InventoryManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.InventoryManagement
{
    public class ReorderRuleConfiguration : IEntityTypeConfiguration<ReorderRule>
    {
        public void Configure(EntityTypeBuilder<ReorderRule> builder)
        {
            builder.HasKey(r => r.Id);
            builder.Property(r => r.MinQty).HasColumnType("decimal(18,2)");
            builder.Property(r => r.MaxQty).HasColumnType("decimal(18,2)");
            builder.Property(r => r.ReorderQty).HasColumnType("decimal(18,2)");


            builder.HasOne<Product>(r => r.Item)
            .WithOne(i => i.ReorderRule)
            .HasForeignKey<ReorderRule>(r => r.ItemId);
        }
    }
}
