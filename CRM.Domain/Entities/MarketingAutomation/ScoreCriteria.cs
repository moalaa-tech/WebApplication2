using CRM.Domain.Base;
using CRM.Domain.Enums.MarketingAutomation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.MarketingAutomation
{
    public class ScoreCriteria : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public int DefaultPoints { get; set; }
        public bool IsActive { get; set; } = true;
        public ScoreCategory Category { get; set; }

        public ICollection<LeadScore> LeadScores { get; set; } = new List<LeadScore>();
    }
}
