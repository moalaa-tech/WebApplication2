using CRM.Domain.Entities.ProjectManagment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.ProjectManagment
{
    public class ProjectConfiguration : IEntityTypeConfiguration<Project>
    {
        public void Configure(EntityTypeBuilder<Project> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(p => p.Name)
               .IsRequired()
               .HasMaxLength(100);

            builder.Property(p => p.NameAr)
               .HasMaxLength(100);

            builder.Property(p => p.Description)
                .HasMaxLength(500);

            builder.Property(p => p.StartDate)
                .IsRequired();

            builder.Property(p => p.Status)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(p => p.Budget)
                .IsRequired()
                .HasColumnType("decimal(18,2)");

            // Indexes
            builder.HasIndex(p => p.Name)
                .IsUnique();

            builder.HasIndex(p => p.Status);

            builder.HasIndex(p => p.StartDate);

            builder.HasOne(p => p.Customer)
          .WithMany()
          .HasForeignKey(p => p.CustomerId)
          .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(p => p.Phases)
                .WithOne(p => p.Project)
                .HasForeignKey(p => p.ProjectId);

        }
    }
}
