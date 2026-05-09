using CRM.Domain.Entities.MarketingAutomation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.MarketingAutomation
{
    public class LeadScoreRuleConfiguration : IEntityTypeConfiguration<LeadScoreRule>
    {
        public void Configure(EntityTypeBuilder<LeadScoreRule> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(lsr => lsr.RuleName)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(lsr => lsr.Condition)
                .IsRequired()
                .HasMaxLength(500);

            builder.Property(lsr => lsr.Points)
                .IsRequired();

            builder.Property(lsr => lsr.ExecutionOrder)
                .IsRequired();

            // Relationship
            builder.HasOne(lsr => lsr.LeadScore)
                .WithMany(ls => ls.Rules)
                .HasForeignKey(lsr => lsr.LeadScoreId)
                .OnDelete(DeleteBehavior.Cascade);



            // Indexes
            builder.HasIndex(lsr => lsr.LeadScoreId)
                .HasDatabaseName("IX_LeadScoreRule_LeadScoreId");

            builder.HasIndex(lsr => lsr.IsActive)
                .HasDatabaseName("IX_LeadScoreRule_IsActive");

           
        }
    }
}
