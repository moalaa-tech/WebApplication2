using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.EmailTemplate
{
    public class EmailTemplateViewModel
    {
        public int Id { get; set; }

        [Required]
        public string Name { get; set; }

        [Required]
        public string Subject { get; set; }

        [Required]
        public string Content { get; set; }
    }
}
