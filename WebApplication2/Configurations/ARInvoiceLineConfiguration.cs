using CRM.Domain.Entities.AccountsReceivable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CRM.WebApp.Configurations
{
    public class ARInvoiceLineConfiguration : IEntityTypeConfiguration<InvoiceLine>
    {
        public void Configure(EntityTypeBuilder<InvoiceLine> builder)
        {
            builder.HasKey(il => il.Id);

            builder.Property(il => il.Description)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(il => il.Quantity)
                .HasColumnType("decimal(18,4)")
                .IsRequired();

            builder.Property(il => il.UnitPrice)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(il => il.TaxRate)
                .HasColumnType("decimal(5,2)")
                .HasDefaultValue(0);

            // Indexes
            builder.HasIndex(il => il.InvoiceId);
            builder.HasIndex(il => il.GLAccountId);

            // Relationships
            builder.HasOne(il => il.Invoice)
                .WithMany(i => i.Lines)
                .HasForeignKey(il => il.InvoiceId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(il => il.GLAccount)
                .WithMany()
                .HasForeignKey(il => il.GLAccountId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
