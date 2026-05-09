using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace CRM.WebApp.Configurations
{
    public class BusinessConfiguration : IEntityTypeConfiguration<Business>
    {
        public void Configure(EntityTypeBuilder<Business> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(200);
            builder.Property(a => a.NameAr).HasMaxLength(200);


            builder.HasMany(a => a.Companies).
                WithOne(a => a.Business).
                OnDelete(DeleteBehavior.NoAction);

            // Indexes for performance optimization
            builder.HasIndex(a => new { a.IsActive, a.Name })
                .HasDatabaseName("IX_Business_Active_Name");
                
            // Index for business name searches
            builder.HasIndex(a => a.Name)
                .HasDatabaseName("IX_Business_Name");
        }
    }
}
