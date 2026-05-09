using CRM.Domain.Entities.AccountsPayable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations
{
    public class PaymentApplicationConfiguration : IEntityTypeConfiguration<PaymentApplication>
    {
        public void Configure(EntityTypeBuilder<PaymentApplication> builder)
        {
            builder.HasKey(pa => pa.Id);

            builder.Property(pa => pa.AmountApplied)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            // Relationships
            builder.HasOne(pa => pa.Payment)
                .WithMany(p => p.Applications)
                .HasForeignKey(pa => pa.PaymentId)
                .OnDelete(DeleteBehavior.Cascade);


        }
    }
}
