using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.AssetsManagment
{
    public class GenerateDepreciationDto
    {
        [Required]
        public int FixedAssetId { get; set; }

        [Required]
        public DateTime StartDate { get; set; }

        [Required]
        public DateTime EndDate { get; set; }

        public bool PostImmediately { get; set; }
    }
}
