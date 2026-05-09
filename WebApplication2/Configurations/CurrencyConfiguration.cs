using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations
{
    public class CurrencyConfiguration : IEntityTypeConfiguration<Currency>
    {
        public void Configure(EntityTypeBuilder<Currency> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.NameAr).HasMaxLength(100);
            builder.Property(a => a.Code).IsRequired().HasMaxLength(10);

            //builder.HasMany(a=>a.Companies).WithOne(a => a.Currency)
            //    .HasForeignKey(a => a.CurrencyId).OnDelete(DeleteBehavior.NoAction); 

        }
    }
}
