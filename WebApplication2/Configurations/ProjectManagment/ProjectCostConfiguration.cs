using CRM.Domain.Entities.ProjectManagment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.ProjectManagment
{
    public class ProjectCostConfiguration : IEntityTypeConfiguration<ProjectCost>
    {
        public void Configure(EntityTypeBuilder<ProjectCost> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
           

            builder.Property(pc => pc.Amount)
                .IsRequired()
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(pc => pc.Description)        
                .HasMaxLength(500)
                .IsRequired(false)
                .HasDefaultValue(null);


            builder.Property(pc => pc.DateIncurred)
                .IsRequired()
                .HasColumnType("datetime");


            builder.HasOne(pc => pc.Project)
                .WithMany(p => p.Costs)
                .HasForeignKey(pc => pc.ProjectId)
                .OnDelete(DeleteBehavior.Cascade);
          
        }
    }
}
