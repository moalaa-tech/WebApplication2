using CRM.Domain.Entities.MarketingAutomation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.MarketingAutomation
{
    public class SegmentConfiguration : IEntityTypeConfiguration<Segment>
    {
        public void Configure(EntityTypeBuilder<Segment> builder)
        {

            builder.HasKey(s => s.Id);

            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.Description)
                .HasMaxLength(500);

            builder.Property(s => s.Criteria)
                .IsRequired();

            builder.Property(s => s.CreatedDate)
                .HasDefaultValueSql("GETDATE()");

            builder.Property(s => s.SegmentType)
                .HasMaxLength(50);

            // Add indexes
            builder.HasIndex(s => s.Name)
                .IsUnique()
                .HasDatabaseName("IX_Segment_Name");

            builder.HasIndex(s => s.SegmentType)
                .HasDatabaseName("IX_Segment_Type");

            builder.HasIndex(s => s.IsActive)
                .HasDatabaseName("IX_Segment_IsActive");
        }
    }
}
