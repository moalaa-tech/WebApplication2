using CRM.Domain.Base;
using CRM.Domain.Entities.AccountsReceivable;
using CRM.Domain.Entities.MarketingAutomation.EasyOrder;
using CRM.Domain.Enums;
using CRM.Domain.IdentityEntity;

namespace CRM.Domain.Entities
{
    public class Order : BaseEntity
    {
        public string? Description { get; set; }
        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }
        public InvoiceStatus Status { get; set; } = InvoiceStatus.Pending;
        public int? AssignedToId { get; set; }
        public ApplicationUser? AssignedTo { get; set; }
        public int? StatesId { get; set; }
        public State? State { get; set; }


        public int? CityId { get; set; }
        public City? City { get; set; }

        public Guid? EasyOrderRequestId { get; set; }
        public EasyOrderRequest? EasyOrderRequest { get; set; }
        public ICollection<OrderDetails>? OrderDetails { get; set; }
    }
}
