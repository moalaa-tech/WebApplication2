using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.DocumentManagement
{
    public class CreateDocumentViewModel
    {
        [Required]
        [StringLength(255)]
        [Display(Name = "Title")]
        public string Title { get; set; }

        [StringLength(500)]
        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        [Display(Name = "Document Type")]
        public string DocumentType { get; set; }

        [Display(Name = "Category")]
        public int? CategoryId { get; set; }

        [Display(Name = "Folder")]
        public int? FolderId { get; set; }

        [Display(Name = "Expiry Date")]
        public DateTime? ExpiryDate { get; set; }

        [Display(Name = "Make Public")]
        public bool IsPublic { get; set; }

        [Display(Name = "Tags (comma-separated)")]
        public string Tags { get; set; }

        [Required]
        [Display(Name = "File")]
        public IFormFile File { get; set; }
    }
}
