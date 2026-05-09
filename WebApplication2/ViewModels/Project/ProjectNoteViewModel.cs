using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.Project
{
    public class ProjectNoteViewModel
    {
        public int Id { get; set; }

        [Required]
        public int ProjectId { get; set; }

        [Required]
        [StringLength(100)]
        public string Title { get; set; }

        [Required]
        public string Content { get; set; }

        public bool IsImportant { get; set; }

        [StringLength(50)]
        public string Category { get; set; }
    }
}
