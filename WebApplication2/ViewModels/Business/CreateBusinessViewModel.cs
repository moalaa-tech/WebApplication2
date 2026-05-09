using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.Business
{
    public class CreateBusinessViewModel
    {
        [Required(ErrorMessage = "Business Name is required.")]
        [StringLength(200, ErrorMessage = "Business Name cannot exceed 200 characters.")]
        [DisplayName("Business Name")]
        public string Name { get; set; }

        [StringLength(200, ErrorMessage = "Arabic Business Name cannot exceed 200 characters.")]
        [DisplayName("Arabic Business Name")]
        public string NameAr { get; set; }
    }
}