using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.HumanResources.Employee
{
    public class CreateEmployeeDto
    {
        [Required]
        [StringLength(100)]
        [DisplayName("First Name")]
        public required string FirstName { get; set; }

        [Required]
        [StringLength(100)]
        [DisplayName("Last Name")]
        public required string LastName { get; set; }

        [StringLength(250)]
        [DisplayName("Description")]
        public required string Description { get; set; }

        [DisplayName("Department")]
        public int? DepartmentId { get; set; }

        [DisplayName("Gender")]
        public required string Gender { get; set; }
    }
}
