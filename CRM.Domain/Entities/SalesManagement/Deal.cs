using CRM.Domain.Base;

namespace CRM.Domain.Entities.SalesManagement
{
    public class Deal : BaseEntity
    {
        public int OpportunityId { get; set; }
        public Opportunity Opportunity { get; set; }

        public decimal FinalValue { get; set; }

        public string Description { get; set; }
        public string DealOwner { get; set; }
        public string Name { get; set; }
        public string AccountName { get; set; }
        public bool Type { get; set; }
        public decimal Amount { get; set; }
        public DateTime ClosingDate { get; set; }
        public int ContactId { get; set; }
        public Contact Contact { get; set; }
        public List<DealFile> Files { get; set; }

        public int? QuoteId { get; set; }
        public virtual Quote Quote { get; set; }

    }
}
