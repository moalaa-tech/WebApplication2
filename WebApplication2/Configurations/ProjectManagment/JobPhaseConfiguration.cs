using CRM.Domain.Entities.ProjectManagment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.ProjectManagment
{
    public class JobPhaseConfiguration : IEntityTypeConfiguration<JobPhase>
    {
        public void Configure(EntityTypeBuilder<JobPhase> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);


            builder.Property(p => p.PhaseCode)
                .IsRequired()
                .HasMaxLength(20);

            builder.HasIndex(p => new { p.ProjectId, p.PhaseCode })
                .IsUnique();

            builder.Property(p => p.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(p => p.EstimatedHours)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.EstimatedCost)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.ActualHours)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.ActualCost)
                .HasColumnType("decimal(18,2)");

            builder.Property(p => p.Status)
                .HasConversion<string>();
        }
    }
}
