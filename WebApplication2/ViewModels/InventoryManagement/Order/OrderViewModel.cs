using CRM.Domain.Enums;
using CRM.WebApp.DTOs.Order;
using CRM.WebApp.Paging;

namespace CRM.WebApp.ViewModels.InventoryManagement.Order
{
    public class OrderViewModel
    {
        public int? InvoiceNumber { get; set; }
       
        public string? Search { get; set; }
        public InvoiceStatus? Status { get; set; }

        public int? EmployeeId { get; set; }
        public int? CustomerId { get; set; }

        public DateTime? DateFrom { get; set; }
        public DateTime? DateTo { get; set; }

        public int? ProductId { get; set; }

        public int? States { get; set; }

        public int? StateId { get; set; }

        public int? CountryId { get; set; }
        public int? CityId { get; set; }


        public int PageIndex { get; set; } = 1;
        public int pageSize { get; set; } = 10;
        public PaginatedList<OrderDto>? Result { get; set; }
    }
}
