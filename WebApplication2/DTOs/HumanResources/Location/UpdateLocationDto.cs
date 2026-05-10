using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.HumanResources.Location
{
    public class UpdateLocationDto
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "Location name is required.")]
        [StringLength(100, ErrorMessage = "Location name cannot exceed 100 characters.")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Location name ar is required.")]
        [StringLength(100, ErrorMessage = "Location name ar cannot exceed 100 characters.")]
        public string NameAr { get; set; }

        [StringLength(200, ErrorMessage = "Address cannot exceed 200 characters.")]
        public string Address { get; set; }

        [StringLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
        public string City { get; set; }

        [StringLength(100, ErrorMessage = "State cannot exceed 100 characters.")]
        public string State { get; set; }

        [StringLength(100, ErrorMessage = "Country cannot exceed 100 characters.")]
        public string Country { get; set; }

        [StringLength(20, ErrorMessage = "Postal code cannot exceed 20 characters.")]
        public string PostalCode { get; set; }

        [Phone(ErrorMessage = "Invalid phone number format.")]
        [StringLength(20, ErrorMessage = "Phone cannot exceed 20 characters.")]
        public string Phone { get; set; }

        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters.")]
        public string Email { get; set; }

        [Range(1, 10000, ErrorMessage = "Capacity must be between 1 and 10000.")]
        public int? Capacity { get; set; }

        [Display(Name = "Manager")]
        public int? ManagerId { get; set; }
    }
}
