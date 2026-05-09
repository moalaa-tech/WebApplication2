using CRM.Domain.Entities.CustomerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.CustomerService
{
    public class SupportAgentConfiguration : IEntityTypeConfiguration<SupportAgent>
    {
        public void Configure(EntityTypeBuilder<SupportAgent> b)
        {
            b.HasKey(a => a.Id);
            b.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            b.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            b.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);


            b.HasIndex(x => x.IsActive);
        }
    }
}
