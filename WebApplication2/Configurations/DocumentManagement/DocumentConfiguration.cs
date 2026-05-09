using CRM.Domain.Entities.DocumentManagement;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.DocumentManagement
{
    public class DocumentConfiguration : IEntityTypeConfiguration<Document>
    {
        public void Configure(EntityTypeBuilder<Document> builder)
        {
            builder.ToTable("Documents");

            builder.HasKey(d => d.Id);
            builder.Property(a => a.DateCreated).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.DateModified).IsRequired().HasDefaultValueSql("GETDATE()");
            builder.Property(a => a.IsActive).IsRequired().HasDefaultValue(1);

            builder.Property(d => d.Title)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(d => d.Description)
                .HasMaxLength(500);

            builder.Property(d => d.DocumentType)
                .IsRequired()
                .HasMaxLength(50);

            builder.Property(d => d.FileName)
                .IsRequired()
                .HasMaxLength(255);

            builder.Property(d => d.FileExtension)
                .IsRequired()
                .HasMaxLength(10);

            builder.Property(d => d.MimeType)
                .IsRequired();

            builder.Property(d => d.Status)
                .HasMaxLength(50)
                .HasDefaultValue("Active");

            builder.Property(d => d.UploadedBy)
                .IsRequired();

            builder.Property(d => d.UploadDate)
                .IsRequired();

            builder.Property(d => d.Version)
                .HasDefaultValue(1);

            builder.Property(d => d.IsLatestVersion)
                .HasDefaultValue(true);

            builder.Property(d => d.DownloadCount)
                .HasDefaultValue(0);

           
            // Relationships
            builder.HasOne(d => d.Category)
                .WithMany(c => c.Documents)
                .HasForeignKey(d => d.CategoryId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(d => d.Folder)
                .WithMany(f => f.Documents)
                .HasForeignKey(d => d.FolderId)
                .OnDelete(DeleteBehavior.SetNull);

            builder.HasOne(d => d.PreviousVersion)
                .WithMany()
                .HasForeignKey(d => d.PreviousVersionId)
                .OnDelete(DeleteBehavior.Restrict);


            // Indexes
            builder.HasIndex(d => d.Title);
            builder.HasIndex(d => d.DocumentType);
            builder.HasIndex(d => d.UploadDate);
            builder.HasIndex(d => d.CategoryId);
            builder.HasIndex(d => d.FolderId);
            builder.HasIndex(d => d.Status);
            builder.HasIndex(d => new { d.IsLatestVersion, d.Status });
        }
    }
}
