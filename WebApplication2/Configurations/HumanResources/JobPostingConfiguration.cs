using CRM.Domain.Entities.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.HumanResources
{
    public class JobPostingConfiguration : IEntityTypeConfiguration<JobPosting>
    {
        public void Configure(EntityTypeBuilder<JobPosting> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(jp => jp.Title)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(jp => jp.Description)
                .IsRequired();

            builder.Property(jp => jp.PostingDate)
                .IsRequired();

            builder.HasOne(jp => jp.Department)
                .WithMany()
                .HasForeignKey(jp => jp.DepartmentId);
        }
    }
}
