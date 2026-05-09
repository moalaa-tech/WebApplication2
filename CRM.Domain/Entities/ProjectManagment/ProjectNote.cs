using CRM.Domain.Base;
using CRM.Domain.IdentityEntity;
using System.ComponentModel.DataAnnotations;

namespace CRM.Domain.Entities.ProjectManagment
{
    public class ProjectNote : BaseEntity
    {
        [Required]
        public int ProjectId { get; set; }
        public Project Project { get; set; }

        [Required]
        public int UserId { get; set; }
        public ApplicationUser User { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }
        public bool IsImportant { get; set; }

        [StringLength(50)]
        public string Category { get; set; } // General, Technical, Financial, etc.
    }
}
