using CRM.WebApp.DTOs.HumanResources.Employee;
using System.ComponentModel;

namespace CRM.WebApp.DTOs.HumanResources.Location
{
    public class LocationDto
    {
        public int Id { get; set; }
        [DisplayName("Location Name")]
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Address { get; set; }
        public string City { get; set; }
        public string State { get; set; }
        public string Country { get; set; }
        public string PostalCode { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public int? Capacity { get; set; }
        public string ManagerName { get; set; }
        public int? ManagerId { get; set; }
        public EmployeeDto Manager { get; set; }
        public bool IsActive { get; set; }
    }
}
