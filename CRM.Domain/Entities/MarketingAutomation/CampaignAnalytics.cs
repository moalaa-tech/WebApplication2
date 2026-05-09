using CRM.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.MarketingAutomation
{
    public class CampaignAnalytics : BaseEntity
    {

        public int CampaignId { get; set; }
        public Campaign Campaign { get; set; }



        public DateTime StartDate { get; set; }

        public DateTime EndDate { get; set; }

        public decimal TotalBudget { get; set; }

        public decimal TotalSpent { get; set; }

        public int Impressions { get; set; }
        public int Clicks { get; set; }
        public int Conversions { get; set; }

        public decimal ConversionRate { get; set; }

        public decimal CostPerClick { get; set; }

        public decimal CostPerConversion { get; set; }

        public decimal RevenueGenerated { get; set; }

        public decimal ROIPercentage { get; set; }

        public DateTime RecordedDate { get; set; }
        public DateTime? LastUpdated { get; set; }

        public string Notes { get; set; }

        public string Channel { get; set; } // e.g., Social, Email, PPC, etc.
    }
}
