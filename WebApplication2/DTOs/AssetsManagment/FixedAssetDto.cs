using CRM.Domain.Enums.AssetsManagment;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.AssetsManagment
{
    public class FixedAssetDto
    {
        public int Id { get; set; }

        [Required]
        [StringLength(50)]
        public string AssetNumber { get; set; }

        [Required]
        [StringLength(200)]
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        [Required]
        public DateTime AcquisitionDate { get; set; }

        [Required]
        public decimal AcquisitionCost { get; set; }

        [Required]
        public decimal SalvageValue { get; set; }

        [Required]
        public int UsefulLife { get; set; }

        [Required]
        public DepreciationMethod DepreciationMethod { get; set; }
        public string DepreciationMethodName { get; set; }

        public int? ParentAssetId { get; set; }
        public string ParentAssetName { get; set; }

        public decimal CurrentBookValue { get; set; }
        public decimal TotalDepreciation { get; set; }
        public int RemainingLife { get; set; }
        public decimal MonthlyDepreciation { get; set; }

        public bool IsActive { get; set; }
        public DateTime CreatedAt { get; set; }
        public DateTime? UpdatedAt { get; set; }

        public int ChildAssetsCount { get; set; }
        public int DepreciationSchedulesCount { get; set; }
    }
}
