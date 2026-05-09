using CRM.Domain.Base;
using CRM.Domain.Enums.AssetsManagment;
using System.ComponentModel.DataAnnotations.Schema;


namespace CRM.Domain.Entities.AssetsManagment
{
    public class FixedAsset : BaseEntity
    {
        public string AssetNumber { get; set; }
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Description { get; set; }
        public DateTime AcquisitionDate { get; set; }
        public decimal AcquisitionCost { get; set; }
        public decimal SalvageValue { get; set; }
        public int UsefulLife { get; set; } // in months
        public DepreciationMethod DepreciationMethod { get; set; }
        public int? ParentAssetId { get; set; }
        public FixedAsset ParentAsset { get; set; }
        public ICollection<FixedAsset> ChildAssets { get; set; }
        public ICollection<DepreciationSchedule> DepreciationSchedules { get; set; }


        // Calculated properties
        [NotMapped]
        public decimal CurrentBookValue => CalculateCurrentBookValue();

        [NotMapped]
        public decimal TotalDepreciation => CalculateTotalDepreciation();

        [NotMapped]
        public int RemainingLife => CalculateRemainingLife();

        private decimal CalculateCurrentBookValue()
        {
            var totalDepreciation = DepreciationSchedules?
                .Where(d => d.IsPosted)
                .Sum(d => d.DepreciationAmount) ?? 0;
            return AcquisitionCost - totalDepreciation;
        }

        private decimal CalculateTotalDepreciation()
        {
            return DepreciationSchedules?
                .Where(d => d.IsPosted)
                .Sum(d => d.DepreciationAmount) ?? 0;
        }

        private int CalculateRemainingLife()
        {
            var monthsDepreciated = DepreciationSchedules?
                .Count(d => d.IsPosted) ?? 0;
            return UsefulLife - monthsDepreciated;
        }
    }
}
