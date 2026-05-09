using CRM.Domain.Entities.MarketingAutomation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.MarketingAutomation
{
    public class ScoreCriteriaConfiguration : IEntityTypeConfiguration<ScoreCriteria>
    {
        public void Configure(EntityTypeBuilder<ScoreCriteria> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(c => c.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder.Property(c => c.Category)
                .HasConversion<string>();

            
        }
    }
}
