using AutoMapper;
using CRM.WebApp.DTOs.CustomerService.Ticket;
using CRM.WebApp.Services.CustomerSupport_Service;
using CRM.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace WebApplication2.Api.Controllers.CustomerService
{
    [ApiController]
    [Route("api/[controller]")]
    public class TicketsController : ControllerBase
    {
        private readonly ITicketService _ticketService;
        private readonly ICustomerService _customerService;
        private readonly IUserService _userService;

        private readonly IMapper _mapper;

        public TicketsController(
            ITicketService ticketService,
            IMapper mapper,
            ICustomerService customerService,
            IUserService userService)
        {
            _ticketService = ticketService;
            _mapper = mapper;
            _customerService = customerService;
            _userService = userService;
        }

        [HttpGet]
        public async Task<IActionResult> Index()
        {
            var tickets = await _ticketService.GetAllTicketsAsync();
            return Ok(tickets);
        }

        [HttpGet("{id:int}")]
        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return Ok(ticket);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateTicketDto ticketDto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _ticketService.CreateTicketAsync(ticketDto);
            return Ok();
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> Edit(int id, [FromBody] UpdateTicketDto ticketDto)
        {
            if (id != ticketDto.Id)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            await _ticketService.UpdateTicketAsync(ticketDto);
            return Ok();
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> Delete(int id)
        {
            await _ticketService.DeleteTicketAsync(id);
            return Ok();
        }
    }
}