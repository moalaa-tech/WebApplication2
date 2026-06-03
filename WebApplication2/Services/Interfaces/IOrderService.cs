using CRM.Domain.Enums;
using CRM.WebApp.Controllers.InventoryManagment;
using CRM.WebApp.DTOs.Order;
using CRM.WebApp.Paging;
using CRM.WebApp.ViewModels.InventoryManagement.Order;

namespace CRM.WebApp.Services.Interfaces
{
    public interface IOrderService
    {
        Task<PaginatedList<OrderDto>> GetAllOrdersAsync(OrderViewModel model);
        Task<OrderDto> GetOrderByIdAsync(int id);
        Task<OrderDto> CreateOrderAsync(CreateOrderDto createOrderDto);
        Task<UpdateOrderDto> UpdateOrderAsync(UpdateOrderDto updateOrderDto);
        Task DeleteOrderAsync(int id);
        Task<bool> OrderExistsAsync(int id);
        Task<IEnumerable<OrderDto>> Search(string search = null);

        Task<bool> ChangeStatusAsync(int id, InvoiceStatus newStatus);
        Task<bool> SaveAssignedLocationAsync(UpdateAreaDto model);
        Task<StatisticsData> CalculateStatisticsAsync(int? employeeId, DateTime? dateFrom, DateTime? dateTo);

    }
}
