using CRM.Domain.Base;

namespace CRM.Domain.Entities.CustomerService
{
    public class KnowledgeBaseArticle : BaseEntity
    {
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public string Tags { get; set; } // Comma-separated
        public bool IsPublished { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
        public int AuthorId { get; set; }
        public int ViewCount { get; set; }
        public float HelpfulnessRating { get; set; } // 0-5

        // Navigation properties
        public SupportAgent Author { get; set; }
        public ICollection<KBArticleAttachment> Attachments { get; set; }
        public ICollection<KBArticleComment> Comments { get; set; }
    }
}
