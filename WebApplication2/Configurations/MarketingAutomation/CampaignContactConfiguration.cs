using CRM.Domain.Entities.MarketingAutomation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.MarketingAutomation
{
    public class CampaignContactConfiguration : IEntityTypeConfiguration<CampaignContact>
    {
        public void Configure(EntityTypeBuilder<CampaignContact> builder)
        {
            builder
             .HasKey(cc => new { cc.CampaignId, cc.ContactId });

            builder.HasOne(cc => cc.Campaign).WithMany(c => c.Contacts).HasForeignKey(cc => cc.CampaignId);

            builder.HasOne(cc => cc.Contact)
                .WithMany(a => a.Campaigns)
                .HasForeignKey(cc => cc.ContactId);
        }
    }
}
