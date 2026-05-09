using CRM.Domain.Base;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Entities.ProjectManagment
{
    public class ProjectDocument : BaseEntity
    {
        [Required]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        [Required]
        public string UserId { get; set; } // ASP.NET Core Identity user

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(255)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string FileType { get; set; } // pdf, docx, xlsx, etc.

        [Required]
        public string FilePath { get; set; } // Path to stored file

        [Required]
        public long FileSize { get; set; } // Size in bytes

        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        [StringLength(50)]
        public string DocumentType { get; set; } // Contract, Proposal, Report, etc.

        public int? Version { get; set; } = 1;

        public bool IsApproved { get; set; }
    }
}
