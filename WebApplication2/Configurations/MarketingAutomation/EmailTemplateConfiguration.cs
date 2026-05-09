using CRM.Domain.Entities.MarketingAutomation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.MarketingAutomation
{
    public class EmailTemplateConfiguration : IEntityTypeConfiguration<EmailTemplate>
    {
        public void Configure(EntityTypeBuilder<EmailTemplate> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(et => et.Name)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(et => et.Subject)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(et => et.Content)
                .IsRequired(); // No max length for rich text content


            // SQL Indexes for performance
            builder.HasIndex(et => et.Name)
                .IsUnique(); // Template names should likely be unique

            builder.HasIndex(et => et.IsActive);
            builder.HasIndex(et => et.DateCreated);

            // Seed data (optional for development/testing)
            //builder.HasData(
            //    new EmailTemplate { Id = 1, Name = "Welcome Email", Subject = "Welcome to Our Service!", Content = "Dear user, welcome...", IsActive = true, DateCreated = DateTime.UtcNow },
            //    new EmailTemplate { Id = 2, Name = "Password Reset", Subject = "Reset Your Password", Content = "Click here to reset...", IsActive = true, DateCreated = DateTime.UtcNow }
            //);
        }
    }
}
