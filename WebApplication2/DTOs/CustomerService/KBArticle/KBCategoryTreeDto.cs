namespace CRM.WebApp.DTOs.CustomerService.KBArticle
{
    public class KBCategoryTreeDto
    {
        public string Name { get; set; }
        public string Slug { get; set; }
        public int ArticleCount { get; set; }
        public List<KBCategoryTreeDto> Children { get; set; } = new List<KBCategoryTreeDto>();
    }
}
