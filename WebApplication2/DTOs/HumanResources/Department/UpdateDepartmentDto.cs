using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.HumanResources.Department
{
    public class UpdateDepartmentDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Department name is required.")]
        [StringLength(100, ErrorMessage = "Department name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Department name ar is required.")]
        [StringLength(100, ErrorMessage = "Department name ar cannot exceed 100 characters.")]
        public string NameAr { get; set; }



        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters.")]
        public string Description { get; set; }

        [Required(ErrorMessage = "Location is required.")]
        [StringLength(100, ErrorMessage = "Location cannot exceed 100 characters.")]
        public string Location { get; set; }

        [Required(ErrorMessage = "Budget is required.")]
        [Range(0.01, 1000000000.00, ErrorMessage = "Budget must be a positive value.")]
        public decimal Budget { get; set; }

        [Required(ErrorMessage = "Established Date is required.")]
        [DataType(DataType.Date)]
        [Display(Name = "Established Date")]
        public DateTime EstablishedDate { get; set; }

        [Display(Name = "Manager")]
        public int? ManagerId { get; set; }
    }
}
