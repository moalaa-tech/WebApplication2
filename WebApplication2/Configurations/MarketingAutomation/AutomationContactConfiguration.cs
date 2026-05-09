using CRM.Domain.Entities.MarketingAutomation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.MarketingAutomation
{
    public class AutomationContactConfiguration : IEntityTypeConfiguration<AutomationContact>
    {
        public void Configure(EntityTypeBuilder<AutomationContact> builder)
        {
            builder.HasKey(ac => new { ac.AutomationId, ac.ContactId });
        }
    }
}
