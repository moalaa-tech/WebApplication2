using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.MarketingAutomation
{
    public class EmailTemplateCreateEditViewModel
    {
        public int Id { get; set; } // Only used for editing, not for creating

        [Required(ErrorMessage = "Template name is required.")]
        [StringLength(255, ErrorMessage = "Template name cannot exceed 255 characters.")]
        [Display(Name = "Template Name")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Subject is required.")]
        [StringLength(500, ErrorMessage = "Subject cannot exceed 500 characters.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Body is required.")]
        [DataType(DataType.Html)] // Hint for rich text editor
        public string Body { get; set; }

        [Display(Name = "Is Active")]
        public bool IsActive { get; set; } = true; // Default to active
    }
}
