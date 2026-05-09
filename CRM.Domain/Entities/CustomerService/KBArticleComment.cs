using CRM.Domain.Base;

namespace CRM.Domain.Entities.CustomerService
{
    public class KBArticleComment : BaseEntity
    {
        public int ArticleId { get; set; }
        public int CommenterId { get; set; }
        public string CommentText { get; set; }
        public DateTime CreatedDate { get; set; }
        public bool IsHelpful { get; set; }

        public KnowledgeBaseArticle Article { get; set; }
    }
}
