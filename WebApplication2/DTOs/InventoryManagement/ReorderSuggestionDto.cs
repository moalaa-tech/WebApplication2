namespace CRM.WebApp.DTOs.InventoryManagement
{
    public class ReorderSuggestionDto
    {
        public int ProductId { get; set; }

        public string ProductName { get; set; }

        public string SKU { get; set; }

        /// <summary>
        /// Current stock quantity available in inventory.
        /// </summary>
        public int CurrentStock { get; set; }

        /// <summary>
        /// Minimum quantity required to avoid shortages.
        /// </summary>
        public int MinimumStockLevel { get; set; }

        /// <summary>
        /// Suggested quantity to reorder = (MinimumStockLevel - CurrentStock)
        /// Ensured to never be negative (use max(0, MinimumStockLevel - CurrentStock)).
        /// </summary>
        public int SuggestedOrderQuantity { get; set; }
    }
}
