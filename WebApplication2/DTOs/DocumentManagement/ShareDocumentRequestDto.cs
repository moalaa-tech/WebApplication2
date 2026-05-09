using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.DocumentManagement
{
    public class ShareDocumentRequestDto
    {
        [Required]
        public int DocumentId { get; set; }
        [Required]
        public int SharedWithUserId { get; set; }
        [Required]
        public string PermissionLevel { get; set; }
        public DateTime? ExpiryDate { get; set; }
        public bool CanDownload { get; set; } = true;
    }
}
