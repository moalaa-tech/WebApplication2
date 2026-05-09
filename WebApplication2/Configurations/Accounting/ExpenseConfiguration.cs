using CRM.Domain.Entities.Accounting;
using CRM.Domain.Entities.AssetsManagment;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.Accounting
{
    public class ExpenseConfiguration : IEntityTypeConfiguration<Expense>
    {
        public void Configure(EntityTypeBuilder<Expense> builder)
        {
            builder.HasKey(a => a.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(true);


            builder.Property(a => a.Title).IsRequired().HasMaxLength(200);

            builder.Property(e => e.TitleAR)
            .IsRequired()
            .HasMaxLength(200);

            builder.Property(e => e.Amount)
                .HasColumnType("decimal(18,2)") // مهم جداً
                .IsRequired();

            builder.Property(e => e.Notes)
                .HasMaxLength(500);

            builder.Property(e => e.Date)
                .IsRequired();


            // Category FK (Optional)
            builder.HasOne(e => e.Category)
                .WithMany(c => c.Expenses)
                .HasForeignKey(e => e.CategoryId)
                .OnDelete(DeleteBehavior.Restrict);

            // CreatedBy (User)
            builder.HasOne(e => e.CreatedBy)
                .WithMany() // لو عندك Navigation في ApplicationUser غيّرها
                .HasForeignKey(e => e.CreatedById)
                .OnDelete(DeleteBehavior.Restrict);

            // ModifiedBy (User)
            builder.HasOne(e => e.ModifiedBy)
                .WithMany() // نفس الشيء حسب تصميم المستخدم
                .HasForeignKey(e => e.ModifiedById)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}
