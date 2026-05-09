

using CRM.WebApi.DbContext.EasyOrderModels;

namespace CRM.WebApi.DbContext
{
    public class Order : BaseEntity
    {
        public string? Description { get; set; }

        public InvoiceStatus Status { get; set; } = InvoiceStatus.Open;

        public int? CustomerId { get; set; }
        public Customer? Customer { get; set; }


        public Guid? EasyOrderRequestId { get; set; }
        public EasyOrderRequest? EasyOrderRequest { get; set; }

        public ICollection<OrderDetails>? OrderDetails { get; set; }
    }
}
