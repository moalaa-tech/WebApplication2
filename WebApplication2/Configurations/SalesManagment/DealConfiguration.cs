using CRM.Domain.Entities.SalesManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.SalesManagment
{
    public class DealConfiguration : IEntityTypeConfiguration<Deal>
    {
        public void Configure(EntityTypeBuilder<Deal> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(a => a.Name).IsRequired().HasMaxLength(500);
            builder.Property(a => a.Description).IsRequired(false).HasMaxLength(500);

            builder.Property(bl => bl.Amount)
          .HasPrecision(18, 2)
          .HasDefaultValue(0);


            builder.Property(bl => bl.FinalValue)
         .HasPrecision(18, 2)
         .HasDefaultValue(0);


        }
    }
}
