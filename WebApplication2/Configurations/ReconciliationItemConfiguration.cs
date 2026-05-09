using CRM.Domain.Entities.Banking;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations
{
    public class ReconciliationItemConfiguration : IEntityTypeConfiguration<ReconciliationItem>
    {
        public void Configure(EntityTypeBuilder<ReconciliationItem> builder)
        {
            builder.HasKey(ri => ri.Id);

            builder.Property(ri => ri.AdjustedAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(ri => ri.Notes)
                .HasMaxLength(500);

            // Indexes
            builder.HasIndex(ri => ri.ReconciliationId);
            builder.HasIndex(ri => ri.BankTransactionId)
                .IsUnique();

            // Relationships
            builder.HasOne(ri => ri.Reconciliation)
                .WithMany(r => r.Items)
                .HasForeignKey(ri => ri.ReconciliationId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ri => ri.BankTransaction)
                .WithOne()
                .HasForeignKey<ReconciliationItem>(ri => ri.BankTransactionId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
