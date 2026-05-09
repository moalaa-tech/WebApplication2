using CRM.Domain.Entities.ProjectManagment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.ProjectManagment
{
    public class ProjectTaskConfiguration : IEntityTypeConfiguration<ProjectTask>
    {
        public void Configure(EntityTypeBuilder<ProjectTask> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(pt => pt.TaskName)
                          .IsRequired()
                          .HasMaxLength(200);

            builder.Property(pt => pt.Description)
                .HasMaxLength(1000)
                .IsRequired(false)
                .HasDefaultValue(null);

            builder.Property(pt => pt.EstimatedHours)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);


            builder.Property(pt => pt.CompletionPercentage)
                .IsRequired()
                .HasDefaultValue(0)
                .HasColumnType("decimal(18,2)");

            builder.Property(pt => pt.ActualHours)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);



            builder.Property(pt => pt.Status)
                .IsRequired()
                .HasConversion<string>();



            builder.Property(pt => pt.Priority)
                .IsRequired()
                .HasConversion<string>();

            builder.Property(pt => pt.StartDate);

            builder.Property(pt => pt.DueDate)
                .IsRequired();


            builder.HasMany(pt => pt.SubTasks)
                .WithOne(pt => pt.ParentTask)
                .HasForeignKey(pt => pt.ParentTaskId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(pt => pt.Dependencies);

            builder.HasOne(pt => pt.Project)
                .WithMany(p => p.Tasks)
                .HasForeignKey(pt => pt.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(pt => pt.DemandPlan);




        }
    }
}