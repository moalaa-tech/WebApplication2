using CRM.Domain.Entities.Banking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.Banking
{
    public class BankAccountConfiguration : IEntityTypeConfiguration<BankAccount>
    {
        public void Configure(EntityTypeBuilder<BankAccount> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);



            builder.Property(b => b.AccountNumber).IsRequired().HasMaxLength(30);
            builder.Property(b => b.BankName).IsRequired().HasMaxLength(100);
            builder.Property(b => b.AccountName).IsRequired().HasMaxLength(100);
            builder.Property(b => b.Currency).IsRequired().HasMaxLength(10);
            builder.HasIndex(b => b.AccountNumber).IsUnique();


            builder.Property(bl => bl.CurrentBalance)
      .HasPrecision(18, 2)
      .HasDefaultValue(0);
        }
    }
}
