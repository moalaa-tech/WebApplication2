using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.HumanResources.Employee
{
    public class UpdateEmployeeDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        public string LastName { get; set; }

        [StringLength(250)]
        public string Description { get; set; }

        public int? DepartmentId { get; set; }
        public int GenderId { get; set; }
    }
}
