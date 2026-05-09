using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.Company
{
    public class CreateCompanyDto
    {
        [Required(ErrorMessage = "Company Name is required.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Company Name must be between 2 and 200 characters.")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Arabic Company Name cannot exceed 200 characters.")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "Business ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Business ID must be a positive number.")]
        public int BusinessId { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters.")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
        public string City { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "City ID must be a positive number.")]
        public int CityId { get; set; } = 1; // Default to 1, but can be overridden

        [Required(ErrorMessage = "User ID is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "User ID must be a positive number.")]
        public int UserId { get; set; }


    }
}
