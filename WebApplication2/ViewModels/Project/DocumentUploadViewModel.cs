using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.Project
{
    public class DocumentUploadViewModel
    {
        [Required]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        public IFormFile File { get; set; }

        [StringLength(50)]
        public string DocumentType { get; set; }
    }
}
