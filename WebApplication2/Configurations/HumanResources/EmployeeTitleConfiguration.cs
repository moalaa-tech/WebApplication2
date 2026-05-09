using CRM.Domain.Entities.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.HumanResources
{
    public class EmployeeTitleConfiguration : IEntityTypeConfiguration<EmployeeTitle>
    {
        public void Configure(EntityTypeBuilder<EmployeeTitle> builder)
        {
            builder.HasKey(x => new { x.EmployeeId, x.JobTitleId });

            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
        }
    }
}
