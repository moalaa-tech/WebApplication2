namespace CRM.WebApp.ViewModels.SupplyChainManagement.Purchase
{
    public class EditPurchaseOrderItemViewModel : CreatePurchaseOrderItemViewModel
    {
        public int Id { get; set; }
        public int PurchaseOrderId { get; set; }
    }
}
