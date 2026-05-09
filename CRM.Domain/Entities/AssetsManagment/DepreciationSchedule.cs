using CRM.Domain.Base;

namespace CRM.Domain.Entities.AssetsManagment
{
    public class DepreciationSchedule : BaseEntity
    {
        public int FixedAssetId { get; set; }
        public DateTime ScheduleDate { get; set; }
        public decimal DepreciationAmount { get; set; }
        public decimal AccumulatedDepreciation { get; set; }
        public decimal BookValue { get; set; }
        public bool IsPosted { get; set; }
        public FixedAsset FixedAsset { get; set; }
        public string Notes { get; set; }

    }
}
