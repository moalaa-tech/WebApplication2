using CRM.Domain.Entities.DocumentManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.DocumentManagement
{
    public class DocumentCategoryConfiguration : IEntityTypeConfiguration<DocumentCategory>
    {
        public void Configure(EntityTypeBuilder<DocumentCategory> builder)
        {
            builder.ToTable("DocumentCategories");

            builder.HasKey(dc => dc.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(1);

            builder.Property(dc => dc.Name)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(dc => dc.NameAr)
              .IsRequired()
              .HasMaxLength(100);

            builder.Property(dc => dc.Description)
                .HasMaxLength(500);

            builder.HasIndex(dc => dc.Name).IsUnique();
            builder.HasIndex(dc => dc.ParentCategoryId);
        }
    }
}
