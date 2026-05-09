using CRM.WebApp.DTOs.CustomerService.KBArticle;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.CustomerService.KnowledgeBaseArticle
{
    public class KnowledgeBaseArticleCreateDto
    {
        [Required(ErrorMessage = "Article title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Article content is required")]
        public string Content { get; set; }  // HTML content

        [Required(ErrorMessage = "Category is required")]
        [StringLength(100, ErrorMessage = "Category cannot exceed 100 characters")]
        public string Category { get; set; }

        [StringLength(500, ErrorMessage = "Tags cannot exceed 500 characters")]
        public string Tags { get; set; }  // Comma-separated tags

        [Required(ErrorMessage = "Author ID is required")]
        public int AuthorId { get; set; }

        public bool IsPublished { get; set; } = false;

        [StringLength(50, ErrorMessage = "Version cannot exceed 50 characters")]
        public string Version { get; set; } = "1.0";

        [StringLength(2000, ErrorMessage = "Change log cannot exceed 2000 characters")]
        public string ChangeLog { get; set; } = "Initial version";

        // SEO properties
        [StringLength(200, ErrorMessage = "Meta title cannot exceed 200 characters")]
        public string MetaTitle { get; set; }

        [StringLength(500, ErrorMessage = "Meta description cannot exceed 500 characters")]
        public string MetaDescription { get; set; }

        [StringLength(200, ErrorMessage = "Slug cannot exceed 200 characters")]
        [RegularExpression(@"^[a-z0-9]+(?:-[a-z0-9]+)*$",
            ErrorMessage = "Slug must be URL-friendly (lowercase, hyphens)")]
        public string Slug { get; set; }

        // Attachments
        public List<KBArticleAttachmentCreateDto> Attachments { get; set; } = new List<KBArticleAttachmentCreateDto>();

        // Related articles
        public List<int> RelatedArticleIds { get; set; } = new List<int>();

        // Display properties
        public string FeaturedImageUrl { get; set; }
        public string Excerpt { get; set; }

        // Validation method for business rules
        public bool IsValid()
        {
            return !string.IsNullOrWhiteSpace(Title)
                && !string.IsNullOrWhiteSpace(Content)
                && !string.IsNullOrWhiteSpace(Category)
                && AuthorId > 0;
        }
    }
}
