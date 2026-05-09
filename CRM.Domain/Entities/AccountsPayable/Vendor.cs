using CRM.Domain.Base;
using CRM.Domain.Enums;

namespace CRM.Domain.Entities.AccountsPayable
{
    public class Vendor : BaseEntity
    {
        public string Name { get; set; }
        public string NameAr { get; set; }
        public string TaxId { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public PaymentTerm PaymentTerm { get; set; }

        public ICollection<Invoice> Invoices { get; set; }
        public ICollection<Payment> Payments { get; set; }
    }
}
