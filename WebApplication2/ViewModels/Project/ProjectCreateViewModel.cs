using CRM.Domain.Enums.ProjectManagment;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.Project
{
    public class ProjectCreateViewModel
    {
        [Required]
        [Display(Name = "Project Code")]
        public string ProjectCode { get; set; }

        [Required]
        [Display(Name = "Project Name")]
        public string Name { get; set; }

        [Display(Name = "Description")]
        public string Description { get; set; }

        [Required]
        [Display(Name = "Customer")]
        public int CustomerId { get; set; }


        [Required]
        [Display(Name = "Start Date")]
        [DataType(DataType.Date)]
        public DateTime StartDate { get; set; } = DateTime.Today;

        [Display(Name = "End Date")]
        [DataType(DataType.Date)]
        public DateTime? EndDate { get; set; }

        [Required]
        [Display(Name = "Status")]
        public ProjectStatus Status { get; set; } = ProjectStatus.Planning;

        [Display(Name = "Budget")]
        [DataType(DataType.Currency)]
        public decimal Budget { get; set; }

        public IEnumerable<SelectListItem>? Customers { get; set; }
    }
}
