using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.InventoryManagement.Order
{
    public class UsedAssignedLocationViewModel
    {
        public int Id { get; set; }

        [Required]
        public int EmployeeId { get; set; }
        public string? EmployeeName { get; set; }

        [Required]
        public int CountryId { get; set; }
        public string? CountryName { get; set; }

        [Required]
        public int StatesId { get; set; }
        public string? StateName { get; set; }

        [Required]
        public int CityId { get; set; }
        public string? CityName { get; set; }
    }
}
