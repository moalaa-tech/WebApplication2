using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using FollowUpTask = CRM.Domain.Entities.ProjectManagment.FollowUpTask;

namespace CRM.WebApp.Configurations.ProjectManagment
{
    public class FollowUpTaskConfiguration : IEntityTypeConfiguration<FollowUpTask>
    {
        public void Configure(EntityTypeBuilder<FollowUpTask> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(a => a.Subject).IsRequired().HasMaxLength(500);
            builder.Property(a => a.Description).IsRequired(false).HasMaxLength(500);

        }
    }
}
