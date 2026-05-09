namespace CRM.WebApp.DTOs.SupplyChainManagement.Procurement
{
    public class UpdateSupplierDto : CreateSupplierDto
    {
        public int Id { get; set; }
        public bool IsActive { get; set; }
    }
}
