using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.EmailTemplate
{
    public class EmailTemplateCreateEditViewModel
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string Subject { get; set; }

        [Required]
        public required string Content { get; set; }
    }
}
