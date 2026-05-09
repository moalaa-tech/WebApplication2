using CRM.Domain.Entities.MarketingAutomation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.MarketingAutomation
{
    public class CampaignConfiguration : IEntityTypeConfiguration<Campaign>
    {
        public void Configure(EntityTypeBuilder<Campaign> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(c => c.Name)
            .IsRequired()
            .HasMaxLength(100);

            builder.Property(c => c.NameAr)
            .HasMaxLength(100);

            builder.Property(c => c.Description)
                .HasMaxLength(500);

            builder.Property(c => c.Budget)
                .HasColumnType("decimal(18,2)");

            builder.Property(c => c.Status)
                .HasConversion<string>();

           

            builder.HasOne(c => c.EmailTemplate)
                .WithMany(a => a.Campaigns)
                .HasForeignKey(c => c.EmailTemplateId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(c => c.CampaignTypes)
                .WithMany(a => a.Campaigns)
                .HasForeignKey(c => c.CampaignTypesId)
                .OnDelete(DeleteBehavior.NoAction);

           

            builder.HasIndex(c => c.StartDate);

            builder.HasIndex(c => c.Status);

        }
    }
}
