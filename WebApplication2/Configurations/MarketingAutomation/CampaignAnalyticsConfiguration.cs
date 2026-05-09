using CRM.Domain.Entities.MarketingAutomation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.MarketingAutomation
{
    public class CampaignAnalyticsConfiguration : IEntityTypeConfiguration<CampaignAnalytics>
    {
        public void Configure(EntityTypeBuilder<CampaignAnalytics> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);



            builder.Property(c => c.TotalBudget)
                .HasColumnType("decimal(18, 2)");

            builder.Property(c => c.TotalSpent)
                .HasColumnType("decimal(18, 2)");

            builder.Property(c => c.ConversionRate)
                .HasColumnType("decimal(18, 2)");

            builder.Property(c => c.CostPerClick)
                .HasColumnType("decimal(18, 2)");

            builder.Property(c => c.CostPerConversion)
                .HasColumnType("decimal(18, 2)");

            builder.Property(c => c.RevenueGenerated)
                .HasColumnType("decimal(18, 2)");

            builder.Property(c => c.ROIPercentage)
                .HasColumnType("decimal(18, 2)");

            builder.Property(c => c.Notes)
                .HasMaxLength(500);

            builder.Property(c => c.Channel)
                .IsRequired();

            builder.Property(c => c.RecordedDate)
                .HasDefaultValueSql("GETDATE()");

            // Add indexes
            builder.HasIndex(c => c.CampaignId)
                .HasDatabaseName("IX_CampaignAnalytics_CampaignId");

            

            builder.HasIndex(c => c.Channel)
                .HasDatabaseName("IX_CampaignAnalytics_Channel");

            builder.HasIndex(c => c.StartDate)
                .HasDatabaseName("IX_CampaignAnalytics_StartDate");

            builder.HasIndex(c => c.EndDate)
                .HasDatabaseName("IX_CampaignAnalytics_EndDate");
        }
    }
}
