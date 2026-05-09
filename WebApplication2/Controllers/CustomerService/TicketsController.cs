using AutoMapper;
using CRM.WebApp.DTOs.CustomerService.Ticket;
using CRM.WebApp.Services.CustomerSupport_Service;
using CRM.WebApp.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CRM.WebApp.Controllers.CustomerService
{
    //[Authorize]
    public class TicketsController : Controller
    {
        private readonly ITicketService _ticketService;
        private readonly ICustomerService _customerService; // Add this
        private readonly IUserService _userService; // Add this

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
            return View(tickets);
        }

        [HttpGet]
        public async Task<IActionResult> Details(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        [HttpGet]
        public async Task<IActionResult> Create()
        {
            var customers = await _customerService.GetAllCustomersAsync();
            ViewBag.Customers = customers;
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateTicketDto ticketDto)
        {
            if (ModelState.IsValid)
            {
                await _ticketService.CreateTicketAsync(ticketDto);
                return RedirectToAction(nameof(Index));
            }
            var errors = ModelState.Values.SelectMany(v => v.Errors);
            var customers = await _customerService.GetAllCustomersAsync();
            ViewBag.Customers = customers;
            return View(ticketDto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(int id)
        {
            var customers = await _customerService.GetAllCustomersAsync();
            ViewBag.Customers = customers;

            var users = await _userService.GetAllUsersAsync();
            ViewBag.Users = users;


            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, UpdateTicketDto ticketDto)
        {
            if (id != ticketDto.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                await _ticketService.UpdateTicketAsync(ticketDto);
                return RedirectToAction(nameof(Index));
            }
            var customers = await _customerService.GetAllCustomersAsync();
            ViewBag.Customers = customers;

            var users = await _userService.GetAllUsersAsync();
            ViewBag.Users = users;


            return View(ticketDto);
        }

        [HttpGet]
        public async Task<IActionResult> Delete(int id)
        {
            var ticket = await _ticketService.GetTicketByIdAsync(id);
            if (ticket == null)
            {
                return NotFound();
            }
            return View(ticket);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            await _ticketService.DeleteTicketAsync(id);
            return RedirectToAction(nameof(Index));
        }
    }
}
