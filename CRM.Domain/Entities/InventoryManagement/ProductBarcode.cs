using CRM.Domain.Base;

namespace CRM.Domain.Entities.InventoryManagement
{
    public class ProductBarcode : BaseEntity
    {
        public required string Code { get; set; }
        public string? Symbology { get; set; } // e.g., EAN13, UPC, QR
        public bool IsPrimary { get; set; }

        public int ProductId { get; set; }
        public Product Product { get; set; }

        public int? ProductVariantId { get; set; }
        public ProductVariant? ProductVariant { get; set; }
        public string BarcodeValue { get; set; }
    }
}