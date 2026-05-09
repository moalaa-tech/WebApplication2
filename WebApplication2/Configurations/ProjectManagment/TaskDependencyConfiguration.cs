using CRM.Domain.Entities.ProjectManagment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.ProjectManagment
{
    public class TaskDependencyConfiguration : IEntityTypeConfiguration<TaskDependency>
    {
        public void Configure(EntityTypeBuilder<TaskDependency> builder)
        {
            builder.HasKey(td => td.Id);
            builder.Property(td => td.DateCreated)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(td => td.DateModified);
            builder.Property(td => td.IsActive)
                .IsRequired()
                .HasDefaultValue(true);

            builder.HasOne(td => td.Task);

            builder.HasOne(td => td.DependsOnTask)
                .WithMany()
                .HasForeignKey(td => td.DependsOnTaskId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.Property(td => td.DependencyType);



            builder.Property(td => td.Description)
                .HasMaxLength(500)
                .IsRequired(false);

            // Indexes
            builder.HasIndex(td => new { td.TaskId, td.DependsOnTaskId, td.DependencyType })
                .IsUnique()
                .HasDatabaseName("IX_TaskDependency_Unique");



        }
    }
}
