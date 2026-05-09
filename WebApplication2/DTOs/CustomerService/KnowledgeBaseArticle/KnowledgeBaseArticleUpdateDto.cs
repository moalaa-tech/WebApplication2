using CRM.WebApp.DTOs.CustomerService.KBArticle;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.CustomerService.KnowledgeBaseArticle
{
    public class KnowledgeBaseArticleUpdateDto
    {
        [Required(ErrorMessage = "Article ID is required")]
        public int Id { get; set; }

        [Required(ErrorMessage = "Title is required")]
        [StringLength(200, ErrorMessage = "Title cannot exceed 200 characters")]
        public string Title { get; set; }

        [Required(ErrorMessage = "Content is required")]
        public string Content { get; set; } // HTML content

        [Required(ErrorMessage = "Category is required")]
        [StringLength(100, ErrorMessage = "Category cannot exceed 100 characters")]
        public string Category { get; set; }

        [StringLength(500, ErrorMessage = "Tags cannot exceed 500 characters")]
        public string Tags { get; set; } // Comma-separated

        public bool IsPublished { get; set; }

        [StringLength(50, ErrorMessage = "Version cannot exceed 50 characters")]
        public string Version { get; set; }

        [StringLength(2000, ErrorMessage = "Change log cannot exceed 2000 characters")]
        public string ChangeLog { get; set; }

        public List<int> AttachmentIdsToRemove { get; set; } = new List<int>();
        public List<KBArticleAttachmentCreateDto> NewAttachments { get; set; } = new List<KBArticleAttachmentCreateDto>();
    }
}
