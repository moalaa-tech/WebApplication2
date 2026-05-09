using CRM.Domain.Entities.SupplyChainManagement;
using CRM.Domain.Enums.SupplyChainManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using System.Reflection.Emit;

namespace CRM.WebApp.Configurations.SupplyChainManagement
{
    public class SupplierConfiguration : IEntityTypeConfiguration<Supplier>
    {
        public void Configure(EntityTypeBuilder<Supplier> builder)
        {
            builder.HasKey(s => s.Id);

            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(1);


            builder.Property(s => s.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(s => s.NameAr)
                .HasMaxLength(100);

            builder.Property(s => s.Code)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(s => s.Address)
                .HasMaxLength(200);

            builder.Property(s => s.PhoneNumber)
                .HasMaxLength(15);

            builder.Property(s => s.Email)
                .HasMaxLength(100);

            builder.Property(s => s.ContactPerson)
                .HasMaxLength(100);




            builder.HasIndex(s => s.Code).IsUnique();
            builder.HasMany(s => s.PurchaseOrders)
                   .WithOne(po => po.Supplier)
                   .HasForeignKey(po => po.SupplierId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.HasMany(s => s.Collaborations)
                   .WithOne(c => c.Supplier)
                   .HasForeignKey(c => c.SupplierId)
                   .OnDelete(DeleteBehavior.Cascade);


            builder.Property(s => s.Status)
                     .HasConversion<string>()
                     .IsRequired()
                     .HasDefaultValue(SupplierStatus.Active);


            builder.Property(s => s.ContractStartDate)
                     .IsRequired();
            builder.Property(s => s.ContractEndDate)
                        .IsRequired(false);

            //builder.HasData(
            //    new Supplier
            //    {
            //        Id = 1,
            //        Name = "Global Supplies Inc.",
            //        Code = "GS-001",
            //        ContactEmail = ""
            //    },
            //    new Supplier
            //    {
            //        Id = 2,
            //        Name = "Tech Components Ltd.",
            //        Code = "TC-002",
            //        ContactEmail = ""

            //    });
        }
    }
}
