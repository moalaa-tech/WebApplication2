using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.DTOs.HumanResources.JobTitle
{
    public class UpdateJobTitleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int DepartmentId { get; set; }

        public IEnumerable<SelectListItem> Departments { get; set; }

    }
}
