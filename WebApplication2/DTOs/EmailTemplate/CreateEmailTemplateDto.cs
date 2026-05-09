using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.EmailTemplate
{
    public class CreateEmailTemplateDto
    {

        [Required(ErrorMessage = "Name is required.")]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Subject is required.")]
        [StringLength(500, ErrorMessage = "Subject cannot exceed 500 characters.")]
        public string Subject { get; set; }

        [Required(ErrorMessage = "Body is required.")]
        public string Content { get; set; }

        public bool IsActive { get; set; }
    }
}
