using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.Customer
{
    public class CustomerViewModel
    {
        public int Id { get; set; }

        [DisplayName("Customer Name")]
        public string Name { get; set; }

        [DisplayName("Arabic Customer Name")]
        public string NameAr { get; set; }

        [DisplayName("Description")]
        public string Description { get; set; }

        [DisplayName("Email")]
        public string Email { get; set; }

        [DisplayName("Address")]
        public string Address { get; set; }

        [DisplayName("Phone")]
        public string Phone { get; set; }

        [DisplayName("Credit Limit")]
        [DataType(DataType.Currency)]
        public decimal CreditLimit { get; set; }
    }
}