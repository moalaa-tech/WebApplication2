using CRM.Domain.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations
{
    public class CompanyConfiguration : IEntityTypeConfiguration<Company>
    {
        public void Configure(EntityTypeBuilder<Company> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(a => a.IsDeleted).IsRequired().HasDefaultValueSql("0");
            builder.Property(a => a.Name).IsRequired().HasMaxLength(150);
            builder.Property(a => a.NameAr).HasMaxLength(150);
            builder.Property(a => a.Address).IsRequired().HasMaxLength(500);



            //builder.Property(a => a.Email).IsRequired(true).HasMaxLength(500);
            //builder.Property(a => a.Website).IsRequired(true).HasMaxLength(500);
            //builder.Property(a => a.TaxID).IsRequired(true).HasMaxLength(500);
            //builder.Property(a => a.Code).IsRequired(true).HasMaxLength(100);
            //builder.Property(a => a.CommercialID).IsRequired(true).HasMaxLength(500);
            //builder.Property(a => a.Phone).IsRequired(true).HasMaxLength(500);
            //builder.Property(a => a.Mobile).IsRequired(true).HasMaxLength(500);


            //builder.HasOne(a => a.Country).WithMany(a=>a.Companies).HasForeignKey(a=>a.CountryId)
            //    .OnDelete(DeleteBehavior.NoAction);

            //builder.HasOne(a => a.Currency).WithMany(a => a.Companies).HasForeignKey(a => a.CurrencyId)
            //    .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(a => a.Business).WithMany(a => a.Companies).HasForeignKey(a => a.BusinessId)
                .OnDelete(DeleteBehavior.NoAction);

            // Indexes for performance optimization
            builder.HasIndex(a => a.BusinessId)
                .HasDatabaseName("IX_Company_BusinessId");
            
            builder.HasIndex(a => a.CityId)
                .HasDatabaseName("IX_Company_CityId");
                
            builder.HasIndex(a => a.UserId)
                .HasDatabaseName("IX_Company_UserId");
                
                
            // Composite index for common queries
            builder.HasIndex(a => new { a.IsActive, a.IsDeleted, a.DateCreated })
                .HasDatabaseName("IX_Company_Active_Deleted_Created");
                
            // Index for company name searches
            builder.HasIndex(a => a.Name)
                .HasDatabaseName("IX_Company_Name");
        }
    }
}
