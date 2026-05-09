namespace CRM.WebApp.DTOs.InventoryManagement
{
    public class BarcodeDto
    {
        public int Id { get; set; }
        public string Code { get; set; }
        public string? Symbology { get; set; }
        public bool IsPrimary { get; set; }
        public int ProductId { get; set; }
        public int? ProductVariantId { get; set; }
    }
}