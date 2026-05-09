using CRM.Domain.Entities.ProjectManagment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations
{
    public class PhaseExpenseConfiguration : IEntityTypeConfiguration<PhaseExpense>
    {
        public void Configure(EntityTypeBuilder<PhaseExpense> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(bl => bl.Amount)
          .HasPrecision(18, 2)
          .HasDefaultValue(0);
        }
    }
}
