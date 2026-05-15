using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations
{
    public class UsedAssignedLocationConfiguration : IEntityTypeConfiguration<UsedAssignedLocation>
    {
        public void Configure(EntityTypeBuilder<UsedAssignedLocation> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.HasOne(a => a.Employee)
                .WithMany()
                .HasForeignKey(a => a.EmployeeId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.Country)
                .WithMany()
                .HasForeignKey(a => a.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.State)
                .WithMany()
                .HasForeignKey(a => a.StatesId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.City)
                .WithMany()
                .HasForeignKey(a => a.CityId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasIndex(a => a.EmployeeId)
                .HasDatabaseName("IX_UsedAssignedLocation_EmployeeId");

            builder.HasIndex(a => new { a.CountryId, a.StatesId, a.CityId })
                .HasDatabaseName("IX_UsedAssignedLocation_Location");
        }
    }
}
