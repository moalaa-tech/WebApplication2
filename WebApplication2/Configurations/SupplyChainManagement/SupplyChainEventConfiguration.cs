using CRM.Domain.Entities.SupplyChainManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.SupplyChainManagement
{
    public class SupplyChainEventConfiguration : IEntityTypeConfiguration<SupplyChainEvent>
    {
        public void Configure(EntityTypeBuilder<SupplyChainEvent> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(1);


            builder.Property(x => x.Description)
                .IsRequired()
                .HasMaxLength(500);


            builder.Property(x => x.EventDate)
                .IsRequired()
                .HasColumnType("datetime");

            builder.HasOne(x => x.DemandPlan)
                   .WithMany(p => p.SupplyChainEvents)
                   .HasForeignKey(x => x.DemandPlanId)
                   .OnDelete(DeleteBehavior.Cascade);

            builder.HasMany(x => x.Details)
                     .WithOne()
                     .HasForeignKey(x => x.SupplyChainEventId)
                     .OnDelete(DeleteBehavior.Cascade);

            builder.Property(x => x.EventName)
                .IsRequired()
                .HasMaxLength(200);


            builder.HasIndex(x => new { x.EventName, x.EventDate })
                   .IsUnique()
                   .HasDatabaseName("IX_SupplyChainEvent_EventName_EventDate");
            builder.HasIndex(x => x.DemandPlanId)
                     .HasDatabaseName("IX_SupplyChainEvent_DemandPlanId");


            builder.Property(x => x.DemandPlanId)
                .IsRequired()
                .HasColumnName("DemandPlanId")
                .HasComment("Foreign key to DemandPlan entity");
          
        }
    }
}
