using CRM.Domain.Entities.SalesManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.SalesManagment
{
    public class QuoteConfiguration : IEntityTypeConfiguration<Quote>
    {
        public void Configure(EntityTypeBuilder<Quote> builder)
        {
            builder.HasKey(ra => ra.Id);

            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(i => i.DiscountAmount).HasPrecision(18, 4);
            builder.Property(i => i.SubTotal).HasPrecision(18, 4);

            builder.Property(i => i.TotalAmount).HasPrecision(18, 4);

            builder.Property(i => i.TaxAmount).HasPrecision(18, 4);


        }
    }
}
