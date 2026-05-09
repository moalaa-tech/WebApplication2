using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.Employee
{
    public class CreateEmployeeViewModel
    {
        [Required]
        public required string FirstName { get; set; }

        [Required]
        public required string LastName { get; set; }

        public required int DepartmentId { get; set; }
        public required int JobTitleId { get; set; }
    }
}
