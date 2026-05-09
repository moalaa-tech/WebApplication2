using CRM.Domain.Entities.SalesManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.SalesManagment
{
    public class QuoteLineItemConfiguration : IEntityTypeConfiguration<QuoteLineItem>
    {
        public void Configure(EntityTypeBuilder<QuoteLineItem> builder)
        {
            builder.HasKey(ra => ra.Id);

            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(i => i.UnitPrice).HasPrecision(18, 4);
            builder.Property(i => i.DiscountPercentage).HasPrecision(18, 4);


        }
    }
}
