using CRM.Domain.Enums.ProjectManagment;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.Project
{
    public class CreateProjectDto
    {
        [Required]
        [StringLength(20)]
        public string ProjectCode { get; set; }

        [Required]
        [StringLength(100)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public int CustomerId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        public DateTime? EndDate { get; set; }

        [Required]
        public ProjectStatus Status { get; set; }

        [Range(0, 999999999)]
        public decimal Budget { get; set; }
    }
}
