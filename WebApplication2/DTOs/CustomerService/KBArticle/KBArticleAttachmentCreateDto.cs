using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.CustomerService.KBArticle
{
    public class KBArticleAttachmentCreateDto
    {
        [Required(ErrorMessage = "File name is required")]
        [StringLength(255, ErrorMessage = "File name cannot exceed 255 characters")]
        public string FileName { get; set; }

        [Required(ErrorMessage = "File path is required")]
        [StringLength(500, ErrorMessage = "File path cannot exceed 500 characters")]
        public string FilePath { get; set; }

        [Required(ErrorMessage = "File type is required")]
        [StringLength(100, ErrorMessage = "File type cannot exceed 100 characters")]
        public string FileType { get; set; }

        [Required(ErrorMessage = "File size is required")]
        [Range(1, long.MaxValue, ErrorMessage = "File size must be positive")]
        public long FileSize { get; set; } // in bytes

        [StringLength(100, ErrorMessage = "Description cannot exceed 100 characters")]
        public string Description { get; set; }
    }
}
