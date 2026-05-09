namespace CRM.WebApp.DTOs.CustomerService.KBArticle
{
    public class KBArticleCategoryDto
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public string Icon { get; set; } // CSS class or icon URL
        public int ArticleCount { get; set; }
        public DateTime LastArticleDate { get; set; }
        public string LastArticleTitle { get; set; }
        public Guid LastArticleId { get; set; }

        // Hierarchy support
        public string ParentCategory { get; set; }
        public List<string> SubCategories { get; set; } = new List<string>();

        // Display properties
        public string Color { get; set; } // Hex color for UI
        public bool IsFeatured { get; set; }
        public int DisplayOrder { get; set; }

        // SEO properties
        public string Slug { get; set; }
        public string MetaTitle { get; set; }
        public string MetaDescription { get; set; }
    }
}
