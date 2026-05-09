using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.DTOs.HumanResources.JobTitle
{
    public class JobTitleDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string DepartmentName { get; set; }

        public IEnumerable<SelectListItem> Departments { get; set; }

    }
}
