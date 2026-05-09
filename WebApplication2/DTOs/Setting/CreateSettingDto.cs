using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.Setting
{
    public class CreateSettingDto
    {
        [Required(ErrorMessage = "Setting name is required.")]
        [StringLength(100, ErrorMessage = "Setting name cannot exceed 100 characters.")]
        public required string Name { get; set; }


        [Required(ErrorMessage = "Setting name is required.")]
        [StringLength(100, ErrorMessage = "Setting name cannot exceed 100 characters.")]
        public required string NameAr { get; set; }


    }
}
