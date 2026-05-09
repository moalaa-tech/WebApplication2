using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.DocumentManagement
{
    public class CreateDocumentCategoryDto
    {
        [Required]
        public required string Name { get; set; }

        [Required]
        public required string NameAr { get; set; }

        public required string Description { get; set; }
        public int? ParentCategoryId { get; set; }
        public bool IsActive { get; set; } = true;
        public int Order { get; set; }
    }
}
