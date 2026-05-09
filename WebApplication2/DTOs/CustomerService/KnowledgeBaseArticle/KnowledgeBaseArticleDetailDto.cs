using CRM.WebApp.DTOs.CustomerService.KBArticle;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.CustomerService.KnowledgeBaseArticle
{
    public class KnowledgeBaseArticleDetailDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }  // HTML formatted content

        [Required]
        [StringLength(100)]
        public string Category { get; set; }

        public string Tags { get; set; }  // Comma-separated list
        public bool IsPublished { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public int ViewCount { get; set; }
        public float HelpfulnessRating { get; set; } // 0-5 scale

        // Author information
        public Guid AuthorId { get; set; }
        public string AuthorName { get; set; }
        public string AuthorEmail { get; set; }
        public string AuthorAvatarUrl { get; set; }

        // Attachments
        public List<KBArticleAttachmentDto> Attachments { get; set; } = new List<KBArticleAttachmentDto>();

        // Comments
        public List<KBArticleCommentDto> Comments { get; set; } = new List<KBArticleCommentDto>();

        // Related articles
        public List<RelatedArticleDto> RelatedArticles { get; set; } = new List<RelatedArticleDto>();

        // Version information
        public string Version { get; set; }
        public string ChangeLog { get; set; }

        // Metadata
        public int WordCount { get; set; }
        public TimeSpan EstimatedReadTime { get; set; }
        public DateTime? LastViewed { get; set; }

        // SEO properties
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
        public string Slug { get; set; }
    }
}
