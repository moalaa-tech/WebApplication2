using CRM.Domain.Enums.CustomerService;
using CRM.WebApp.DTOs.CustomerService.Ticket;

namespace CRM.WebApp.Services.CustomerSupport_Service
{
    public interface ITicketService
    {
        Task<IEnumerable<TicketDto>> GetAllTicketsAsync();
        Task<TicketDto> GetTicketByIdAsync(int id);
        Task CreateTicketAsync(CreateTicketDto ticketDto);
        Task UpdateTicketAsync(UpdateTicketDto ticketDto);
        Task DeleteTicketAsync(int id);
        Task<IEnumerable<TicketDto>> GetTicketsByCustomerAsync(int customerId);
        Task<IEnumerable<TicketDto>> GetTicketsByStatusAsync(TicketStatus status);
    }
}
