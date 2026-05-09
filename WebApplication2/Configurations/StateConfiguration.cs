using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations
{
    internal class StateConfiguration : IEntityTypeConfiguration<State>
    {
        public void Configure(EntityTypeBuilder<State> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.NameAr).HasMaxLength(100);
            builder.HasOne(a => a.Country).WithMany(g => g.States).HasForeignKey(a => a.CountryId)
                .OnDelete(DeleteBehavior.NoAction);

            // Indexes for performance optimization
            builder.HasIndex(s => s.CountryId)
                .HasDatabaseName("IX_State_CountryId");
                
            builder.HasIndex(s => s.Name)
                .HasDatabaseName("IX_State_Name");
                
            builder.HasIndex(s => new { s.IsActive, s.CountryId, s.Name })
                .HasDatabaseName("IX_State_Active_Country_Name");
        }
    }
}
