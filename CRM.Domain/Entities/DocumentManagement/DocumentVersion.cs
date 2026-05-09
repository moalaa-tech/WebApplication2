using CRM.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.DocumentManagement
{
    public class DocumentVersion : BaseEntity
    {
        public int DocumentId { get; set; }
        public Document Document { get; set; }

        [Required]
        public int VersionNumber { get; set; }

        [Required]
        [StringLength(255)]
        public string FileName { get; set; }

        [Required]
        public long FileSize { get; set; }

        [Required]
        public string FilePath { get; set; }

        public string Changes { get; set; }

        [Required]
        public string ModifiedBy { get; set; }

        [Required]
        public DateTime ModifiedDate { get; set; } = DateTime.UtcNow;
    }
}
