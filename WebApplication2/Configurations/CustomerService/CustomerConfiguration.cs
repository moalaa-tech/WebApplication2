using CRM.Domain.Entities.AccountsReceivable;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.CustomerService
{
    public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
    {
        public void Configure(EntityTypeBuilder<Customer> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);


            builder.Property(a => a.Name).IsRequired().HasMaxLength(100);
            builder.Property(a => a.NameAr).HasMaxLength(100);
            builder.Property(c => c.Address).HasMaxLength(500);
            builder.Property(c => c.Phone).IsRequired().HasMaxLength(20);
            builder.Property(c => c.Email).HasMaxLength(250);
            builder.Property(c => c.Description).HasMaxLength(500);

            builder.Property(bl => bl.CreditLimit)
           .HasColumnType("decimal(18,2)")
           .HasDefaultValue(0);


            builder.HasData(
                new Customer { Id = 1, Name = "Acme Corp", NameAr = "شركة أكمي", Description = "Tech Company", Email = "contact@acmecorp.com", Address = "123 Tech Lane", Phone = "012345678901" },
                new Customer { Id = 2, Name = "Globex Inc.", NameAr = "غلوبكس المحدودة", Description = "Manufacturing", Email = "info@globex.com", Address = "456 Industrial Blvd", Phone = "012345678901" }
            );

            // Indexes for performance optimization
            builder.HasIndex(c => c.Email)
                .IsUnique()
                .HasDatabaseName("IX_Customer_Email_Unique");
                
            builder.HasIndex(c => c.Name)
                .HasDatabaseName("IX_Customer_Name");
                
            builder.HasIndex(c => new { c.IsActive, c.DateCreated })
                .HasDatabaseName("IX_Customer_Active_Created");
                
            builder.HasIndex(c => c.Phone)
                .HasDatabaseName("IX_Customer_Phone");
        }
    }
}
