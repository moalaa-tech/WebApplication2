using CRM.Domain.Entities.AccountsPayable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations
{
    public class PaymentConfiguration : IEntityTypeConfiguration<Payment>
    {
        public void Configure(EntityTypeBuilder<Payment> builder)
        {
            builder.HasKey(p => p.Id);

            builder.Property(p => p.PaymentNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(p => p.PaymentDate)
                .IsRequired();

            builder.Property(p => p.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(p => p.Method)
                .IsRequired();

            builder.Property(p => p.Reference)
                .HasMaxLength(100);

            builder.Property(p => p.Memo)
                .HasMaxLength(500);

            // Indexes
            builder.HasIndex(p => p.PaymentNumber)
                .IsUnique();
            builder.HasIndex(p => p.PaymentDate);
            builder.HasIndex(p => p.VendorId);
            builder.HasIndex(p => p.BankAccountId);

            // Relationships
            builder.HasOne(p => p.Vendor)
                .WithMany(v => v.Payments)
                .HasForeignKey(p => p.VendorId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(p => p.BankAccount)
                .WithMany()
                .HasForeignKey(p => p.BankAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
