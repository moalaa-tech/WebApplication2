using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations
{
    public class CountryConfiguration : IEntityTypeConfiguration<Country>
    {
        public void Configure(EntityTypeBuilder<Country> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.NameAr).HasMaxLength(100);
            builder.Property(a => a.ISO2).IsRequired().HasMaxLength(10);
            builder.Property(a => a.ISO3).IsRequired().HasMaxLength(10);

            //builder.HasMany(a => a.Companies).WithOne(a => a.Country)
            //    .HasForeignKey(a => a.CountryId).OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.States).WithOne(a => a.Country)
                .HasForeignKey(a => a.CountryId).OnDelete(DeleteBehavior.NoAction);

           
                            
                
        }
    }
}
