using CRM.Domain.Base;
using CRM.Domain.Entities.Accounting;

namespace CRM.Domain.Entities.Budgeting
{
    public class ForecastLine : BaseEntity
    {
        public int ForecastId { get; set; }
        public int GLAccountId { get; set; }
        public int PeriodMonth { get; set; } // 1-12
        public decimal ForecastAmount { get; set; }
        public decimal ActualAmount { get; set; }
        public decimal Variance => ForecastAmount - ActualAmount;

        // Navigation properties
        public Forecast Forecast { get; set; }
        public GLAccount GLAccount { get; set; }
    }
}
