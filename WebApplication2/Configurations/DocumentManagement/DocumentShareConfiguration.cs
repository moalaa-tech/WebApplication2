using CRM.Domain.Entities.DocumentManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.DocumentManagement
{
    public class DocumentShareConfiguration : IEntityTypeConfiguration<DocumentShare>
    {
        public void Configure(EntityTypeBuilder<DocumentShare> builder)
        {
            builder.HasKey(ds => ds.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(1);

            builder.Property(ds => ds.PermissionLevel)
                .IsRequired()
                .HasMaxLength(20);

            // Indexes
            builder.HasIndex(ds => ds.DocumentId);
            builder.HasIndex(ds => ds.SharedWithUserId);
            builder.HasIndex(ds => ds.SharedByUserId);
            builder.HasIndex(ds => new { ds.DocumentId, ds.SharedWithUserId }).IsUnique();
            builder.HasIndex(ds => ds.ExpiryDate);

            // Relationships
            builder.HasOne(ds => ds.Document)
                .WithMany(d => d.SharedWith)
                .HasForeignKey(ds => ds.DocumentId)
                .OnDelete(DeleteBehavior.NoAction);


            builder.HasOne(ds => ds.Document)
                .WithMany(d => d.SharedWith)
                .HasForeignKey(ds => ds.DocumentId)
                .OnDelete(DeleteBehavior.NoAction);

            builder.HasOne(ds => ds.SharedByUser)
                .WithMany(d => d.DocumentShares)
                //.HasForeignKey(ds => ds.DocumentId)
                .OnDelete(DeleteBehavior.NoAction);
        }
    }
}
