using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.DocumentManagement
{
    public class UpdateDocumentFolderDto
    {
        [Required]
        public int Id { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public int? ParentFolderId { get; set; }
        public bool IsSystemFolder { get; set; }
        public bool IsActive { get; set; }
    }
}
