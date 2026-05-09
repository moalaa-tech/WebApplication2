namespace CRM.WebApp.ViewModels.SupplyChainManagement.Supplier
{
    public class SupplierDropdownViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Code { get; set; }
        public bool IsActive { get; set; }

        public string DisplayText => $"{Code} - {Name} {(IsActive ? "" : "(Inactive)")}";
    }
}
