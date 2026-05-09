using CRM.Domain.Entities.Banking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.Banking
{
    public class BankTransactionConfiguration : IEntityTypeConfiguration<BankTransaction>
    {
        public void Configure(EntityTypeBuilder<BankTransaction> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(t => t.Description).HasMaxLength(200);


            builder.Property(bt => bt.TransactionDate)
                .IsRequired();

            builder.Property(bt => bt.Amount).HasPrecision(18, 2)
                .HasColumnType("decimal(18,2)")
                .IsRequired().HasDefaultValue(0);

            builder.Property(bt => bt.RunningBalance)
                .HasColumnType("decimal(18,2)").HasPrecision(18, 2).HasDefaultValue(0);

            builder.Property(bt => bt.ReferenceNumber)
                .HasMaxLength(100);

            builder.Property(bt => bt.Description).HasMaxLength(500);


            // Relationships
            builder.HasOne(bt => bt.BankAccount)
                .WithMany(ba => ba.Transactions)
                .HasForeignKey(bt => bt.BankAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bt => bt.RelatedPayment)
                .WithOne()
                .HasForeignKey<BankTransaction>(bt => bt.RelatedPaymentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasOne(bt => bt.RelatedReceipt)
                .WithOne()
                .HasForeignKey<BankTransaction>(bt => bt.RelatedReceiptId)
                .OnDelete(DeleteBehavior.Restrict);


            // Indexes
            builder.HasIndex(bt => bt.BankAccountId);
            builder.HasIndex(bt => bt.TransactionDate);
            builder.HasIndex(bt => bt.PostedDate);
            builder.HasIndex(bt => bt.Status);

            builder.HasIndex(bt => bt.RelatedPaymentId)
                .IsUnique()
                .HasFilter("[RelatedPaymentId] IS NOT NULL");
            builder.HasIndex(bt => bt.RelatedReceiptId)
                .IsUnique()
                .HasFilter("[RelatedReceiptId] IS NOT NULL");

           

        }
    }
}
