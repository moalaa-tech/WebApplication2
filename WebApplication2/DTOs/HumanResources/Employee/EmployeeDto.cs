using CRM.WebApp.DTOs.HumanResources.Department;
using System.ComponentModel;

namespace CRM.WebApp.DTOs.HumanResources.Employee
{
    public class EmployeeDto
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public int? DepartmentId { get; set; }
        public DepartmentDto Department { get; set; }
    }
}
