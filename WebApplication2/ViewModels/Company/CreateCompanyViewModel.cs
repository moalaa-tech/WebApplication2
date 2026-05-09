using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CRM.WebApp.ViewModels.Company
{
    public class CreateCompanyViewModel
    {
        [Required(ErrorMessage = "Company Name is required.")]
        [StringLength(200, MinimumLength = 2, ErrorMessage = "Company Name must be between 2 and 200 characters.")]
        [DisplayName("Company Name")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Arabic Company Name cannot exceed 200 characters.")]
        [DisplayName("Arabic Company Name")]
        public string NameAr { get; set; }

        [Required(ErrorMessage = "Business is required.")]
        [DisplayName("Business")]
        public int BusinessId { get; set; }

        [Required(ErrorMessage = "Address is required.")]
        [StringLength(500, ErrorMessage = "Address cannot exceed 500 characters.")]
        [DisplayName("Address")]
        public string Address { get; set; }

        [Required(ErrorMessage = "City is required.")]
        [StringLength(100, ErrorMessage = "City cannot exceed 100 characters.")]
        [DisplayName("City")]
        public string City { get; set; }

        [Required(ErrorMessage = "User is required.")]
        [DisplayName("User")]
        public int UserId { get; set; }

        // For dropdown lists
        public List<SelectListItem> Businesses { get; set; } = new List<SelectListItem>();
        public List<SelectListItem> Users { get; set; } = new List<SelectListItem>();
    }
}