using CRM.Domain.Entities.MarketingAutomation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.MarketingAutomation
{
    public class CampaignTypeConfiguration : IEntityTypeConfiguration<CampaignTypes>
    {
        public void Configure(EntityTypeBuilder<CampaignTypes> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(ct => ct.Name)
                .IsRequired()
                .HasMaxLength(50);


            builder.HasMany(ct => ct.Campaigns)
                .WithOne(c => c.CampaignTypes)
                .HasForeignKey(c => c.CampaignTypesId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(ct => ct.Name)
                .IsUnique();
        }
    }
}
