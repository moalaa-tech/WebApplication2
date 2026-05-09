using CRM.Domain.Entities.AccountsReceivable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations
{
    public class ReceiptApplicationConfiguration : IEntityTypeConfiguration<ReceiptApplication>
    {
        public void Configure(EntityTypeBuilder<ReceiptApplication> builder)
        {
            builder.HasKey(ra => ra.Id);

            builder.Property(ra => ra.AmountApplied)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // Relationships
            builder.HasOne(ra => ra.Receipt)
                .WithMany(r => r.Applications)
                .HasForeignKey(ra => ra.ReceiptId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
