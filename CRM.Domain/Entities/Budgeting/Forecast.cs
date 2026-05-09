using CRM.Domain.Base;
using CRM.Domain.Enums;

namespace CRM.Domain.Entities.Budgeting
{
    public class Forecast : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public DateTime ForecastDate { get; set; }
        public ForecastScenario Scenario { get; set; }

        public ICollection<ForecastLine> Lines { get; set; }
    }
}
