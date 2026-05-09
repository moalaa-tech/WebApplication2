using CRM.Domain.Entities.MarketingAutomation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.MarketingAutomation
{
    public class AutomationStepConfiguration : IEntityTypeConfiguration<AutomationStep>
    {
        public void Configure(EntityTypeBuilder<AutomationStep> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);


            builder.Property(x => x.Order).IsRequired();
            builder.Property(x => x.Action).IsRequired();

            //builder.HasOne(x => x.Automation)
            //    .WithMany(a => a.Steps)
            //    .HasForeignKey(x => x.AutomationId);

            //builder.HasOne(x => x.EmailTemplate)
            //    .WithMany()
            //    .HasForeignKey(x => x.EmailTemplateId)
            //    .OnDelete(DeleteBehavior.SetNull);
        }
    }

}
