using CRM.Domain.Entities.MarketingAutomation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.MarketingAutomation
{
    public class LeadScoreConfiguration : IEntityTypeConfiguration<LeadScore>
    {
        public void Configure(EntityTypeBuilder<LeadScore> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);


            builder.HasOne(ls => ls.Lead)
                .WithMany(l => l.Scores)
                .HasForeignKey(ls => ls.LeadId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(ls => ls.Criteria)
                .WithMany(c => c.LeadScores)
                .HasForeignKey(ls => ls.ScoreCriteriaId)
                .OnDelete(DeleteBehavior.Restrict);

            
        }
    }

}
