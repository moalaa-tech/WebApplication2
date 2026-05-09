using CRM.Domain.Entities.AssetsManagment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.AssetsManagment
{
    public class DepreciationScheduleConfiguration : IEntityTypeConfiguration<DepreciationSchedule>
    {
        public void Configure(EntityTypeBuilder<DepreciationSchedule> builder)
        {
            builder.HasKey(a => a.Id);

            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(bl => bl.AccumulatedDepreciation)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);


            builder.Property(bl => bl.BookValue)
               .HasColumnType("decimal(18,2)")
               .HasDefaultValue(0);

            builder.Property(bl => bl.DepreciationAmount)
               .HasColumnType("decimal(18,2)")
               .HasDefaultValue(0);

            builder.HasOne(ds => ds.FixedAsset)
                   .WithMany(fa => fa.DepreciationSchedules)
                   .HasForeignKey(ds => ds.FixedAssetId)
                   .OnDelete(DeleteBehavior.NoAction);


            // Indexes
            builder.HasIndex(ds => ds.FixedAssetId);
            builder.HasIndex(ds => ds.ScheduleDate);
            builder.HasIndex(ds => ds.IsPosted);
            builder.HasIndex(ds => new { ds.FixedAssetId, ds.ScheduleDate })
                .IsUnique();

            // Seed data
            //builder.HasData(
            //    new DepreciationSchedule
            //    {
            //        Id = 1,
            //        FixedAssetId = 1,
            //        ScheduleDate = new DateTime(2022, 2, 15),
            //        DepreciationAmount = 40.00m,
            //        AccumulatedDepreciation = 40.00m,
            //        BookValue = 1460.00m,
            //        IsPosted = true,
            //        Notes = "Monthly depreciation for February 2022",
            //        DateCreated = DateTime.Now,
            //        DateModified = DateTime.Now,
            //        IsActive = true
            //    },
            //    new DepreciationSchedule
            //    {
            //        Id = 2,
            //        FixedAssetId = 1,
            //        ScheduleDate = new DateTime(2022, 3, 15),
            //        DepreciationAmount = 40.00m,
            //        AccumulatedDepreciation = 80.00m,
            //        BookValue = 1420.00m,
            //        IsPosted = true,
            //        Notes = "Monthly depreciation for March 2022",
            //        DateCreated = DateTime.Now,
            //        DateModified = DateTime.Now,
            //        IsActive = true
            //});


        }
    }
}
