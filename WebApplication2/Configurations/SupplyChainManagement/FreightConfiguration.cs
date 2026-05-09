using CRM.Domain.Entities.SupplyChainManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CRM.WebApp.Configurations.SupplyChainManagement
{
    public class FreightConfiguration : IEntityTypeConfiguration<Freight>
    {
        public void Configure(EntityTypeBuilder<Freight> builder)
        {
            builder.HasKey(f => f.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(1);



            builder.Property(f => f.Weight)
                .IsRequired()
                .HasColumnType("decimal(18,2)");


            builder.Property(f => f.PickupDate)
                .IsRequired();
            builder.Property(f => f.FreightNumber);

           
            builder.Property(f => f.Type)
                .IsRequired()
                .HasConversion<string>();


            builder.Property(f => f.DeliveryDate)
                .IsRequired();


            builder.Property(f => f.Volume)
                .IsRequired().HasConversion<decimal>()
                .HasColumnType("decimal(18,2)");

            builder.Property(f => f.FreightNumber);

            builder.HasOne(f => f.Shipping)
                .WithMany(s => s.Freights)
                .HasForeignKey(f => f.ShippingId)
                .OnDelete(DeleteBehavior.Cascade);


            builder.HasIndex(f => new { f.Type, f.PickupDate });


        }
    }
}
