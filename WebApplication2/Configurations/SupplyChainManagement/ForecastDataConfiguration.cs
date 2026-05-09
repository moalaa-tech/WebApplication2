using CRM.Domain.Entities.SupplyChainManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.SupplyChainManagement
{
    public class ForecastDataConfiguration : IEntityTypeConfiguration<ForecastData>
    {
        public void Configure(EntityTypeBuilder<ForecastData> builder)
        {            
            builder.HasKey(x => x.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(1);
            builder.Property(x => x.DataType)
                .IsRequired()
                .HasMaxLength(50);



            builder.Property(x => x.DataDate)
                .IsRequired()
                .HasColumnType("datetime");


            builder.Property(x => x.Value)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            builder.Property(x => x.ValueDecimal).HasConversion<decimal>()
                .HasColumnType("decimal(18,2)")
                .IsRequired();




            builder.Property(x => x.Notes)
                .HasMaxLength(500);



            builder.HasOne(x => x.DemandPlan)
                .WithMany(x => x.ForecastData)
                .HasForeignKey(x => x.DemandPlanId)
                .OnDelete(DeleteBehavior.Cascade);



            builder.HasOne(x => x.SupplyChainEvent)
                .WithMany()
                .HasForeignKey(x => x.SupplyChainEventId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(x => x.Item)
                .WithMany()
                .HasForeignKey(x => x.ItemId)
                .OnDelete(DeleteBehavior.NoAction);



            builder.HasOne(x => x.Shipping)
                .WithMany()
                .HasForeignKey(x => x.ShippingId)
                .OnDelete(DeleteBehavior.NoAction);



            builder.HasOne(x => x.Freight)
                .WithMany()
                .HasForeignKey(x => x.FreightId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(x => x.HistoricalData)
                .WithMany()
                .HasForeignKey(x => x.HistoricalDataId)
                .OnDelete(DeleteBehavior.NoAction);



        }
    }
}
