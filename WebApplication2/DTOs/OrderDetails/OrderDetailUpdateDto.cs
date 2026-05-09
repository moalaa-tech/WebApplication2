using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.OrderDetails
{
    public class OrderDetailUpdateDto
    {
        public int Id { get; set; } // 0 for new items, existing ID for updates

        [Required(ErrorMessage = "Item Name is required.")]
        public string ItemName { get; set; }

        [Required(ErrorMessage = "Quantity is required.")]
        [Range(1, int.MaxValue, ErrorMessage = "Quantity must be at least 1.")]
        public int Quantity { get; set; }

        [Required(ErrorMessage = "Unit Price is required.")]
        [Range(0.01, double.MaxValue, ErrorMessage = "Unit Price must be greater than 0.")]
        public decimal UnitPrice { get; set; }

        public bool IsDeleted { get; set; } // To mark an existing detail for deletion

    }
}
