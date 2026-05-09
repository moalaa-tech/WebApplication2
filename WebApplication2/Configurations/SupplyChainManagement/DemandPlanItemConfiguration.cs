using CRM.Domain.Entities.SupplyChainManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.SupplyChainManagement
{
    public class DemandPlanItemConfiguration : IEntityTypeConfiguration<DemandPlanItem>
    {
        public void Configure(EntityTypeBuilder<DemandPlanItem> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(1);

            builder.Property(x => x.Quantity).IsRequired().HasConversion<int>()
                   .HasColumnType("int");


            builder.HasOne(x => x.DemandPlan)
                   .WithMany(p => p.Items)
                   .HasForeignKey(x => x.DemandPlanId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.Property(x => x.RequiredDate)
                .IsRequired()
                .HasColumnType("datetime");


            builder.Property(x => x.CostType)
                .HasConversion<string>()
                .IsRequired();

            builder.Property(x => x.EstimatedCost)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(x => x.Priority)
                .IsRequired();

            builder.Property(x => x.Notes)
                .HasMaxLength(500)
                .IsRequired(false);

            builder.Property(x => x.IsProcured)
                .IsRequired()
                .HasDefaultValue(false);

            builder.Property(x => x.ProductName)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(x => x.ProductId)
                                .IsRequired();

            builder.Property(x => x.ProductCode)
                .IsRequired()
                .HasMaxLength(50);


            //builder.HasIndex(x => new { x.DemandPlanId, x.ProductId })
            //    .IsUnique()
            //    .HasDatabaseName("IX_DemandPlanItem_DemandPlanId_ProductId");

            //builder.HasIndex(x => x.ProductCode)
            //    .IsUnique()
            //    .HasDatabaseName("IX_DemandPlanItem_ProductCode");


            //builder.HasIndex(x => x.ProductName)
            //    .IsUnique()
            //    .HasDatabaseName("IX_DemandPlanItem_ProductName");


            //builder.HasIndex(x => new { x.DemandPlanId, x.RequiredDate })
            //    .HasDatabaseName("IX_DemandPlanItem_DemandPlanId_RequiredDate");



            //builder.HasIndex(x => new { x.DemandPlanId, x.Priority })
            //    .HasDatabaseName("IX_DemandPlanItem_DemandPlanId_Priority");

        }
    }
}
