using CRM.Domain.Entities.Budgeting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.Budgeting
{
    public class ForecastLineConfiguration : IEntityTypeConfiguration<ForecastLine>
    {
        public void Configure(EntityTypeBuilder<ForecastLine> builder)
        {
            builder.HasKey(a => a.Id);

            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);

            builder.Property(fl => fl.ForecastAmount)
                .HasColumnType("decimal(18,2)")
                .IsRequired();

            builder.Property(fl => fl.ActualAmount)
                .HasColumnType("decimal(18,2)")
                .HasDefaultValue(0);

            builder.Property(fl => fl.PeriodMonth)
                .IsRequired()
                .HasAnnotation("Range", new[] { 1, 12 });

            // Indexes
            builder.HasIndex(fl => fl.ForecastId);
            builder.HasIndex(fl => fl.GLAccountId);
            builder.HasIndex(fl => new { fl.ForecastId, fl.PeriodMonth });

            // Relationships
            builder.HasOne(fl => fl.Forecast)
                .WithMany(f => f.Lines)
                .HasForeignKey(fl => fl.ForecastId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(fl => fl.GLAccount)
                .WithMany()
                .HasForeignKey(fl => fl.GLAccountId)
                .OnDelete(DeleteBehavior.Restrict);

            //builder.HasData(
            //   new ForecastLine { Id = 1, ForecastId = 1, GLAccountId = 101, PeriodMonth = 1, ForecastAmount = 10500m, ActualAmount = 9500m },
            //   new ForecastLine { Id = 2, ForecastId = 1, GLAccountId = 101, PeriodMonth = 2, ForecastAmount = 12500m, ActualAmount = 11000m }
            //);
        }
    }
}
