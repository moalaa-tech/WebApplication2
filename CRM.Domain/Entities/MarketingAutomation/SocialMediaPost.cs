using CRM.Domain.Base;
using CRM.Domain.Enums.MarketingAutomation;
using CRM.Domain.IdentityEntity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.MarketingAutomation
{
    public class SocialMediaPost : BaseEntity
    {
        public required string Content { get; set; }
        public DateTime ScheduledTime { get; set; }
        public SocialMediaPlatform Platform { get; set; }
        public PostStatus Status { get; set; }
        public int CreatedById { get; set; }
        public required ApplicationUser CreatedBy { get; set; }
        public int ModifiedById { get; set; }
        public required ApplicationUser ModifiedBy { get; set; }
    }
}
