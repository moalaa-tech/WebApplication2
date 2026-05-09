using CRM.Domain.Entities.SupplyChainManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.SupplyChainManagement
{
    public class LabelRecordConfiguration : IEntityTypeConfiguration<LabelRecord>
    {
        public void Configure(EntityTypeBuilder<LabelRecord> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(1);

            builder.HasKey(x => x.Id);

            builder.Property(x => x.TrackingNumber).IsRequired().HasMaxLength(100);
            builder.HasIndex(x => x.TrackingNumber).IsUnique();
            builder.Property(x => x.CarrierService).IsRequired().HasMaxLength(100);
            builder.Property(x => x.LabelData).IsRequired();

            builder.Property(x => x.FileFormat).IsRequired().HasMaxLength(10);

            builder.Property(x => x.LabelUrl).HasMaxLength(200);


        }
    }
}
