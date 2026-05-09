using CRM.Domain.Entities.Budgeting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.Budgeting
{
    public class BudgetConfiguration : IEntityTypeConfiguration<Budget>
    {
        public void Configure(EntityTypeBuilder<Budget> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(200);
            builder.Property(a => a.NameAr).HasMaxLength(200);


            //builder.HasData(
            //      new Budget { Id = 1, Name = "2025 Main Budget", FiscalYear = 2025 }
            //);
        }
    }
}
