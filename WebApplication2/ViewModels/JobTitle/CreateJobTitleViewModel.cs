using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.JobTitle
{
    public class CreateJobTitleViewModel
    {
        [Required]
        public required string Title { get; set; }

        [Required]
        public int DepartmentId { get; set; }

    }
}
