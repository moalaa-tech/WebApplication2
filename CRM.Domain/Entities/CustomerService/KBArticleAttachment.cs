using CRM.Domain.Base;

namespace CRM.Domain.Entities.CustomerService
{
    public class KBArticleAttachment : BaseEntity
    {
        public int ArticleId { get; set; }
        public string FileName { get; set; }
        public string FilePath { get; set; }
        public string FileType { get; set; }
        public long FileSize { get; set; }

        public KnowledgeBaseArticle Article { get; set; }
    }
}
