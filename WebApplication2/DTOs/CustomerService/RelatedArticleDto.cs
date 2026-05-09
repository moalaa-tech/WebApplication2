namespace CRM.WebApp.DTOs.CustomerService
{
    public class RelatedArticleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category { get; set; }
        public string Slug { get; set; }
        public DateTime LastUpdated { get; set; }
    }
}
