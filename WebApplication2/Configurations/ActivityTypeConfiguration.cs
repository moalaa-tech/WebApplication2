using CRM.Domain.Entities.SalesManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations
{
    public class ActivityTypeConfiguration : IEntityTypeConfiguration<ActivityType>
    {
        public void Configure(EntityTypeBuilder<ActivityType> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);


            builder.Property(a => a.Name).IsRequired().HasMaxLength(500);
            builder.Property(a => a.NameAr).HasMaxLength(500);

            //        WebsiteVisit,
            //EmailOpened,
            //EmailClicked,
            //FormSubmission,
            //ProductPurchased,
            //DemoRequested,
            //ContentDownload,

            builder.HasData(
                new ActivityType { Id = 1, Name = "Call", NameAr = "مكالمة" },
                new ActivityType { Id = 2, Name = "Email", NameAr = "بريد إلكتروني" },
                new ActivityType { Id = 3, Name = "Meeting", NameAr = "اجتماع" },
                new ActivityType { Id = 4, Name = "Task", NameAr = "مهمة" },
                new ActivityType { Id = 5, Name = "Note", NameAr = "ملاحظة" }
            );
        }
    }
}
