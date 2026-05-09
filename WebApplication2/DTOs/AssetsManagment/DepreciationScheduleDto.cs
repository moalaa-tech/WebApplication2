using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.AssetsManagment
{
    public class DepreciationScheduleDto
    {
        public int Id { get; set; }
        public int FixedAssetId { get; set; }
        public string AssetNumber { get; set; }
        public string AssetName { get; set; }

        [Required]
        public DateTime ScheduleDate { get; set; }

        [Required]
        public decimal DepreciationAmount { get; set; }

        [Required]
        public decimal AccumulatedDepreciation { get; set; }

        [Required]
        public decimal BookValue { get; set; }

        [Required]
        public bool IsPosted { get; set; }
        public string Notes { get; set; }

        public DateTime CreatedAt { get; set; }
    }
}
