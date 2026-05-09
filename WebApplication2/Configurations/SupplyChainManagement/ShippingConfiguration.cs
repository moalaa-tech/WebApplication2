using CRM.Domain.Entities.SupplyChainManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CRM.WebApp.Configurations.SupplyChainManagement
{
    public class ShippingConfiguration : IEntityTypeConfiguration<Shipping>
    {
        public void Configure(EntityTypeBuilder<Shipping> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(1);



            builder.Property(x => x.ShippingNumber).IsRequired().HasMaxLength(50);
            builder.Property(x => x.Method).HasConversion<string>();
            builder.Property(x => x.Status).HasConversion<string>();
            builder.HasIndex(x => x.ShippingNumber).IsUnique();

            builder.HasOne(x => x.Logistics)
                   .WithMany()
                   .HasForeignKey(x => x.LogisticsId);


            builder.Property(a => a.Cost).HasConversion<decimal>()
                   .HasColumnType("decimal(18,2)")
                   .IsRequired();


            builder.HasIndex(s => new { s.Status, s.ShippingDate });


            // add configuration for cost 









        }
    }
}
