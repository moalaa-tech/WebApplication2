using CRM.Domain.Entities.AssetsManagment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.AssetsManagment
{
    public class FixedAssetConfiguration : IEntityTypeConfiguration<FixedAsset>
    {
        public void Configure(EntityTypeBuilder<FixedAsset> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(fa => fa.AssetNumber)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(fa => fa.Name)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(fa => fa.NameAr)
                .HasMaxLength(200);

            builder.Property(fa => fa.Description)
                .HasMaxLength(500);

            builder.Property(bl => bl.AcquisitionCost)
              .HasColumnType("decimal(18,2)")
              .HasDefaultValue(0);

            builder.Property(bl => bl.SalvageValue)
              .HasColumnType("decimal(18,2)")
              .HasDefaultValue(0);

            builder.HasOne(a => a.ParentAsset)
                   .WithMany(a => a.ChildAssets)
                   .HasForeignKey(a => a.ParentAssetId)
                   .OnDelete(DeleteBehavior.NoAction);

            builder.HasMany(a => a.DepreciationSchedules)
                     .WithOne(ds => ds.FixedAsset)
                     .HasForeignKey(ds => ds.FixedAssetId)
                     .OnDelete(DeleteBehavior.NoAction);

            // Indexes
            builder.HasIndex(fa => fa.AssetNumber)
                .IsUnique();
            builder.HasIndex(fa => fa.Name);
            builder.HasIndex(fa => fa.AcquisitionDate);
            builder.HasIndex(fa => fa.ParentAssetId);
            builder.HasIndex(fa => new { fa.IsActive, fa.DepreciationMethod });


            // Seed data

            //builder.HasData(
            //    new FixedAsset
            //    {
            //        Id = 1,
            //        AssetNumber = "FA-1001",
            //        Name = "Office Laptop",
            //        Description = "Dell Latitude 7420",
            //        AcquisitionDate = new DateTime(2022, 1, 15),
            //        AcquisitionCost = 1500.00m,
            //        SalvageValue = 300.00m,
            //        UsefulLife = 36, // 3 years
            //        DepreciationMethod = Domain.Enums.AssetsManagment.DepreciationMethod.StraightLine,
            //        DateCreated = DateTime.Now,
            //        DateModified = DateTime.Now,
            //        IsActive = true
            //    },
            //    new FixedAsset
            //    {
            //        Id = 2,
            //        AssetNumber = "FA-1002",
            //        Name = "Office Desk",
            //        Description = "Ergonomic Office Desk",
            //        AcquisitionDate = new DateTime(2021, 6, 10),
            //        AcquisitionCost = 800.00m,
            //        SalvageValue = 100.00m,
            //        UsefulLife = 60, // 5 years
            //        DepreciationMethod = Domain.Enums.AssetsManagment.DepreciationMethod.StraightLine,
            //        DateCreated = DateTime.Now,
            //        DateModified = DateTime.Now,
            //        IsActive = true
            //    }
            //);


        }
    }
}
