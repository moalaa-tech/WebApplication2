using CRM.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.MarketingAutomation
{
    public class LeadScoreRule : BaseEntity
    {
        public int LeadScoreId { get; set; }

        
        public string RuleName { get; set; }

       
        public string Condition { get; set; }

       
        public int Points { get; set; }

        public int ExecutionOrder { get; set; }


        // Navigation property
        public LeadScore LeadScore { get; set; }

    }
}
