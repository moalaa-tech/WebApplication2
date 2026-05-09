using CRM.WebApp.DTOs.HumanResources.Employee;
using System.ComponentModel;

namespace CRM.WebApp.DTOs.HumanResources.Department
{
    public class DepartmentDto
    {
        public int Id { get; set; }
        [DisplayName("Department Name")]
        public string Name { get; set; }
        public string NameAr { get; set; }

        public string Description { get; set; }
        public string Location { get; set; }
        public decimal Budget { get; set; }
        public DateTime EstablishedDate { get; set; }
        public string ManagerName { get; set; }
        public object EmployeeCount { get; internal set; }
        public int? ManagerId { get; internal set; }
        public EmployeeDto Manager { get; set; }
        public bool IsActive { get; set; }
    }
}
