using CRM.Domain.Entities.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.HumanResources
{
    public class PayrollConfiguration : IEntityTypeConfiguration<Payroll>
    {
        public void Configure(EntityTypeBuilder<Payroll> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(p => p.BasicSalary)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Allowances)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Deductions)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.TaxAmount)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.NetSalary)
                .HasColumnType("decimal(18,2)");

            builder.HasOne(p => p.Employee)
                .WithMany(e => e.Payrolls)
                .HasForeignKey(p => p.EmployeeId);
        }
    }
}
