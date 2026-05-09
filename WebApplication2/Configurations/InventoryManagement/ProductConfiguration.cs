using CRM.Domain.Entities.InventoryManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.InventoryManagement
{
    public class ProductConfiguration : IEntityTypeConfiguration<Product>
    {
        public void Configure(EntityTypeBuilder<Product> builder)
        {
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(200);
            builder.Property(a => a.NameAr).HasMaxLength(200);


            builder.Property(d => d.FreightCost).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(d => d.ShippingCost).HasColumnType("decimal(18,2)").IsRequired();
            builder.Property(d => d.TotalCost).HasColumnType("decimal(18,2)").IsRequired();


            builder
           .HasOne(p => p.ProductType) // A product has one product type
           .WithMany(pt => pt.Products) // A product type can have many products
           .HasForeignKey(p => p.ProductTypeId) // Foreign key in Product
           .IsRequired(false); // ProductTypeId is nullable

            // builder.HasData(           
            //     new Product { Name = "Laptop", TotalCost = 1200.00m, Description = "Powerful laptop for work and gaming." },
            //     new Product { Name = "Mouse", TotalCost = 25.00m, Description = "Ergonomic wireless mouse." },
            //     new Product { Name = "Keyboard", TotalCost = 75.00m, Description = "Mechanical keyboard with RGB lighting." },
            //     new Product { Name = "Monitor", TotalCost = 300.00m, Description = "27-inch 4K display." }
            //);
        }
    }
}
