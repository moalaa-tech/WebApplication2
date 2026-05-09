using CRM.Domain.Base;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CRM.Domain.Entities.MarketingAutomation
{
    public class Segment:BaseEntity
    {
        public string Name { get; set; }

        [StringLength(500)]
        public string Description { get; set; }

        public string Criteria { get; set; }

        public DateTime CreatedDate { get; set; }
        public DateTime? LastModifiedDate { get; set; }
        public bool IsActive { get; set; }

        public string SegmentType { get; set; }
    }
}
