using CRM.Domain.Entities.Budgeting;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.Budgeting
{
    public class BudgetLineConfiguration : IEntityTypeConfiguration<BudgetLine>
    {
        public void Configure(EntityTypeBuilder<BudgetLine> builder)
        {
            builder.HasKey(bl => bl.Id);

            builder.Property(bl => bl.BudgetAmount).HasPrecision(18, 2).HasDefaultValue(0);


            builder.Property(bl => bl.ActualAmount).HasPrecision(18, 2).HasDefaultValue(0);


            builder.Property(bl => bl.PeriodMonth)
                .IsRequired()
                .HasAnnotation("Range", new[] { 1, 12 });

            // Indexes
            builder.HasIndex(bl => bl.BudgetId);
            builder.HasIndex(bl => bl.GLAccountId);
            builder.HasIndex(bl => new { bl.BudgetId, bl.PeriodMonth });

            // Relationships
            builder.HasOne(bl => bl.Budget)
                .WithMany(b => b.Lines)
                .HasForeignKey(bl => bl.BudgetId)
                .OnDelete(DeleteBehavior.Cascade);

            builder.HasOne(bl => bl.GLAccount)
                .WithMany()
                .HasForeignKey(bl => bl.GLAccountId)
                .OnDelete(DeleteBehavior.Restrict);


            //        builder.HasData(
            //    new BudgetLine { Id = 1, BudgetId = 1, GLAccountId = 101, PeriodMonth = 1, BudgetAmount = 10000m, ActualAmount = 9500m },
            //    new BudgetLine { Id = 2, BudgetId = 1, GLAccountId = 101, PeriodMonth = 2, BudgetAmount = 12000m, ActualAmount = 11000m }
            //);
        }
    }
}
