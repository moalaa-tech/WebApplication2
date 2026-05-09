using CRM.WebApp.DTOs.OrderDetails;
using System.ComponentModel.DataAnnotations;

namespace CRM.WebApp.DTOs.Order
{
    public class CreateOrderDto
    {
        [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters.")]
        public string? Description { get; set; }

        [Display(Name = "Customer")] 
        public required int CustomerId { get; set; }

        [Range(0.01, double.MaxValue, ErrorMessage = "Total Amount must be greater than zero.")]

        public decimal? TotalAmount { get; set; }

        public required List<OrderDetailCreateDto> Items { get; set; } = new List<OrderDetailCreateDto>();
    }
}
