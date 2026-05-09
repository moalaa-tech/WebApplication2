using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations
{
    public class LogEntryConfiguration : IEntityTypeConfiguration<LogEntry>
    {
        public void Configure(EntityTypeBuilder<LogEntry> builder)
        {
            // Table name
            builder.ToTable("Logs");

            // Primary key
            builder.HasKey(l => l.Id);

            // Properties configuration
            builder.Property(l => l.Timestamp)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(l => l.Level)
                .IsRequired()
                .HasMaxLength(50).IsRequired(false);

            builder.Property(l => l.Message).IsRequired(false);

            builder.Property(l => l.Exception).IsRequired(false);

            builder.Property(l => l.Logger)
                .HasMaxLength(255).IsRequired(false);

            builder.Property(l => l.Url)
                .HasMaxLength(255).IsRequired(false);

            builder.Property(l => l.HttpMethod)
                .HasMaxLength(50).IsRequired(false);

            builder.Property(l => l.UserName)
                .HasMaxLength(50).IsRequired(false);

            builder.Property(l => l.ClientIP)
                .HasMaxLength(50).IsRequired(false);

            builder.Property(l => l.RequestBody).IsRequired(false);

            builder.Property(l => l.ResponseBody).IsRequired(false);

            // Indexes
            builder.HasIndex(l => l.Timestamp);
            builder.HasIndex(l => l.Level);
            builder.HasIndex(l => l.UserName);
            builder.HasIndex(l => l.StatusCode);
        }
    }
}
