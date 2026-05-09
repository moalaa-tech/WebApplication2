using CRM.Domain.Entities.CustomerService;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace CRM.WebApp.Configurations.CustomerService
{
    public class KnowledgeBaseArticleConfiguration : IEntityTypeConfiguration<KnowledgeBaseArticle>
    {
        public void Configure(EntityTypeBuilder<KnowledgeBaseArticle> builder)
        {

            builder.HasKey(kb => kb.Id);

            builder.Property(kb => kb.Title)
                .IsRequired()
                .HasMaxLength(200);

            builder.Property(kb => kb.Content)
                .IsRequired();

            builder.Property(kb => kb.Category)
                .IsRequired()
                .HasMaxLength(100);

            builder.Property(kb => kb.CreatedDate)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(kb => kb.LastUpdated)
                .IsRequired()
                .HasDefaultValueSql("GETDATE()");

            builder.Property(kb => kb.IsPublished)
                .IsRequired()
                .HasDefaultValue(false);

            // Indexes
            builder.HasIndex(kb => kb.Category);
            builder.HasIndex(kb => kb.IsPublished);
            builder.HasIndex(kb => kb.CreatedDate);
        }
    }
}
