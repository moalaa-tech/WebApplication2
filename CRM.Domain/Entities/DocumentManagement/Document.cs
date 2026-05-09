using CRM.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.DocumentManagement
{
    public class Document : BaseEntity
    {
        [Required]
        [StringLength(255)]
        public string Title { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        [StringLength(50)]
        public string DocumentType { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; }

        [Required]
        public long FileSize { get; set; }

        [Required]
        [StringLength(10)]
        public string FileExtension { get; set; }

        [Required]
        public string FilePath { get; set; }

        [Required]
        public string MimeType { get; set; }

        public int? CategoryId { get; set; }
        public DocumentCategory Category { get; set; }

        public int? FolderId { get; set; }
        public DocumentFolder Folder { get; set; }

        [Required]
        public int Version { get; set; } = 1;

        public bool IsLatestVersion { get; set; } = true;

        public int? PreviousVersionId { get; set; }
        public Document PreviousVersion { get; set; }

        [Required]
        public string UploadedBy { get; set; }

        [Required]
        public DateTime UploadDate { get; set; } = DateTime.UtcNow;

        public DateTime? ModifiedDate { get; set; }

        public DateTime? ExpiryDate { get; set; }

        [Required]
        public bool IsPublic { get; set; }

        [StringLength(50)]
        public string Status { get; set; } = "Active";

        public string Tags { get; set; }

        public int DownloadCount { get; set; }

        // Navigation properties
        public ICollection<DocumentShare> SharedWith { get; set; }
        public ICollection<DocumentVersion> VersionHistory { get; set; }
    }
}
