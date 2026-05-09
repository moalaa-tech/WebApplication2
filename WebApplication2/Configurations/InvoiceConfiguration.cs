using CRM.Domain.Entities.AccountsPayable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations
{
    public class InvoiceConfiguration : IEntityTypeConfiguration<Invoice>
    {
        public void Configure(EntityTypeBuilder<Invoice> builder)
        {
            builder.HasKey(i => i.Id);

            builder.Property(i => i.InvoiceNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(i => i.InvoiceDate)
                .IsRequired();

            builder.Property(i => i.DueDate)
                .IsRequired();

            builder.Property(i => i.Amount).HasColumnType("decimal(18,4)")
                .HasPrecision(18, 4).HasDefaultValue(0);

            builder.Property(i => i.PaidAmount).HasColumnType("decimal(18,4)")
                .HasPrecision(18, 4)
                .HasDefaultValue(0)
                ;

          


            builder.HasOne(i => i.Vendor)
                           .WithMany(v => v.Invoices)
                           .HasForeignKey(i => i.VendorId)
                           .OnDelete(DeleteBehavior.Restrict);



            // Indexes
            builder.HasIndex(i => i.VendorId);
            builder.HasIndex(i => i.InvoiceNumber)
                .IsUnique();
            builder.HasIndex(i => i.DueDate);
            builder.HasIndex(i => i.Status);

        }
    }
}
