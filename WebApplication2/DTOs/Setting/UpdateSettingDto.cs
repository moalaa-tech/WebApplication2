using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.Setting
{
    public class UpdateSettingDto
    {
        public int Id { get; set; } // Required for updating

        [Required(ErrorMessage = "Setting name is required.")]
        [StringLength(100, ErrorMessage = "Setting name cannot exceed 100 characters.")]
        public required string Name { get; set; }

        [Required(ErrorMessage = "Setting name Ar is required.")]
        [StringLength(100, ErrorMessage = "Setting name Ar cannot exceed 100 characters.")]
        public required string NameAr { get; set; }


    }
}
