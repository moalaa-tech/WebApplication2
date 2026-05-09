using CRM.Domain.Enums.AssetsManagment;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.AssetsManagment
{
    public class UpdateFixedAssetDto
    {
        [Required]
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
        [Range(0.01, double.MaxValue)]
        public decimal AcquisitionCost { get; set; }

        [Required]
        [Range(0, double.MaxValue)]
        public decimal SalvageValue { get; set; }

        [Required]
        [Range(1, int.MaxValue)]
        public int UsefulLife { get; set; }

        [Required]
        public DepreciationMethod DepreciationMethod { get; set; }

        public int? ParentAssetId { get; set; }
        public bool IsActive { get; set; }
    }
}
