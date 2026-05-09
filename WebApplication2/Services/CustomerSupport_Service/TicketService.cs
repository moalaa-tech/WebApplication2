using AutoMapper;
using CRM.Domain.Entities.CustomerService;
using CRM.Domain.Enums.CustomerService;
using CRM.WebApp.DTOs.CustomerService.Ticket;
using CRM.WebApp.Repositories;
using Microsoft.EntityFrameworkCore;

namespace CRM.WebApp.Services.CustomerSupport_Service
{
    public class TicketService : ITicketService
    {
        private readonly IRepository<Ticket> _ticketRepository;
        private readonly IMapper _mapper;

        public TicketService(IRepository<Ticket> ticketRepository, IMapper mapper)
        {
            _ticketRepository = ticketRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TicketDto>> GetAllTicketsAsync()
        {
            var tickets = await _ticketRepository.GetAllAsync(a => a.Customer, x => x.AssignedAgent)
                .ToListAsync();
            return _mapper.Map<IEnumerable<TicketDto>>(tickets);
        }

        public async Task<TicketDto> GetTicketByIdAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdWithIncludeAsync(a => a.Id == id, x => x.Customer);
            return _mapper.Map<TicketDto>(ticket);
        }

        public async Task CreateTicketAsync(CreateTicketDto ticketDto)
        {
            var ticket = _mapper.Map<Ticket>(ticketDto);
            await _ticketRepository.AddAsync(ticket);
            await _ticketRepository.SaveChangesAsync();
        }

        public async Task UpdateTicketAsync(UpdateTicketDto ticketDto)
        {
            var ticket = _mapper.Map<Ticket>(ticketDto);
            _ticketRepository.Update(ticket);
            await _ticketRepository.SaveChangesAsync();
        }

        public async Task DeleteTicketAsync(int id)
        {
            var ticket = await _ticketRepository.GetByIdAsync(id);

            _ticketRepository.Delete(ticket);
            await _ticketRepository.SaveChangesAsync();
        }

        public async Task<IEnumerable<TicketDto>> GetTicketsByCustomerAsync(int customerId)
        {
            var tickets = await _ticketRepository.GetAllAsync(a => a.Customer, x => x.AssignedAgent).Where(t => t.CustomerId == customerId).ToListAsync();
            return _mapper.Map<IEnumerable<TicketDto>>(tickets);
        }

        public async Task<IEnumerable<TicketDto>> GetTicketsByStatusAsync(TicketStatus status)
        {
            var tickets = await _ticketRepository.GetAllAsync(a => a.Customer, x => x.AssignedAgent).Where(t => t.Status == status).ToListAsync();
            return _mapper.Map<IEnumerable<TicketDto>>(tickets);
        }
    }
}
