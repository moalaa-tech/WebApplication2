using CRM.Domain.Entities.AccountsReceivable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations
{
    public class ReceiptConfiguration : IEntityTypeConfiguration<Receipt>
    {
        public void Configure(EntityTypeBuilder<Receipt> builder)
        {
            builder.HasKey(r => r.Id);

            builder.Property(r => r.ReceiptNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(r => r.ReceiptDate)
                .IsRequired();

            builder.Property(r => r.Amount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(r => r.Method)
                .IsRequired();

            builder.Property(r => r.Reference)
                .HasMaxLength(100);

            builder.Property(r => r.Memo)
                .HasMaxLength(500);

            // Indexes
            builder.HasIndex(r => r.ReceiptNumber)
                .IsUnique();
            builder.HasIndex(r => r.ReceiptDate);
            builder.HasIndex(r => r.CustomerId);
            builder.HasIndex(r => r.BankAccountId);

            // Relationships
            builder.HasOne(r => r.Customer)
                .WithMany(c => c.Receipts)
                .HasForeignKey(r => r.CustomerId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(r => r.BankAccount)
                .WithMany()
                .HasForeignKey(r => r.BankAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
