using CRM.Domain.Entities.Banking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.Banking
{
    public class ReconciliationItemConfiguration : IEntityTypeConfiguration<ReconciliationItem>
    {
        public void Configure(EntityTypeBuilder<ReconciliationItem> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(bl => bl.AdjustedAmount).HasPrecision(18, 2).HasDefaultValue(0);

            builder.Property(i => i.Notes).HasMaxLength(300);
        }
    }
}
