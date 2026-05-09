using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.CustomerService.KBArticle
{
    public class KBArticleSearchDto
    {
        [StringLength(100, ErrorMessage = "Search term cannot exceed 100 characters")]
        public string SearchTerm { get; set; }

        [StringLength(100, ErrorMessage = "Category cannot exceed 100 characters")]
        public string Category { get; set; }

        public bool OnlyPublished { get; set; } = true;

        public DateTime? FromDate { get; set; }
        public DateTime? ToDate { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Page number must be positive")]
        public int PageNumber { get; set; } = 1;

        [Range(1, 100, ErrorMessage = "Page size must be between 1 and 100")]
        public int PageSize { get; set; } = 10;
    }
}
