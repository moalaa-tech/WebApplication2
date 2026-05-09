using CRM.Domain.Base;
using CRM.Domain.Entities.MarketingAutomation;
using CRM.Domain.Enums;


namespace CRM.Domain.Entities
{
    public class Contact : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string Surname { get; set; }
        public string Phone { get; set; }
        public string Email { get; set; }
        public string Position { get; set; }
        public int UserId { get; set; }
        public int IsDeleted { get; set; }

        public DateTime? LastContactDate { get; set; }

        public int CompanyId { get; set; }
        public Company Company { get; set; }

        // Navigation properties
        public ICollection<Interaction> Interactions { get; set; }
        public ICollection<CampaignContact> Campaigns { get; set; }
        public ICollection<AutomationContact> Automations { get; set; }
        public ContactStatus Status { get; set; }
    }
}
