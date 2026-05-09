using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.ViewModels.SupplyChainManagement.Purchase
{
    public class EditPurchaseOrderViewModel : CreatePurchaseOrderViewModel
    {
        public int Id { get; set; }

        [DisplayName("Status")]
        public string Status { get; set; }

        [DisplayName("Total Amount")]
        [DataType(DataType.Currency)]
        public decimal TotalAmount { get; set; }
    }
}
