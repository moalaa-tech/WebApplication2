using CRM.Domain.Entities.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.HumanResources
{
    public class EmployeeConfiguration : IEntityTypeConfiguration<Employee>
    {
        public void Configure(EntityTypeBuilder<Employee> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(500);
            builder.Property(a => a.Gender).IsRequired().HasMaxLength(50);

            builder.Property(a => a.LastName).IsRequired().HasMaxLength(500);
            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(500);

            builder.Property(a => a.FirstName).IsRequired().HasMaxLength(500);
            builder.Property(a => a.Description).IsRequired(false).HasMaxLength(500);

            builder.Property(a => a.DateOfJoining).IsRequired().HasDefaultValueSql("GETDATE()");


            builder.HasOne(a => a.Department)
                .WithMany(x => x.Employees)
                .OnDelete(DeleteBehavior.NoAction);



            // builder.HasData(
            //  new Employee { Id = 1, Name = "John", FirstName = "John", LastName = "Doe", Gender = "male" },
            //     new Employee { Id = 2, Name = "John", FirstName = "Jane", LastName = "Smith", Gender = "male" }
            //);


        }
    }
}
