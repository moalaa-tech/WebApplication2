using CRM.Domain.Entities.SupplyChainManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.SupplyChainManagement
{
    public class DemandPlanConfiguration : IEntityTypeConfiguration<DemandPlan>
    {
        public void Configure(EntityTypeBuilder<DemandPlan> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(1);



            builder.Property(x => x.PlanName).IsRequired().HasMaxLength(100);
            builder.Property(x => x.Status).HasConversion<string>();
            builder.HasIndex(x => x.PlanName).IsUnique();

            builder.HasMany(x => x.Items)
                   .WithOne()
                   .HasForeignKey(x => x.DemandPlanId).OnDelete(DeleteBehavior.NoAction);
                       
            builder.HasMany(x => x.ForecastData)
                   .WithOne()
                   .HasForeignKey(x => x.DemandPlanId);

            builder.HasMany(x => x.SupplyChainEvents).WithOne()
                   .HasForeignKey(x => x.DemandPlanId);

            builder.HasMany(x => x.HistoricalData)
                     .WithOne()
                     .HasForeignKey(x => x.DemandPlanId);


            builder.Property(x => x.StartDate)
                .IsRequired()
                .HasColumnType("datetime");


            builder.Property(x => x.EndDate)
                .IsRequired()
                .HasColumnType("datetime");
                    

        }
    }
}
