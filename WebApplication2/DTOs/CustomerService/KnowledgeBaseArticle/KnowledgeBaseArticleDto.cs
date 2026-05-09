namespace CRM.WebApp.DTOs.CustomerService.KnowledgeBaseArticle
{
    public class KnowledgeBaseArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string Category { get; set; }
        public bool IsPublished { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
