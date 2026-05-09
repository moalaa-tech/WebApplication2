using CRM.Domain.Entities.MarketingAutomation;
using CRM.Domain.Enums.MarketingAutomation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.MarketingAutomation
{
    public class SocialMediaPostConfiguration : IEntityTypeConfiguration<SocialMediaPost>
    {
        public void Configure(EntityTypeBuilder<SocialMediaPost> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(p => p.Content)
                .IsRequired()
                .HasMaxLength(2000);

            builder.Property(p => p.ScheduledTime)
                .IsRequired();

            builder.Property(p => p.Platform)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(p => p.Status)
                .IsRequired()
                .HasConversion<string>()
                .HasDefaultValue(PostStatus.Draft);

            builder.HasOne(p => p.CreatedBy)
                .WithMany()
                .HasForeignKey(p => p.CreatedById)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(p => p.ModifiedBy)
               .WithMany()
               .HasForeignKey(p => p.ModifiedById)
               .OnDelete(DeleteBehavior.NoAction);

            // Indexes
            builder.HasIndex(p => p.ScheduledTime);
            builder.HasIndex(p => p.Status);
            builder.HasIndex(p => new { p.Platform, p.Status });
        }
    }
}
