using CRM.Domain.Entities.Accounting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations
{
    public class JournalEntryConfiguration : IEntityTypeConfiguration<JournalEntry>
    {
        public void Configure(EntityTypeBuilder<JournalEntry> builder)
        {
            builder.HasKey(j => j.Id);

            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(j => j.EntryDate)
                .IsRequired();

            builder.Property(j => j.Reference)
                .HasMaxLength(50);

            builder.Property(j => j.Description)
                .HasMaxLength(500);

            builder.Property(j => j.IsPosted)
                .IsRequired()
                .HasDefaultValue(false);

            // Indexes
            builder.HasIndex(j => j.EntryDate);
            builder.HasIndex(j => j.Reference)
                .IsUnique();
            builder.HasIndex(j => j.IsPosted);
        }
    }
}
