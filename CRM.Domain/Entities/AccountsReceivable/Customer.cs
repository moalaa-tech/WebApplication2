using CRM.Domain.Base;
using CRM.Domain.Entities.CustomerService;
using CRM.Domain.Enums;

namespace CRM.Domain.Entities.AccountsReceivable
{
    public class Customer : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string? Description { get; set; }
        public string? Email { get; set; }

        public string? Address { get; set; }
        public string Phone { get; set; }

        public CreditTerm CreditTerm { get; set; }
        public decimal CreditLimit { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<Receipt> Receipts { get; set; }
        public ICollection<Ticket> Tickets { get; set; }

        public ICollection<ServiceRequest> ServiceRequests { get; set; }


    }
}
