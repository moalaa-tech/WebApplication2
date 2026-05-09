using CRM.Domain.Entities.Banking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.Banking
{
    public class ReconciliationConfiguration : IEntityTypeConfiguration<Reconciliation>
    {
        public void Configure(EntityTypeBuilder<Reconciliation> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(bl => bl.AdjustedBookBalance).HasPrecision(18, 2).HasDefaultValue(0);


            builder.Property(bl => bl.StatementBalance).HasPrecision(18, 2).HasDefaultValue(0);


            builder.HasIndex(r => r.StatementDate);
        }
    }
}
