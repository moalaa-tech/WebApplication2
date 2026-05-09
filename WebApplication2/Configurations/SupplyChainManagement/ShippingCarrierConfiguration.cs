using CRM.Domain.Entities.SupplyChainManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.SupplyChainManagement
{
    public class ShippingCarrierConfiguration : IEntityTypeConfiguration<ShippingCarrier>
    {
        public void Configure(EntityTypeBuilder<ShippingCarrier> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(1);

            builder.Property(x => x.Name).IsRequired().HasMaxLength(100);
            builder.HasIndex(x => x.Name).IsUnique();
            builder.Property(x => x.SupportedServices)
                   .HasConversion(
                       v => string.Join(',', v),
                       v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
                   .HasColumnType("nvarchar(max)");

            builder.Property(x => x.SupportedCountries)
                     .HasConversion(
                          v => string.Join(',', v),
                          v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
                     .HasColumnType("nvarchar(max)");


            builder.Property(x => x.MaximumPackageWeight)
                     .HasColumnType("decimal(18,2)")
                     .IsRequired();


            builder.Property(x => x.PackageTypes)
                        .HasConversion(
                            v => string.Join(',', v),
                            v => v.Split(',', StringSplitOptions.RemoveEmptyEntries))
                        .HasColumnType("nvarchar(max)");

            // Additional configurations can be added here as needed



        }
    }
}
