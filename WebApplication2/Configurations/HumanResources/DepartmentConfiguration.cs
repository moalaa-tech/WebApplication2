using CRM.Domain.Entities.HR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.HumanResources
{
    public class DepartmentConfiguration : IEntityTypeConfiguration<Department>
    {
        public void Configure(EntityTypeBuilder<Department> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(500);
            builder.Property(a => a.NameAr).HasMaxLength(500);
            builder.Property(a => a.Description).IsRequired(false).HasMaxLength(500);
            builder.Property(d => d.Location).IsRequired().HasMaxLength(100);
            builder.Property(d => d.Budget).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(d => d.EstablishedDate).IsRequired();

            builder.HasOne(d => d.Manager)
                       .WithMany()
                       .HasForeignKey(d => d.ManagerId)
                       .OnDelete(DeleteBehavior.SetNull);


            builder.HasMany(a => a.Employees)
                   .WithOne(d => d.Department)
                   .OnDelete(DeleteBehavior.NoAction);

            //builder.HasData(
            //        new Department
            //        {
            //            Id = 1,
            //            Name = "Human Resources",
            //            Description = "Manages employee relations and benefits.",
            //            Location = "Building A, Floor 3",
            //            Budget = 500000.00m,
            //            EstablishedDate = new DateTime(1990, 1, 1),
            //            ManagerId = 1,
            //            DateCreated= DateTime.Now,
            //            DateModified= DateTime.Now,
            //            IsActive= true,
            //        },
            //        new Department
            //        {
            //            Id = 2,
            //            Name = "Engineering",
            //            Description = "Develops software and hardware solutions.",
            //            Location = "Building B, Floor 1",
            //            Budget = 1200000.00m,
            //            EstablishedDate = new DateTime(1985, 5, 10),
            //            ManagerId = 2,
            //            DateCreated= DateTime.Now,
            //            DateModified= DateTime.Now,
            //            IsActive= true,
            //        }
            //);
        }


    }
}
