using CRM.Domain.Entities.Budgeting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.Budgeting
{
    public class ForecastConfiguration : IEntityTypeConfiguration<Forecast>
    {
        public void Configure(EntityTypeBuilder<Forecast> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);
            builder.Property(a => a.Name).IsRequired().HasMaxLength(200);
            builder.Property(a => a.NameAr).HasMaxLength(200);

            //builder.HasData(
            //     new Forecast { Id = 1, Name = "Q1 Forecast", ForecastDate = new DateTime(2025, 1, 1), Scenario = ForecastScenario.BaseCase }
            // );
        }
    }
}
