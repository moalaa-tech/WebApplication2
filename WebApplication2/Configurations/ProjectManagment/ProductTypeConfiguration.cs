using CRM.Domain.Entities.InventoryManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.ProjectManagment
{
    public class ProductTypeConfiguration : IEntityTypeConfiguration<ProductType>
    {
        public void Configure(EntityTypeBuilder<ProductType> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(200);
            builder.Property(a => a.NameAr).HasMaxLength(200);

            builder.Property(a => a.Description).IsRequired(false).HasMaxLength(500);


            builder.HasData(
              new ProductType { Id = 1, Name = "Electronics", NameAr = "إلكترونيات" },
              new ProductType { Id = 2, Name = "Clothing", NameAr = "ملابس" },
              new ProductType { Id = 3, Name = "Books", NameAr = "كتب" }
            );
        }
    }
}
